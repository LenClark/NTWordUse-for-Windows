using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NTWordUse
{
    public partial class frmMain : Form
    {
        /*******************************************************************************************************
         *                                                                                                     *
         *                                          NTWordUse                                                  *
         *                                          =========                                                  *
         *                                                                                                     *
         *  The purpose of the program is to display all words used in the New Testament, broken down by       *
         *    grammatical structure (e.g. Nouns by case, verbs by standard paradigms).                         *
         *                                                                                                     *
         *  Note: I have used the term "grammatical category" (sometimes just "Category") to cover the various *
         *        different aspects of grammar, such as noun, verb, adjective and so on.                       *
         *                                                                                                     *
         *******************************************************************************************************/

        bool isActivated = false;

        /*=====================================================================================================*
         *                                                                                                     *
         *                                     integer variables                                               *
         *                                     =================                                               *
         *                                                                                                     *
         *  categoryCode    he program is to display all words used in the New Testament, broken down by       *
         *  noOfNTBooks     The number of books identified by the NT Titles file.  This will also be used to   *
         *                  separate the NT books from the LXX books (which have an index following on from    *
         *                  the NT books                                                                       *
         *  noOfLxxBooks    The actual number of LXX books identified in the LXX title file. (1 to 59)         *
         *  noOfStoredBooks In fact, noOfNTBooks + noOfLxxBooks, used as the extent of the stored books        *
         *  nRootCount      The number of root entries                                                         *
         *  nNoOfNtRoots    The highest count for NT entries.  Values greater are from LXX uniquely.           *
         *                                                                                                     *
         *=====================================================================================================*/
        int categoryCode = 0, noOfNTBooks = 0, noOfLxxBooks = 0, noOfStoredBooks = 0, nRootCount = 0, noOfNtRoots = 0, ntRawCount = 0, lxxRawCount = 0;
        String fileBase = @"..\Source\", titlesFile = "Titles.txt", lxxTitlesFile = "LXX_Titles.txt", ntTextFile = "NTText.txt", lxxTextFolder = "LXX_Text";
        String keyEntryWorkspace = "";

        /*=====================================================================================================*
         *                                                                                                     *
         *  Category Codes:                                                                                    *
         *  ==============                                                                                     *
         *                                                                                                     *
         *  These are specific to the NT source data and are determined by the source data.  They are, in      *
         *  fact, field 2 of the NT source data.                                                               *
         *                                                                                                     *
         *=====================================================================================================*/
        String[] categoryCodes = { "N-", "V-", "A-", "D-", "P-", "RA", "RD", "RI", "RP", "RR", "X-", "C-", "I-" };
        String[] fullCategoryNames = { "Noun", "Verb", "Adjective", "Adverb", "Preposition", "Article", "Demonstrative Pronoun", "Indefinite Pronoun",
                                      "Personal Pronoun", "Relative Pronoun", "Particle", "Conjunction", "Interjection" };
        String[] tenseNames = { "Present", "Imperfect", "Aorist", "Perfect", "Pluperfect", "Future" };
        String[] moodNames = { "Indicative", "Imperative", "Subjunctive", "Optative", "Infinitive" };

        /*=====================================================================================================*
         *                                                                                                     *
         *                                            bookNames                                                *
         *                                            =========                                                *
         *                                                                                                     *
         *  This list stores information that allows us to access the data for NT books.  Each book is         *
         *  referenced by a code supplied in the titles file.  The list allows us to covert this into a        *
         *  meaningful book name.                                                                              *
         *                                                                                                     *
         *  Key:   A simple sequence number (1 upwards)                                                        *
         *  Value: An instance of classBook                                                                    *
         *                                                                                                     *
         *=====================================================================================================*/
        SortedList<int, classBooks> bookNames = new SortedList<int, classBooks>();

        /*=====================================================================================================*
         *                                                                                                     *
         *                                          rootDataStore                                              *
         *                                          =============                                              *
         *                                                                                                     *
         *  This list enables us to access the base root data, as required.  It holds data for both NT and     *
         *  LXX texts, so where root words are found in the NT, isInNT = true.  If isInNT = false, then the    *
         *  root is found only in the LXX.  (Note, where isInNT = true, the word may be found in the LXX, as   *
         *  well as in the NT).                                                                                *
         *                                                                                                     *
         *  Key:   A simple sequence number (1 upwards)                                                        *
         *  Value: An instance of classRootData                                                                *
         *                                                                                                     *
         *=====================================================================================================*/
        SortedList<int, classRootData> rootDataStore = new SortedList<int, classRootData>();

        /*=====================================================================================================*
         *                                                                                                     *
         *                                          rootLookup                                                 *
         *                                          ==========                                                 *
         *                                                                                                     *
         *  This is effectively the inverse of rootDataStore.                                                  *
         *                                                                                                     *
         *  Given the root form (e.g. λογος), how do we find the key to rootDataStore.  This lookup list       *
         *  us to do so.                                                                                       *
         *                                                                                                     *
         *  Note: the combined root form plus category code is used to create a unique key                     *
         *                                                                                                     *
         *  Key:   The lookup key (category code + root form)                                                  *
         *  Value: The integer key to classRootData                                                            *
         *                                                                                                     *
         *=====================================================================================================*/
        SortedList<String, int> rootLookup = new SortedList<string, int>();

        SortedList<Char, Char> gkFlattener = new SortedList<Char, Char>();

        // Grouped Controls
        RadioButton[] rbtnCategories;
        CheckBox[] chkTenses, chkMoods;
        Button[,] gkKeys;
        Form mainForm;
        RadioButton selectedButton;

        /*=====================================================================================================*
         *                                                                                                     *
         *                                       Global Classes                                                *
         *                                       ==============                                                *
         *                                                                                                     *
         *  Basically, these are classes that:                                                                 *
         *     a) have a single instance, used throughout the life of the application, and therefore           *
         *     b) need to be accessed by multiple other classes and forms.                                     *
         *                                                                                                     *
         *=====================================================================================================*/
        classGlobal globalVars;
        classProcesses processRawData;

        /*=====================================================================================================*
         *                                                                                                     *
         *  Background Tasks:                                                                                  *
         *  ================                                                                                   *
         *                                                                                                     *
         *  In order to enable visible progress, we need to perform the initialisation tasks as background     *
         *  tasks (and, in addition, as multi-tasks).  The following definitions support multi-tasking.  For   *
         *  this, we also need the line                                                                        *
         *                                                                                                     *
         *     using System.Threading;                                                                         *
         *                                                                                                     *
         *  above.                                                                                             *
         *                                                                                                     *
         *=====================================================================================================*/

        Thread initialisationThread;
        frmProgress progressForm;

        private delegate void performProgressAdvance();
        private delegate void performGBRButtonAddition(RadioButton rbtnTarget);
        private delegate void performGBCheckBoxAddition(GroupBox gbTarget, CheckBox cbTarget);
        private delegate void performKeyboardAddition(Button btnTarget);
        private delegate void performToolTipAddition(ToolTip ttTarget, Control ctrlTarget, String hint);
        private delegate void performListBoxAddition(String message);
        private delegate void postInitialisationCleanup();

        public frmMain()
        {
            InitializeComponent();
            mainForm = this;
            mainForm.Visible = false;
            processRawData = new classProcesses();
            globalVars = new classGlobal();
            globalVars.initialiseRegistry();
        }

        private void updateProgress()
        {
            progressForm.advanceProgressBar();
        }

        private void updateGbRButton(RadioButton rbtnTarget)
        {
            gbGrammarClass.Controls.Add(rbtnTarget);
        }

        private void updateGbCheckBox(GroupBox gbTarget, CheckBox cbTarget)
        {
            gbTarget.Controls.Add(cbTarget);
        }

        private void updateKeyboardWithButton(Button btnTarget)
        {
            pnlKeys.Controls.Add(btnTarget);
        }

        private void updateTooltip(ToolTip ttTarget, Control ctrlTarget, String hint)
        {
            ttTarget.SetToolTip(ctrlTarget, hint);
        }

        private void updateListbox(String message)
        {
            lbRootList.Items.Add(message);
        }

        private void doPostInitialisationCleanup()
        {
            if (lbRootList.Items.Count > 0) lbRootList.SelectedIndex = 0;
            progressForm.Close();
            progressForm.Dispose();
            mainForm.Visible = true;
        }
        private void frmMain_Activated(object sender, EventArgs e)
        {
            /*****************************************************************************************************************************
             *                                                                                                                           *
             *                                                     frmMain_Activated                                                     *
             *                                                     =================                                                     *
             *                                                                                                                           *
             *  This is the controling method for building the form.  We have used form activation rather than the class instantiation   *
             *  because the latter functions *before* the visible form is available and makes the display of progress impossible (or, at *
             *  least, difficult).  We ensure that the form creation elements of the activation method is only called once by means of   *
             *  the variable, isActivated.                                                                                               *
             *                                                                                                                           *
             *****************************************************************************************************************************/

            if (!isActivated)
            {

                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *  It takes some time to initialise the application, so we provide a visual indication of progress  *
                 *  using a dialog.                                                                                  *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/

                progressForm = new frmProgress();
                progressForm.Show();

                initialisationThread = new Thread(new ThreadStart(performInitialisation));
                initialisationThread.IsBackground = true;
                initialisationThread.Start();

                isActivated = true;
            }
        }
        private void performInitialisation()
        {
            int noOfCategories, idx;

            noOfCategories = categoryCodes.Length;

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 1a: Additional Controls for the main form                                                  *
                 *   =======                                                                                         
                 *                                                                                                   
                 *   This section will create a list of related Radio Buttons (displayed in the Group Box            *
                 *   gbGrammarClass and headed "Grammatical Categories").                                            *
                 *                                                                                                   *
                 *   This enables us to handle related controls as an array of values.                               *
                 *                                                                                                   *
                 *   Note: the application provides a window on _New Testament_ Greek word forms.  We are optionally *
                 *         including Septuagint data as _supporting_ information but the structures (and word forms  *
                 *         referenced) will specifically and only be those of the New Testament.                     *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                int top = 21, left = 16, height = 17, gap = 4;
                SortedDictionary<int, String> categoryLookup = new SortedDictionary<int, string>();
                RadioButton currentRButton;

                rbtnCategories = new RadioButton[noOfCategories];
                noOfCategories = fullCategoryNames.Length;
                for (idx = 0; idx < noOfCategories; idx++)
                {
                    categoryLookup.Add(idx, fullCategoryNames[idx]);
                    currentRButton = new RadioButton();
                    currentRButton.Tag = idx;
                    currentRButton.Name = "grammCat" + idx.ToString();
                    currentRButton.Text = fullCategoryNames[idx];
                    currentRButton.Top = top + (gap + height) * idx;
                    currentRButton.Left = left;
                    currentRButton.Height = height;
                    currentRButton.AutoSize = true;
                    if (idx == 0)
                    {
                        currentRButton.Checked = true;
                        selectedButton = currentRButton;
                    }
                    currentRButton.CheckedChanged += currentRButton_CheckedChanged;
                    gbGrammarClass.Invoke(new performGBRButtonAddition(updateGbRButton), currentRButton);
                    rbtnCategories[idx] = currentRButton;
                }
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 1b                                                                                         *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   Now a sequence of check boxes for both Tenses and Moods. (These are specific to Verbs.)         *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                int noOfTenses, noOfMoods, chkTTop = 58, chkGap = 22, chkTLeft = 20, chkMTop = 72, chkMLeft = 42;  // i.e. Noun, Verb, Adjective, etc.
                CheckBox tempCheckbox;

                noOfTenses = tenseNames.Length;
                chkTenses = new CheckBox[noOfTenses];
                for (idx = 0; idx < noOfTenses; idx++)
                {
                    tempCheckbox = new CheckBox();
                    tempCheckbox.Name = "chkTense" + idx.ToString();
                    tempCheckbox.Text = tenseNames[idx];
                    tempCheckbox.Top = chkTTop + (idx * chkGap);
                    tempCheckbox.Left = chkTLeft;
                    gbTenses.Invoke(new performGBCheckBoxAddition(updateGbCheckBox), gbTenses, tempCheckbox);
                    chkTenses[idx] = tempCheckbox;
                }
                noOfMoods = moodNames.Length;
                chkMoods = new CheckBox[noOfMoods];
                for (idx = 0; idx < noOfMoods; idx++)
                {
                    tempCheckbox = new CheckBox();
                    tempCheckbox.Name = "chkMood" + idx.ToString();
                    tempCheckbox.Text = moodNames[idx];
                    tempCheckbox.Top = chkMTop + (idx * chkGap);
                    tempCheckbox.Left = chkMLeft;
                    gbMood.Invoke(new performGBCheckBoxAddition(updateGbCheckBox), gbMood, tempCheckbox);
                    chkMoods[idx] = tempCheckbox;
                }
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 1c: Virtual Keyboard setup.                                                                *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                int keyHeight = 34, keyWidth = 34, keyBigWidth = 48, keyTop = 4, keyLeft = 40, keyGap = 4, keyRows = 5, keyCols = 6, kRowIdx, kColIdx, keySeq = 0;
                Button currentButton;
                String[,] keyFaces = {{ "α", "β", "γ", "δ", "ε", "BkSp" },
                                   { "ζ", "η", "θ", "ι", "κ", "Clear" },
                                   { "λ", "μ", "ν", "ξ", "ο", "π" },
                                   { "ρ", "σ", "ς", "τ", "υ", "φ" },
                                   { "χ", "ψ", "ω", "Space", " ", " " } };
                String[,] gkKeyHints = { { "alpha", "beta", "gamma", "delta", "epsilon", "Backspace" },
                            { "zeta", "eta", "theta", "iota", "kappa", "Clear" },
                            { "lambda", "mu", "nu", "xi", "omicron", "pi" },
                            { "rho", "sigma", "final sigma", "tau", "upsilon", "phi" },
                            { "chi", "psi", "omega", "Space", " ", " " } };
                Font buttonFont = new Font("Times New Roman", 12, FontStyle.Regular);
                ToolTip[,] keyToolTips = new ToolTip[keyRows, keyCols];

                gkKeys = new Button[keyRows, keyCols];
                for (kRowIdx = 0; kRowIdx < keyRows; kRowIdx++)
                {
                    for (kColIdx = 0; kColIdx < keyCols; kColIdx++)
                    {
                        currentButton = new Button();
                        currentButton.Height = keyHeight;
                        if ((kColIdx == keyCols - 1) && (kRowIdx < 2))
                        {
                            currentButton.Width = keyBigWidth;
                        }
                        else
                        {
                            if ((kColIdx == 3) && (kRowIdx == 4))
                            {
                                currentButton.Width = 2 * (keyWidth + keyGap) + keyBigWidth;
                            }
                            else
                            {
                                currentButton.Width = keyWidth;
                                currentButton.Font = buttonFont;
                            }
                        }
                        currentButton.Top = keyTop + kRowIdx * (keyHeight + keyGap);
                        currentButton.Left = keyLeft + kColIdx * (keyWidth + keyGap);
                        currentButton.Name = "Key_Row" + kRowIdx.ToString() + "_Col" + kColIdx.ToString();
                        currentButton.Tag = keySeq++;
                        currentButton.Text = keyFaces[kRowIdx, kColIdx];
                        currentButton.Click += keyButton_Click;
                        pnlKeys.Invoke(new performKeyboardAddition(updateKeyboardWithButton), currentButton);
                        gkKeys[kRowIdx, kColIdx] = currentButton;

                        keyToolTips[kRowIdx, kColIdx] = new ToolTip();
                        keyToolTips[kRowIdx, kColIdx].ToolTipTitle = "Key Value";
                        pnlKeys.Invoke(new performToolTipAddition(updateTooltip), keyToolTips[kRowIdx, kColIdx], gkKeys[kRowIdx, kColIdx], gkKeyHints[kRowIdx, kColIdx]);
                        if ((kColIdx == 3) && (kRowIdx == 4)) break;
                    }
                }
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 1d: Greek conversion data.                                                                 *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   Provide a "look-up table" to associate non-virgin Greek characters with their virgin            *
                 *     counterparts.  In other words for each combination of character with accent, breathing, iota  *
                 *     subscript or diaeresis, associate the base character.  Note also that capitals will also be   *
                 *     associated with lower case basic characters.                                                  *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/

                Char[] gkLetters = { 'ἀ', 'ἁ', 'ἂ', 'ἃ', 'ἄ', 'ἅ', 'ἆ', 'ἇ', 'Ἀ', 'Ἁ', 'Ἂ', 'Ἃ', 'Ἄ', 'Ἅ', 'Ἆ', 'Ἇ', 'ᾀ', 'ᾁ', 'ᾂ', 'ᾃ', 'ᾄ', 'ᾅ', 'ᾆ', 'ᾇ',
                                  'ᾈ', 'ᾉ', 'ᾊ', 'ᾋ', 'ᾌ', 'ᾍ', 'ᾎ', 'ᾏ', 'ᾰ', 'ᾱ', 'ᾲ', 'ᾳ', 'ᾴ', '᾵', 'ᾶ', 'ᾷ', 'Ᾰ', 'Ᾱ', 'Ὰ', 'Ά', 'ᾼ', 'ὰ', 'ά',
                                  'ἐ', 'ἑ', 'ἒ', 'ἓ', 'ἔ', 'ἕ', '἖', '἗', 'Ἐ', 'Ἑ', 'Ἒ', 'Ἓ', 'Ἔ', 'Ἕ', 'ὲ', 'έ', 'Ὲ', 'Έ',
                                  'ἠ', 'ἡ', 'ἢ', 'ἣ', 'ἤ', 'ἥ', 'ἦ', 'ἧ', 'Ἠ', 'Ἡ', 'Ἢ', 'Ἣ', 'Ἤ', 'Ἥ', 'Ἦ', 'Ἧ', 'ᾐ', 'ᾑ', 'ᾒ', 'ᾓ', 'ᾔ', 'ᾕ', 'ᾖ',
                                  'ᾗ', 'ᾘ', 'ᾙ', 'ᾚ', 'ᾛ', 'ᾜ', 'ᾝ', 'ᾞ', 'ᾟ', 'ῂ', 'ῃ', 'ῄ', 'ῆ', 'ῇ', 'Ὴ', 'Ή', 'ῌ', 'ὴ', 'ή',
                                  'ἰ', 'ἱ', 'ἲ', 'ἳ', 'ἴ', 'ἵ', 'ἶ', 'ἷ', 'Ἰ', 'Ἱ', 'Ἲ', 'Ἳ', 'Ἴ', 'Ἵ', 'Ἶ', 'Ἷ', 'ῐ', 'ῑ', 'ῒ', 'ΐ', 'ῖ', 'ῗ', 'Ῐ', 'Ῑ', 'Ὶ', 'Ί', 'ὶ', 'ί',
                                  'ὀ', 'ὁ', 'ὂ', 'ὃ', 'ὄ', 'ὅ', 'Ὀ', 'Ὁ', 'Ὂ', 'Ὃ', 'Ὄ', 'Ὅ', 'ὸ', 'ό',
                                  'ὐ', 'ὑ', 'ὒ', 'ὓ', 'ὔ', 'ὕ', 'ὖ', 'ὗ', 'Ὑ', 'Ὓ', 'Ὕ', 'Ὗ', 'ῠ', 'ῡ', 'ῢ', 'ΰ', 'ῦ', 'ῧ', 'Ῠ', 'Ῡ', 'Ὺ', 'Ύ', 'ὺ', 'ύ',
                                  'ὠ', 'ὡ', 'ὢ', 'ὣ', 'ὤ', 'ὥ', 'ὦ', 'ὧ', 'Ὠ', 'Ὡ', 'Ὢ', 'Ὣ', 'Ὤ', 'Ὥ', 'Ὦ', 'Ὧ', 'ᾠ', 'ᾡ', 'ᾢ', 'ᾣ', 'ᾤ', 'ᾥ', 'ᾦ',
                                  'ᾧ', 'ᾨ', 'ᾩ', 'ᾪ', 'ᾫ', 'ᾬ', 'ᾭ', 'ᾮ', 'ᾯ', 'ῲ', 'ῳ', 'ῴ', 'ῶ', 'ῷ', 'Ὸ', 'Ό', 'Ὼ', 'Ώ', 'ῼ', 'ὼ', 'ώ',
                                  'ῤ', 'ῥ', 'Ῥ' };
                Char[] gkBaseLetters = { 'α', 'ε', 'η', 'ι', 'ο', 'υ', 'ω', 'ρ' };

                int[] gkLetterCounts = { 47, 18, 42, 28, 14, 24, 44, 3 };
                Char[] majiscules = { 'Α', 'Β', 'Γ', 'Δ', 'Ε', 'Ζ', 'Η', 'Θ', 'Ι', 'Κ', 'Λ', 'Μ', 'Ν', 'Ξ', 'Ο', 'Π', 'Ρ', 'Σ', 'Τ', 'Υ', 'Φ', 'Χ', 'Ψ', 'Ω' };
                Char[] miniscules = { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω' };
                Char[] restToBeChanged = { 'Ά', 'Έ', 'Ή', 'Ί', 'Ό', 'Ύ', 'Ώ', 'Ϊ', 'Ϋ', 'ά', 'έ', 'ή', 'ί', 'ΰ', 'ϊ', 'ϋ', 'ό', 'ύ', 'ώ' };
                Char[] restComparable = { 'α', 'ε', 'η', 'ι', 'ο', 'υ', 'ω', 'ι', 'υ', 'α', 'ε', 'η', 'ι', 'ο', 'ι', 'υ', 'ο', 'υ', 'ω' };
                Char[] simpleAlphabet = { 'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'ς', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω' };
                int indexMax, jdx, runningCount = 0;

                indexMax = gkLetterCounts.Length;
                for (jdx = 0; jdx < indexMax; jdx++)
                {
                    for (idx = 0; idx < gkLetterCounts[jdx]; idx++)
                    {
                        gkFlattener.Add(gkLetters[runningCount++], gkBaseLetters[jdx]);
                    }
                }
                indexMax = majiscules.Length;
                for (idx = 0; idx < indexMax; idx++)
                {
                    gkFlattener.Add(majiscules[idx], miniscules[idx]);
                }
                indexMax = restToBeChanged.Length;
                for (idx = 0; idx < indexMax; idx++)
                {
                    gkFlattener.Add(restToBeChanged[idx], restComparable[idx]);
                }
                indexMax = simpleAlphabet.Length;
                for (idx = 0; idx < indexMax; idx++)
                {
                    gkFlattener.Add(simpleAlphabet[idx], simpleAlphabet[idx]);
                }
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 2a: Load New Testament information and data                                                *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   Step a is simply loading book names (and id). All we need from this process is a list of book   *
                 *   names, listed by book code.                                                                     *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                String fileBuffer, fullFileName;
                String[] fileContents = new String[3];
                Char[] splitParams = { '\t' };
                StreamReader srBookNames;
                classBooks currentBook;

                // Firstly, get a simple list of book names - primarily for references
                fullFileName = globalVars.BaseDirectory + globalVars.NtTitles;
                srBookNames = new StreamReader(fullFileName);
                fileBuffer = srBookNames.ReadLine();
                while (fileBuffer != null)
                {
                    noOfNTBooks++;
                    noOfStoredBooks++;
                    currentBook = new classBooks();
                    fileContents = fileBuffer.Split(splitParams);
                    currentBook.CommonName = fileContents[1];
                    currentBook.ShortName = fileContents[2];
                    currentBook.ActualBookNumber = noOfNTBooks;
                    bookNames.Add(noOfStoredBooks, currentBook);
                    fileBuffer = srBookNames.ReadLine();
                }
                srBookNames.Close();
                srBookNames.Dispose();
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 2b:                                                                                        *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   We now need to load the actual text details.  Note that we load this data in two stages:        *
                 *   - stage 1 populates what we have called "raw data" (see below)                                  *
                 *   - stage 2 (later) will refine the available data and match different usages by root             *
                 *                                                                                                   *
                 *   However, we will also need to protect usage by reference and the ability to display the text    *
                 *   for any given chapter.                                                                          *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                int catCode = -1, bookNo, chapNo, verseNo, prevBookNo = 0, prevChapNo = 0, prevVerseNo = 0;
                String fileBuffer, fullFileName;
                String[] lineContents = new String[5];
                Char[] splitParams = { '\t' };
                StreamReader srNTText;
                classRootData rootData;
                classBooks currentBook = null;
                classChapter currentChapter = null;
                classVerse currentVerse = null;

                // Now for the  text data. Let's process and store it
                fullFileName = globalVars.BaseDirectory + globalVars.NtTextFile;
                srNTText = new StreamReader(fullFileName);
                fileBuffer = srNTText.ReadLine();
                while (fileBuffer != null)
                {
                    /**********************************************************************
                     *                                                                    *
                     *  Split the line as follows:                                        *
                     *   Field                    Contents                                *
                     *     0      A six digit book/Chapter/Verse reference                *
                     *     1      Two character category reference                        *
                     *     2      Eight character parse sequence                          *
                     *     3      The word as displayed at that verse                     *
                     *     4      The root of the verb                                    *
                     *                                                                    *
                     **********************************************************************/
                    lineContents = fileBuffer.Split(splitParams);
                    for (idx = 0; idx < noOfCategories; idx++)
                    {
                        if (String.Compare(categoryCodes[idx], lineContents[1]) == 0)
                        {
                            catCode = idx;
                            break;
                        }
                    }
                    bookNo = Convert.ToInt32(lineContents[0].Substring(0, 2));
                    chapNo = Convert.ToInt32(lineContents[0].Substring(2, 2));
                    verseNo = Convert.ToInt32(lineContents[0].Substring(4, 2));
                    rootData = processTextData(bookNo, chapNo, verseNo, catCode, lineContents[4]);
                    rootData.addParsedText(processRawData.getParseInfo(lineContents[2]), processRawData.cleanTextWord(lineContents[3], lineContents[4][0]), bookNo, chapNo, verseNo, "", "", true);
                    // Now create the hierarchy to store the text
                    if( bookNo != prevBookNo)
                    {
                        bookNames.TryGetValue(bookNo, out currentBook);
                        prevChapNo = 0;
                        prevBookNo = bookNo;
                    }
                    if( prevChapNo != chapNo)
                    {
                        currentChapter = currentBook.addChapter(chapNo.ToString());
                        prevChapNo = chapNo;
                        prevVerseNo = 0;
                    }
                    if( prevVerseNo != verseNo)
                    {
                        currentVerse = currentChapter.addVerse(verseNo.ToString());
                        prevVerseNo = verseNo;
                    }
                    currentVerse.addWord(lineContents[3], "", "", lineContents[5]);
                    fileBuffer = srNTText.ReadLine();
                }
                srNTText.Close();
                srNTText.Dispose();
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 3a: data load for LXX data                                                                 *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   Step a will handle information about Septuagint book names and the files containing text data.  *
                 *   It is more complex than the equivalent step for NT data because the LXX text is held in         *
                 *   files (one for each book).                                                                      *
                 *                                                                                                   *
                 *   Note also that LXX chapters and verses are not always logically structured and we need to cope  *
                 *   with the possibility of:                                                                        *
                 *                                                                                                   *
                 *     - chapters out of sequence;                                                                   *
                 *     - pre-chapter verses (indexed as verse zero in our data;                                      *
                 *     - verses identified as e.g. 12a, 12b and so on.                                               *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                String fileBuffer, fullFileName;
                String[] fileContents;
                Char[] splitParams = { '\t' };
                StreamReader srBookNames;
                classBooks currentTitleEntry;

                // Firstly, get a simple list of book names - primarily for references
                fullFileName = globalVars.BaseDirectory + globalVars.LxxTitles;
                srBookNames = new StreamReader(fullFileName);
                fileBuffer = srBookNames.ReadLine();
                while (fileBuffer != null)
                {
                    if (fileBuffer[0] != ';')
                    {
                        noOfLxxBooks++;
                        noOfStoredBooks++;
                        fileContents = fileBuffer.Split(splitParams);
                        currentTitleEntry = new classBooks();
                        currentTitleEntry.ShortName = fileContents[0];
                        currentTitleEntry.CommonName = fileContents[1];
                        currentTitleEntry.LxxName = fileContents[2];
                        currentTitleEntry.FileName = fileContents[3];
                        currentTitleEntry.ActualBookNumber = noOfLxxBooks;
                        bookNames.Add(noOfStoredBooks, currentTitleEntry);
                    }
                    fileBuffer = srBookNames.ReadLine();
                }
                srBookNames.Close();
                srBookNames.Dispose();
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            {
                /*---------------------------------------------------------------------------------------------------*
                 *                                                                                                   *
                 *   Step 3b:                                                                                        *
                 *   =======                                                                                         *
                 *                                                                                                   *
                 *   Now load the relevant LXX text data.  This mimics the parallel process for the NT but it is not *
                 *   required for all the processes in which the NT data participates.                               *
                 *                                                                                                   *
                 *---------------------------------------------------------------------------------------------------*/
                int bdx, catCode = -1, bookNo, chapterNo, verseNo = 0;
                String fileBuffer, fullFileName, prevChapRef, prevVerseRef = "?";
                Char firstCodeChar;
                String[] lineContents = new String[5];
                Char[] splitParams = { '\t' };
                StreamReader srLxxText;
                classBooks currentBook = null;
                classChapter currentChapter = null;
                classVerse currentVerse = null;
                classRootData rootData;

                // Now for the  text data. Let's process and store it
                fullFileName = globalVars.BaseDirectory + globalVars.LxxTextDirectory;
                for (bdx = noOfNTBooks + 1; bdx <= noOfStoredBooks; bdx++)
                {
                    chapterNo = 0;
                    prevChapRef = "?";
                    bookNames.TryGetValue(bdx, out currentBook);
                    srLxxText = new StreamReader(fullFileName + @"\" + currentBook.FileName);
                    fileBuffer = srLxxText.ReadLine();
                    while (fileBuffer != null)
                    {
                        /**************************************************************************************
                         *                                                                                    *
                         *  Split the line as follows:                                                        *
                         *                                                                                    *
                         *   Field                    Contents                                                *
                         *   -----  ------------------------------------------------------------------------  *
                         *     1	Chapter number                                                            *
                         *     2	Verse number (note: may = 0 or 12b)                                       *
                         *     3	Initial Parse code                                                        *
                         *     4	Detailed Parse code                                                       *
                         *     5	A unique grammatical identifier                                           *
                         *     6	Word as it is to be displayed in the text                                 *
                         *     7	Word a) all lower case, b) stripped of accents and related furniture      *
                         *     8	Word, as in field 7 but also with breathings and iota subscripts removed  *
                         *     9	Immediate root of word in field 6                                         *
                         *     10	Pre-word characters                                                       *
                         *     11	Post-word non-punctuation characters                                      *
                         *     12	Punctuation                                                               *
                         *     13	Transliterated version of field 6                                         *
                         *     14+	Transliteration of root (with prefixed prepositions separated             *
                         *                                                                                    *
                         *  However, fields 1 and 2 are as supplied by the source file.  In addition, we will *
                         *  create a simple, sequential index for chapters and verses.  This will allow for:  *
                         *  a) out-of-sequence chapters (in a few books, there are gaps and, even, chapters   *
                         *     transposed;                                                                    *
                         *  b) verses that include text as well as digits (e.g. 19b);                         *
                         *  c) unnumbered verses (in our data, given the index 0)                             *
                         *                                                                                    *
                         **************************************************************************************/
                        lineContents = fileBuffer.Split(splitParams);
                        if (String.Compare(lineContents[0], prevChapRef) != 0)
                        {
                            chapterNo++;
                            currentChapter = currentBook.addChapter(lineContents[0]);
                            prevChapRef = lineContents[0];
                            verseNo = 0;
                            prevVerseRef = "?";
                        }
                        if (String.Compare(lineContents[1], prevVerseRef) != 0)
                        {
                            verseNo++;
                            currentVerse = currentChapter.addVerse(lineContents[1]);
                            prevVerseRef = lineContents[1];
                        }
                        currentVerse.addWord(lineContents[5], lineContents[9], lineContents[10], lineContents[11]);
                        firstCodeChar = lineContents[2][0];
                        catCode = -1;
                        for (idx = 0; idx < noOfCategories; idx++)
                        {
                            if (firstCodeChar == 'R')
                            {
                                if (String.Compare(categoryCodes[idx], lineContents[2]) == 0)
                                {
                                    catCode = idx;
                                    break;
                                }
                            }
                            else
                            {
                                if (categoryCodes[idx][0] == firstCodeChar)
                                {
                                    catCode = idx;
                                    break;
                                }
                            }
                        }
                        if( catCode == -1 )
                        {
                            fileBuffer = srLxxText.ReadLine();
                            continue;
                        }
                        /*------------------------------------------------------------------------------*
                         *                                                                              *
                         *  Note that noOfCategories is the number of _NT_ categories.  The if clause   *
                         *    will ignore any LXX entries for ὅστις or indeclinable numbers.  As a      *
                         *    result, the grammatical structures of NT and LXX data will be the same.   *
                         *                                                                              *
                         *------------------------------------------------------------------------------*/
                        rootData = processTextData(bdx, chapterNo, verseNo, catCode, lineContents[8]);
                        rootData.addParsedText(processRawData.getLxxParseInfo(catCode, lineContents[3]), processRawData.cleanTextWord(lineContents[5], lineContents[8][0]), 
                            bdx, chapterNo, verseNo, lineContents[0], lineContents[1], false);
                        fileBuffer = srLxxText.ReadLine();
                    }
                    srLxxText.Close();
                    srLxxText.Dispose();
                    progressForm.Invoke(new performProgressAdvance(updateProgress));
                }
            }

            globalVars.BookList = bookNames;

            {
                classRootData rootData;

                for (idx = 1; idx <= noOfNtRoots; idx++)
                {
                    if (rootDataStore.ContainsKey(idx))
                    {
                        rootDataStore.TryGetValue(idx, out rootData);
                        if ((rootData.CatCode == 0) && ( rootData.IsFoundInNT))
                        {
                            lbRootList.Invoke(new performListBoxAddition(updateListbox), rootData.RootWord);
                        }
                    }
                }
                progressForm.Invoke(new performProgressAdvance(updateProgress));
            }

            this.Invoke(new postInitialisationCleanup(doPostInitialisationCleanup));
        }

        private classRootData processTextData( int bookNo, int chapNo, int verseNo, int catCode, String root)
        {
            int nRootCodeAlt;
            String catString, rawKey;
            classRootData rootData;

            /*...........................................................*
             *                                                           *
             *  Construct rawKey: a unique key for a root.               *
             *                                                           *
             *  Effectively, the key is root + verb | noun | etc.        *
             *                                                           *
             *...........................................................*/
            if (catCode < 10)
            {
                catString = "0" + catCode.ToString();
            }
            else
            {
                catString = catCode.ToString();
            }
            rawKey = root + catString;
            /*...........................................................*
             *                                                           *
             *  The point now is to get to a "record" of a unique root   *
             *    word.  Either the word already has some data (first    *
             *    clause) or we haven't previously encountered it (2nd). *
             *                                                           *
             *...........................................................*/
            if (rootLookup.ContainsKey(rawKey))
            {
                // The root word has already been stored for that category
                rootLookup.TryGetValue(rawKey, out nRootCodeAlt);
                rootDataStore.TryGetValue(nRootCodeAlt, out rootData);
            }
            else
            {
                nRootCount++;
                noOfNtRoots++;
                rootLookup.Add(rawKey, nRootCount);
                rootData = new classRootData();
                rootData.ProcessRawData = processRawData;
                rootData.RootWord = root;
//                if (String.Compare(root, "ἀβουλέω") == 0) MessageBox.Show(nRootCount.ToString(), "nRootCount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rootData.CatCode = catCode;
                rootDataStore.Add(nRootCount, rootData);
            }
            /*----------------------------------------------------------------------------------------------*
             *                                                                                              *
             *  Add to the "record" of the unique root:                                                     *
             *                                                                                              *
             *  a) the reference of this occurrence (book, chapter and verse code);                         *
             *  b) the "parseCode" for the word (= a unique value for its grammatical character);           *
             *  c) the actual word used                                                                     *
             *                                                                                              *
             *  So, for example, if the root referenced by rootData is the verb λύω, this particulaar       *
             *    occurrence might be, e.g., λύεις.  The next one might be λύσαν.  So, we have access to    *
             *    both occurrences.                                                                         *
             *                                                                                              *
             *----------------------------------------------------------------------------------------------*/
            rootData.ProcessRawData = processRawData;
            return rootData;
        }
        void currentRButton_CheckedChanged(object sender, EventArgs e)
        {
            /***********************************************************************
             *                                                                     *
             *                   currentRButton_CheckedChanged                     *
             *                   =============================                     *
             *                                                                     *
             *  Manages the transition from one grammatical category to another.   *
             *                                                                     *
             ***********************************************************************/

            int idx, tagCode = 0, resVal, resValTemp;
            String actualRootWord;
            classRootData rootData;
            SortedDictionary<int, classParsedItem> parseListCopy;

            selectedButton = (RadioButton)sender;
            if (!selectedButton.Checked) return;
            lbRootList.Items.Clear();
            categoryCode = Convert.ToInt32(selectedButton.Tag);
            for (idx = 1; idx <= noOfNtRoots; idx++)
            {
                if (rootDataStore.ContainsKey(idx))
                {
                    rootDataStore.TryGetValue(idx, out rootData);
                    if ((rootData.CatCode == categoryCode) && ( rootData.IsFoundInNT))
                    {
                        lbRootList.Items.Add(rootData.RootWord);
                    }
                }
            }
            clearConstraints();
            if (categoryCode == 1)  // I.e. if we are handling verbs
            {
                this.Width = 785;
            }
            else
            {
                this.Width = 389;
            }
        }

        void keyButton_Click(object sender, EventArgs e)
        {
            int tagValue;
            Button currentButton;

            currentButton = (Button)sender;
            tagValue = Convert.ToInt32(currentButton.Tag);
            switch (tagValue)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26: processAddedChar(currentButton); break;


                case 5: removeLastChar(); break;
                case 11: clearConstraints(); break;
                case 27:; break;
            }
        }

        private void clearConstraints()
        {
            int idx;
            classRootData rootData;

            if (selectedButton == null) return;
            if (!selectedButton.Checked) return;
            keyEntryWorkspace = "";
            lbRootList.Items.Clear();
            categoryCode = Convert.ToInt32(selectedButton.Tag);
            for (idx = 1; idx <= noOfNtRoots; idx++)
            {
                if (rootDataStore.ContainsKey(idx))
                {
                    rootDataStore.TryGetValue(idx, out rootData);
                    if ((rootData.CatCode == categoryCode) && ( rootData.IsFoundInNT))
                    {
                        lbRootList.Items.Add(rootData.RootWord);
                    }
                }
            }
            if (lbRootList.Items.Count > 0) lbRootList.SelectedIndex = 0;
            labTextEnteredMsg.Text = "None";
        }

        private void processAddedChar(Button pressedButton)
        {
            int removalPstn, idx, limit;
            Char targetChar, compareChar, inspectedChar;
            String inspectedEntry;

            removalPstn = keyEntryWorkspace.Length;
            targetChar = pressedButton.Text[0];
            limit = lbRootList.Items.Count;
            for (idx = limit - 1; idx >= 0; idx--)
            {
                inspectedEntry = lbRootList.Items[idx].ToString();
                if (inspectedEntry.Length <= removalPstn)
                {
                    lbRootList.Items.RemoveAt(idx);
                    continue;
                }
                inspectedChar = inspectedEntry[removalPstn];
                if (gkFlattener.ContainsKey(inspectedChar))
                {
                    gkFlattener.TryGetValue(inspectedChar, out compareChar);
                    if (compareChar != targetChar)
                    {
                        lbRootList.Items.RemoveAt(idx);
                        continue;
                    }
                }
                else
                {
                    lbRootList.Items.RemoveAt(idx);
                    continue;
                }
            }
            keyEntryWorkspace += pressedButton.Text;
            labTextEnteredMsg.Text = keyEntryWorkspace;
        }

        private void removeLastChar()
        {
            bool isNoMatch;
            int removalPstn, idx, jdx, limit;
            Char targetChar, compareChar;
            String inspectedEntry;
            classRootData rootData;

            if (!selectedButton.Checked) return;
            removalPstn = keyEntryWorkspace.Length;
            if (removalPstn == 0) return;
            if (removalPstn == 1)
            {
                clearConstraints();
                return;
            }
            keyEntryWorkspace = keyEntryWorkspace.Substring(0, keyEntryWorkspace.Length - 1);
            removalPstn--;
            lbRootList.Items.Clear();
            categoryCode = Convert.ToInt32(selectedButton.Tag);
            for (idx = 1; idx <= noOfNtRoots; idx++)
            {
                isNoMatch = false;
                if (rootDataStore.ContainsKey(idx))
                {
                    rootDataStore.TryGetValue(idx, out rootData);
                    if ((rootData.CatCode == categoryCode) && ( rootData.IsFoundInNT))
                    {
                        inspectedEntry = rootData.RootWord;
                        limit = inspectedEntry.Length;
                        if (limit < removalPstn) continue;
                        for (jdx = 0; jdx < removalPstn; jdx++)
                        {
                            targetChar = inspectedEntry[jdx];
                            if (gkFlattener.ContainsKey(targetChar))
                            {
                                gkFlattener.TryGetValue(targetChar, out compareChar);
                                if (compareChar != keyEntryWorkspace[jdx])
                                {
                                    isNoMatch = true;
                                    break;
                                }
                            }
                            else
                            {
                                isNoMatch = true;
                                break;
                            }
                        }
                        if (isNoMatch) continue;
                        lbRootList.Items.Add(inspectedEntry);
                    }
                }
            }
            if (lbRootList.Items.Count > 0) lbRootList.SelectedIndex = 0;
            labTextEnteredMsg.Text = keyEntryWorkspace;
        }

        private void btnTenseSelect_Click(object sender, EventArgs e)
        {
            bool compResult;
            int idx, noOfTenses;

            compResult = String.Compare(btnTenseSelect.Text, "Select All") == 0;
            noOfTenses = tenseNames.Length;
            for (idx = 0; idx < noOfTenses; idx++)
            {
                chkTenses[idx].Checked = compResult;
            }
            if (compResult)
            {
                btnTenseSelect.Text = "Unselect All";
            }
            else
            {
                btnTenseSelect.Text = "Select All";
            }
        }

        private void btnSelectMood_Click(object sender, EventArgs e)
        {
            bool compResult;
            int idx, noOfMoods;

            if (rbtnMain.Checked)
            {
                compResult = String.Compare(btnSelectMood.Text, "Select All") == 0;
                noOfMoods = moodNames.Length;
                for (idx = 0; idx < noOfMoods; idx++)
                {
                    chkMoods[idx].Checked = compResult;
                }
                if (compResult)
                {
                    btnSelectMood.Text = "Unselect All";
                }
                else
                {
                    btnSelectMood.Text = "Select All";
                }
            }
        }

        private void lbRootList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            perform_Selection();
        }

        private void Conjugate_Click(object sender, EventArgs e)
        {
            perform_Selection();
        }

        private void perform_Selection()
        {
            int rootCode;
            String selectedEntry, classKey, catCodeString;

            classRootData currentRootData;

            if (categoryCode < 10)
            {
                catCodeString = "0" + categoryCode.ToString();
            }
            else
            {
                catCodeString = categoryCode.ToString();
            }
            selectedEntry = lbRootList.SelectedItem.ToString();
            classKey = selectedEntry + catCodeString;
            if (rootLookup.ContainsKey(classKey))
            {
                rootLookup.TryGetValue(classKey, out rootCode);
                if (rootDataStore.ContainsKey(rootCode))
                {
                    rootDataStore.TryGetValue(rootCode, out currentRootData);
                    switch (categoryCode)
                    {
                        case 0: processNouns(currentRootData, selectedEntry); break;
                        case 1: processVerbs(currentRootData, selectedEntry); break;
                        case 2: processAdjectives(currentRootData, selectedEntry); break;
                        case 3: processSimple(currentRootData, selectedEntry); break;
                        case 4: processSimple(currentRootData, selectedEntry); break;
                        case 5: processAdjectives(currentRootData, selectedEntry); break;
                        case 6: processAdjectives(currentRootData, selectedEntry); break;
                        case 7: processAdjectives(currentRootData, selectedEntry); break;
                        case 8: processAdjectives(currentRootData, selectedEntry); break;
                        case 9: processAdjectives(currentRootData, selectedEntry); break;
                        case 10: processSimple(currentRootData, selectedEntry); break;
                        case 11: processSimple(currentRootData, selectedEntry); break;
                        case 12: processSimple(currentRootData, selectedEntry); break;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to find data matching the " + fullCategoryNames[categoryCode] + ": " + selectedEntry, "Unforeseen Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Unable to find data matching the " + fullCategoryNames[categoryCode] + ": " + selectedEntry, "Unforeseen Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void processNouns(classRootData currentEntry, String selectedEntry)
        {
            /*******************************************************************************************
             *                                                                                         *
             *                                  processNouns                                           *
             *                                  ============                                           *
             *                                                                                         *
             *  Parameters:                                                                            *
             *  ==========                                                                             *
             *  currentEntry:  The rootData instance associated with the selectedEntry                 *
             *  selectedEntry: The text of the entry (noun root) currently selected                    *
             *                                                                                         *
             *                                                                                         *
             *  Process:                                                                               *
             *  =======                                                                                *
             *                                                                                         *
             *  The aim of this method is to identify all the forms of the word found in the NT or     *
             *  (optionally) the LXX and present them in a suitable table.  The table is actually      *
             *  managed by frmUsage.  Here, we organise the information to be displayed in the 3-d     *
             *  array, wordDetails.  The dimensions of the array are as follows:                       *
             *                                                                                         *
             *      Dimension                             Purpose                                      *
             *          0           Not used in Nouns                                                  *
             *          1           Case                                                               *
             *          2           Singular or plural (LXX Dual is treated as plural)                 *
             *                                                                                         *
             *  wordRefs is also a 3-d array, keyed in much the same way as wordDetails.  It stores    *
             *  the classReferenceDetail instance for the specific row and column, if it exists.       *
             *                                                                                         *
             *******************************************************************************************/

            int parseCode, codeCalculation, colCode, noOfRows = 6, calcCellStatus;
            String wordOut = "";
            int[,,] cellStatus;
            String[,,] wordDetails;
            SortedList<int, classReference>[,,] wordRefs;
            SortedList<int, classParsedItem> currentEntryDetails;
            SortedList<int, classReference> refStore;
            frmUsage generalUsage;

            // wordDetails provides the text for the final table
            wordDetails = new String[1, 6, 3];
            cellStatus = new int[1, 6, 3];
            wordRefs = new SortedList<int, classReference>[1, 6, 3];
            wordDetails[0, 0, 1] = "Singular";
            wordDetails[0, 0, 2] = "Plural";
            generalUsage = new frmUsage(false, globalVars);
            generalUsage.setCategoryName(fullCategoryNames[categoryCode]);
            generalUsage.setWordItself(selectedEntry);
            currentEntryDetails = currentEntry.getParseList();
            wordDetails[0, 1, 0] = "Nominative";
            wordDetails[0, 2, 0] = "Vocative";
            wordDetails[0, 3, 0] = "Accusative";
            wordDetails[0, 4, 0] = "Genitive";
            wordDetails[0, 5, 0] = "Dative";
            foreach (KeyValuePair<int, classParsedItem> pMember in currentEntryDetails)
            {
                wordOut = pMember.Value.getWords();
                parseCode = pMember.Value.ParseCode;
                codeCalculation = (parseCode % 100000) / 10000;
                colCode = parseCode % 10;
                calcCellStatus = 0;
                if (pMember.Value.IsInNT) calcCellStatus = 1;
                if (pMember.Value.IsInLXX) calcCellStatus += 2;
                if (colCode == 7)
                {
                    wordDetails[0, codeCalculation, 1] = wordOut;
                    cellStatus[0, codeCalculation, 1] = calcCellStatus;
                    refStore = pMember.Value.ReferenceList;
                    wordRefs[0, codeCalculation, 1] = refStore;  // New line
                }
                if (colCode == 8)
                {
                    wordDetails[0, codeCalculation, 2] = wordOut;
                    cellStatus[0, codeCalculation, 2] = calcCellStatus;
                    refStore = pMember.Value.ReferenceList;
                    wordRefs[0, codeCalculation, 2] = refStore;  // New line
                }
            }
            generalUsage.BookList = bookNames;
            generalUsage.displayTable(wordDetails, cellStatus, 3, noOfRows, wordRefs);
            generalUsage.Show();
        }

        private void processVerbs(classRootData currentEntry, String selectedEntry)
        {
            /*************************************************************************************************************
             *                                                                                                           *
             *                                              processVerbs                                                 *
             *                                              ============                                                 *
             *                                                                                                           *
             *  The user has selected a verb from the list of NT words.  This method will prepare the word for display   *
             *  and then pass the relevant variables to frmUsage for actual display.                                     *
             *                                                                                                           *
             *  A table has three columns:                                                                               *
             *    0  Descriptive text                                                                                    *
             *    1  Singular                                                                                            *
             *    2  Plural                                                                                              *
             *                                                                                                           *
             *  A table has a variable number of rows.  For nouns this is simply 6: one for headings and then one for    *
             *  each case.  Verbs, however, will be more complex and will vary, depending on the options of tense and    *
             *  mood selected.                                                                                           *
             *                                                                                                           *
             *  An additional complication is that verbs will also have three "pages" of tables: one for Active, one for *
             *  Middle and one for Passive.                                                                              *
             *                                                                                                           *
             *  To accommodate this complexity, this method will handle a number of 3-dimensional arrays.  In each case  *
             *    index 0 = the "page" or tab                                                                            *
             *    index 1 = the row of the relevant table                                                                *
             *    index 2 = the column                                                                                   *
             *                                                                                                           *
             *  In all cases except the verb, the only value of index 0 will be 0 (i.e. they will, in reality, only be   *
             *  two dimensions).                                                                                         *
             *                                                                                                           *
             *  These complex arrays are:                                                                                *
             *                                                                                                           *
             *  wordDetails  This array stores the actual text to be displayed.  This includes both the side headings    *
             *               and the various individual forms of the word selected;                                      *
             *  wordRefs     This array stores a list of references (i.e. an instance of classReferences) specific to    *
             *               each form of the word associated with the row and column (e.g. if we are looking at the     *
             *               accusative plural of a given word, this will be a list of all references where the          *
             *               accusative plural is used);                                                                 *
             *  cellStatus   this array stores a code for each wordDetail (identified by the same row and column) that   *
             *               indicates whether the word occurs only in the NT, only in the LXX or occurs in both.        *
             *                                                                                                           *
             *************************************************************************************************************/

            bool isUsed = false, isNewTense = false;
            int noOfRows, noOfTenses, noOfMoods, runningTotal, tenseIdx, moodIdx, voiceIdx, idx, entryCode, caseIdx, genderIdx, selectedMoods = 0, selectedTenses = 0, calcCellStatus;
            String tenseDesc, lineDesc;
            int[,,] cellStatus;
            String[,,] wordDetails;
            SortedList<int, classReference>[,,] wordRefs;

            SortedList<int, classParsedItem> currentEntryDetails;
            classParsedItem currentDetails;
            frmUsage generalUsage;

            // Before we start, let's check for completely unchecked boxes - if so, they are treated as all checked
            noOfTenses = tenseNames.Length;
            for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
            {
                if (chkTenses[tenseIdx].Checked) selectedTenses++;
            }
            if (selectedTenses == 0)
            {
                for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
                {
                    chkTenses[tenseIdx].Checked = true;
                }
            }
            if (rbtnMain.Checked)
            {
                // First, let's simply count how many rows we expect
                noOfRows = 1;  // Because we include the "titles" row
                noOfMoods = moodNames.Length;
                // Now we check for completely unchecked boxes for mood as we did for tense
                for (moodIdx = 0; moodIdx < noOfMoods; moodIdx++)
                {
                    if (chkMoods[moodIdx].Checked) selectedMoods++;
                }
                if (selectedMoods == 0)
                {
                    for (moodIdx = 0; moodIdx < noOfMoods; moodIdx++)
                    {
                        chkMoods[moodIdx].Checked = true;
                    }
                }
                for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
                {
                    if (chkTenses[tenseIdx].Checked)
                    {
                        if (isUsed)
                        {
                            noOfRows++;
                            isUsed = false;
                        }
                        runningTotal = 0;
                        for (moodIdx = 0; moodIdx < noOfMoods; moodIdx++)
                        {
                            if (chkMoods[moodIdx].Checked)
                            {
                                if (moodIdx == noOfMoods - 1)
                                {
                                    runningTotal++;
                                }
                                else
                                {
                                    runningTotal += 3;
                                }
                                isUsed = true;
                            }
                        }
                        if (!isUsed) return;  // No Moods are selected, so no data
                        noOfRows += runningTotal;
                    }
                }
                // Now we know how many rows to expect, we can define the array to receive data
                 wordDetails = new String[3, noOfRows + 1, 3];
                cellStatus = new int[3, noOfRows + 1, 3];
                wordRefs = new SortedList<int, classReference>[3, noOfRows + 1, 3];
                wordDetails[0, 0, 1] = "Singular";
                wordDetails[0, 0, 2] = "Plural";
                wordDetails[1, 0, 1] = "Singular";
                wordDetails[1, 0, 2] = "Plural";
                wordDetails[2, 0, 1] = "Singular";
                wordDetails[2, 0, 2] = "Plural";
                currentEntryDetails = currentEntry.getParseList();
                /*--------------------------------------------------------------------------------------*
                 *                                                                                      *
                 *  For each entry in currentEntryDetails, the entryCode comprises the following:       *
                 *    Pstn  Values                                                                      *
                 *      0    1 - 6   Person (1-3, singular, 4-6 plural -- if Mood is not infinitve), or *
                 *           0       For infinitive                                                     *
                 *      1    1 - 6   Tense  (i.e. litterally, 10 - 60)                                  *
                 *      2    1 - 6   Mood   (i.e. 100 - 600)                                            *
                 *      3    1 - 3   Voice  (i.e. 1000 - 3000)                                          *
                 *      4    0       For all moods except Participle, which has values                  *
                 *           1 - 5                                                                      *
                 *      5    0       For all moods except Participle, which has values                  *
                 *           1 - 3                                                                      *
                 *                                                                                      *
                 *--------------------------------------------------------------------------------------*/
            runningTotal = 0;  // Because we include the "titles" row
                isUsed = false;
                noOfTenses = tenseNames.Length;
                for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
                {
                    if (chkTenses[tenseIdx].Checked)
                    {
                        if (isUsed)
                        {
                            runningTotal++;
                            isUsed = false;
                        }
                        tenseDesc = tenseNames[tenseIdx];
                        noOfMoods = moodNames.Length;
                        for (moodIdx = 0; moodIdx < noOfMoods; moodIdx++)
                        {
                            if (chkMoods[moodIdx].Checked)
                            {
                                lineDesc = tenseDesc + " " + moodNames[moodIdx];
                                if (moodIdx == noOfMoods - 1)
                                {
                                    runningTotal++;
                                    for (voiceIdx = 0; voiceIdx < 3; voiceIdx++)
                                    {
                                        wordDetails[voiceIdx, runningTotal, 0] = lineDesc;
                                        entryCode = (voiceIdx + 1) * 1000 + (moodIdx + 1) * 100 + (tenseIdx + 1) * 10;
                                        calcCellStatus = 0;
                                        if (currentEntryDetails.ContainsKey(entryCode))
                                        {
                                            currentEntryDetails.TryGetValue(entryCode, out currentDetails);
                                            wordDetails[voiceIdx, runningTotal, 1] = currentDetails.getWords();
                                            if (currentDetails.IsInNT) calcCellStatus = 1;
                                            if (currentDetails.IsInLXX) calcCellStatus += 2;
                                            cellStatus[voiceIdx, runningTotal, 1] = calcCellStatus;
                                            wordRefs[voiceIdx, runningTotal, 1] = currentDetails.ReferenceList;
                                        }
                                    }
                                }
                                else
                                {
                                    for (idx = 0; idx < 3; idx++)
                                    {
                                        runningTotal++;
                                        for (voiceIdx = 0; voiceIdx < 3; voiceIdx++)
                                        {
                                            switch (idx)
                                            {
                                                case 0: wordDetails[voiceIdx, runningTotal, 0] = lineDesc + " 1st person"; break;
                                                case 1: wordDetails[voiceIdx, runningTotal, 0] = insertSpaces(lineDesc.Length) + " 2nd person"; break;
                                                case 2: wordDetails[voiceIdx, runningTotal, 0] = insertSpaces(lineDesc.Length) + " 3rd person"; break;
                                            }
                                            entryCode = (voiceIdx + 1) * 1000 + (moodIdx + 1) * 100 + (tenseIdx + 1) * 10 + (idx + 1);
                                            calcCellStatus = 0;
                                            if (currentEntryDetails.ContainsKey(entryCode))
                                            {
                                                currentEntryDetails.TryGetValue(entryCode, out currentDetails);
                                                wordDetails[voiceIdx, runningTotal, 1] = currentDetails.getWords();
                                                if (currentDetails.IsInNT) calcCellStatus = 1;
                                                if (currentDetails.IsInLXX) calcCellStatus += 2;
                                                cellStatus[voiceIdx, runningTotal, 1] = calcCellStatus;
                                                wordRefs[voiceIdx, runningTotal, 1] = currentDetails.ReferenceList;
                                            }
                                            calcCellStatus = 0;
                                            if (currentEntryDetails.ContainsKey(entryCode + 3))
                                            {
                                                currentEntryDetails.TryGetValue(entryCode + 3, out currentDetails);
                                                wordDetails[voiceIdx, runningTotal, 2] = currentDetails.getWords();
                                                if (currentDetails.IsInNT) calcCellStatus = 1;
                                                if (currentDetails.IsInLXX) calcCellStatus += 2;
                                                cellStatus[voiceIdx, runningTotal, 2] = calcCellStatus;
                                                wordRefs[voiceIdx, runningTotal, 2] = currentDetails.ReferenceList;
                                            }
                                        }
                                    }
                                }
                                isUsed = true;
                            }
                        }
                    }
                }
                generalUsage = new frmUsage(true, globalVars);
                generalUsage.setCategoryName(fullCategoryNames[categoryCode]);
                generalUsage.setWordItself(selectedEntry);
                generalUsage.BookList = bookNames;
                generalUsage.displayTable(wordDetails, cellStatus, 3, noOfRows, wordRefs);
                generalUsage.Show();
            }
            else  // Participles
            {
                // First, let's simply count how many rows we expect
                noOfRows = 1;  // Because we include the "titles" row
                for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
                {
                    if (isNewTense) noOfRows++;
                    noOfRows += 17;
                    isNewTense = true;
                }
                if (!isNewTense) return;  // No Moods are selected, so no data

                // Now we know how many rows to expect, we can define the array to receive data
                wordDetails = new String[3, noOfRows + 1, 3];
                cellStatus = new int[3, noOfRows + 1, 3];
                wordRefs = new SortedList<int, classReference>[3, noOfRows + 1, 3];
                wordDetails[0, 0, 1] = "Singular";
                wordDetails[0, 0, 2] = "Plural";
                wordDetails[1, 0, 1] = "Singular";
                wordDetails[1, 0, 2] = "Plural";
                wordDetails[2, 0, 1] = "Singular";
                wordDetails[2, 0, 2] = "Plural";
                currentEntryDetails = currentEntry.getParseList();
                runningTotal = 0;  // Because we include the "titles" row
                isUsed = false;
                isNewTense = false;
                noOfTenses = tenseNames.Length;
                for (tenseIdx = 0; tenseIdx < noOfTenses; tenseIdx++)
                {
                    if (chkTenses[tenseIdx].Checked)
                    {
                        if (isNewTense)
                        {
                            runningTotal++;
                        }
                        wordDetails[0, runningTotal + 1, 0] = tenseNames[tenseIdx] + " Masculine Nominative";
                        wordDetails[0, runningTotal + 2, 0] = "          Vocative";
                        wordDetails[0, runningTotal + 3, 0] = "          Accusative";
                        wordDetails[0, runningTotal + 4, 0] = "          Genitive";
                        wordDetails[0, runningTotal + 5, 0] = "          Dative";
                        wordDetails[0, runningTotal + 7, 0] = "    Neuter Nominative";
                        wordDetails[0, runningTotal + 8, 0] = "           Vocative";
                        wordDetails[0, runningTotal + 9, 0] = "           Accusative";
                        wordDetails[0, runningTotal + 10, 0] = "          Genitive";
                        wordDetails[0, runningTotal + 11, 0] = "          Dative";
                        wordDetails[0, runningTotal + 13, 0] = "Feminine  Nominative";
                        wordDetails[0, runningTotal + 14, 0] = "          Vocative";
                        wordDetails[0, runningTotal + 15, 0] = "          Accusative";
                        wordDetails[0, runningTotal + 16, 0] = "          Genitive";
                        wordDetails[0, runningTotal + 17, 0] = "          Dative";

                        wordDetails[1, runningTotal + 1, 0] = tenseNames[tenseIdx] + " Masculine Nominative";
                        wordDetails[1, runningTotal + 2, 0] = "          Vocative";
                        wordDetails[1, runningTotal + 3, 0] = "          Accusative";
                        wordDetails[1, runningTotal + 4, 0] = "          Genitive";
                        wordDetails[1, runningTotal + 5, 0] = "          Dative";
                        wordDetails[1, runningTotal + 7, 0] = "    Neuter Nominative";
                        wordDetails[1, runningTotal + 8, 0] = "           Vocative";
                        wordDetails[1, runningTotal + 9, 0] = "           Accusative";
                        wordDetails[1, runningTotal + 10, 0] = "          Genitive";
                        wordDetails[1, runningTotal + 11, 0] = "          Dative";
                        wordDetails[1, runningTotal + 13, 0] = "Feminine  Nominative";
                        wordDetails[1, runningTotal + 14, 0] = "          Vocative";
                        wordDetails[1, runningTotal + 15, 0] = "          Accusative";
                        wordDetails[1, runningTotal + 16, 0] = "          Genitive";
                        wordDetails[1, runningTotal + 17, 0] = "          Dative";

                        wordDetails[2, runningTotal + 1, 0] = tenseNames[tenseIdx] + " Masculine Nominative";
                        wordDetails[2, runningTotal + 2, 0] = "          Vocative";
                        wordDetails[2, runningTotal + 3, 0] = "          Accusative";
                        wordDetails[2, runningTotal + 4, 0] = "          Genitive";
                        wordDetails[2, runningTotal + 5, 0] = "          Dative";
                        wordDetails[2, runningTotal + 7, 0] = "    Neuter Nominative";
                        wordDetails[2, runningTotal + 8, 0] = "           Vocative";
                        wordDetails[2, runningTotal + 9, 0] = "           Accusative";
                        wordDetails[2, runningTotal + 10, 0] = "          Genitive";
                        wordDetails[2, runningTotal + 11, 0] = "          Dative";
                        wordDetails[2, runningTotal + 13, 0] = "Feminine  Nominative";
                        wordDetails[2, runningTotal + 14, 0] = "          Vocative";
                        wordDetails[2, runningTotal + 15, 0] = "          Accusative";
                        wordDetails[2, runningTotal + 16, 0] = "          Genitive";
                        wordDetails[2, runningTotal + 17, 0] = "          Dative";
                        tenseDesc = tenseNames[tenseIdx];

                        isUsed = false;
                        for (genderIdx = 0; genderIdx < 3; genderIdx++)
                        {
                            if (isUsed) runningTotal++;
                            for (caseIdx = 0; caseIdx < 5; caseIdx++)
                            {
                                runningTotal++;
                                for (voiceIdx = 0; voiceIdx < 3; voiceIdx++)
                                {
                                    entryCode = ((voiceIdx + 1) * 1000) + ((tenseIdx + 1) * 10) + ((genderIdx + 1) * 100000) + ((caseIdx + 1) * 10000) + 607;
                                    calcCellStatus = 0;
                                    if (currentEntryDetails.ContainsKey(entryCode))
                                    {
                                        currentEntryDetails.TryGetValue(entryCode, out currentDetails);
                                        wordDetails[voiceIdx, runningTotal, 1] = currentDetails.getWords();
                                        if (currentDetails.IsInNT) calcCellStatus = 1;
                                        if (currentDetails.IsInLXX) calcCellStatus += 2;
                                        cellStatus[voiceIdx, runningTotal, 1] = calcCellStatus;
                                        wordRefs[voiceIdx, runningTotal, 1] = currentDetails.ReferenceList;
                                    }
                                    entryCode++;
                                    calcCellStatus = 0;
                                    if (currentEntryDetails.ContainsKey(entryCode))
                                    {
                                        currentEntryDetails.TryGetValue(entryCode, out currentDetails);
                                        wordDetails[voiceIdx, runningTotal, 2] = currentDetails.getWords();
                                        if (currentDetails.IsInNT) calcCellStatus = 1;
                                        if (currentDetails.IsInLXX) calcCellStatus += 2;
                                        cellStatus[voiceIdx, runningTotal, 2] = calcCellStatus;
                                        wordRefs[voiceIdx, runningTotal, 2] = currentDetails.ReferenceList;
                                    }
                                    isUsed = true;
                                }
                            }
                        }
                        isNewTense = true;
                    }
                }
                generalUsage = new frmUsage(true, globalVars);
                generalUsage.setCategoryName("Participle");
                generalUsage.setWordItself(selectedEntry);
                generalUsage.BookList = bookNames;
                generalUsage.displayTable(wordDetails, cellStatus, 3, noOfRows, wordRefs);
                generalUsage.Show();
            }
        }

        private void processAdjectives(classRootData currentEntry, String selectedEntry)
        {
            bool isUsed = false;
            int codeCalculation, idx, noOfRows = 57, rowCount, caseIdx, genderIdx, calcCellStatus;
            String[] adjVar = { "Normal", "Comparative", "Superlative" };
            int[,,] cellStatus;
            String[,,] wordDetails;
            SortedList<int, classReference>[,,] wordRefs;
            SortedList<int, classParsedItem> currentEntryDetails;
            classParsedItem currentDetails;
            frmUsage generalUsage;

            wordDetails = new String[1, noOfRows, 3];
            cellStatus = new int[1, noOfRows, 3];
            wordRefs = new SortedList<int, classReference>[1, noOfRows, 3];
            wordDetails[0, 0, 1] = "Singular";
            wordDetails[0, 0, 2] = "Plural";
            generalUsage = new frmUsage(false, globalVars);
            for (idx = 0; idx < 3; idx++)
            {
                wordDetails[0, idx * 19 + 1, 0] = adjVar[idx] + "Adjective";
                wordDetails[0, idx * 19 + 2, 0] = "Masculine Nominative";
                wordDetails[0, idx * 19 + 3, 0] = "          Vocative";
                wordDetails[0, idx * 19 + 4, 0] = "          Accusative";
                wordDetails[0, idx * 19 + 5, 0] = "          Genitive";
                wordDetails[0, idx * 19 + 6, 0] = "          Dative";
                wordDetails[0, idx * 19 + 8, 0] = "    Neuter Nominative";
                wordDetails[0, idx * 19 + 9, 0] = "           Vocative";
                wordDetails[0, idx * 19 + 10, 0] = "           Accusative";
                wordDetails[0, idx * 19 + 11, 0] = "          Genitive";
                wordDetails[0, idx * 19 + 12, 0] = "          Dative";
                wordDetails[0, idx * 19 + 14, 0] = "Feminine  Nominative";
                wordDetails[0, idx * 19 + 15, 0] = "          Vocative";
                wordDetails[0, idx * 19 + 16, 0] = "          Accusative";
                wordDetails[0, idx * 19 + 17, 0] = "          Genitive";
                wordDetails[0, idx * 19 + 18, 0] = "          Dative";
            }
            currentEntryDetails = currentEntry.getParseList();
            rowCount = 0;
            generalUsage.setCategoryName(fullCategoryNames[categoryCode]);
            generalUsage.setWordItself(selectedEntry);
            //            currentEntryDetails = currentEntry.getEntryDetails();
            for (idx = 0; idx < 3; idx++)
            {
                rowCount++;
                if (idx > 0) rowCount++;
                for (genderIdx = 0; genderIdx < 3; genderIdx++)
                {
                    if (genderIdx > 0) rowCount++;
                    for (caseIdx = 0; caseIdx < 5; caseIdx++)
                    {
                        rowCount++;
                        codeCalculation = (idx * 1000000) + ((genderIdx + 1) * 100000) + ((caseIdx + 1) * 10000) + 7;
                        calcCellStatus = 0;
                        if (currentEntryDetails.ContainsKey(codeCalculation))
                        {
                            currentEntryDetails.TryGetValue(codeCalculation, out currentDetails);
                            wordDetails[0, rowCount, 1] = currentDetails.getWords();
                            if (currentDetails.IsInNT) calcCellStatus = 1;
                            if (currentDetails.IsInLXX) calcCellStatus += 2;
                            cellStatus[0, rowCount, 1] = calcCellStatus;
                            wordRefs[0, rowCount, 1] = currentDetails.ReferenceList;
                        }
                        codeCalculation++;
                        calcCellStatus = 0;
                        if (currentEntryDetails.ContainsKey(codeCalculation))
                        {
                            currentEntryDetails.TryGetValue(codeCalculation, out currentDetails);
                            wordDetails[0, rowCount, 2] = currentDetails.getWords();
                            if (currentDetails.IsInNT) calcCellStatus = 1;
                            if (currentDetails.IsInLXX) calcCellStatus += 2;
                            cellStatus[0, rowCount, 2] = calcCellStatus;
                            wordRefs[0, rowCount, 2] = currentDetails.ReferenceList;
                        }
                        isUsed = true;
                    }
                }
            }
            generalUsage.BookList = bookNames;
            generalUsage.displayTable(wordDetails, cellStatus, 3, noOfRows, wordRefs);
            generalUsage.Show();
        }

        private void processSimple(classRootData currentEntry, String selectedEntry)
        {
            int calcCellStatus;
            int[,,] cellStatus;
            String[,,] wordDetails;
            SortedList<int, classReference>[,,] wordRefs;
            SortedList<int, classParsedItem> currentEntryDetails;
            classParsedItem currentDetails;
            frmUsage generalUsage;

            wordDetails = new String[1, 1, 3];
            cellStatus = new int[1, 1, 3];
            wordRefs = new SortedList<int, classReference>[1, 1, 3];
            currentEntryDetails = currentEntry.getParseList();
            calcCellStatus = 0;
            if (currentEntryDetails.ContainsKey(0))
            {
                currentEntryDetails.TryGetValue(0, out currentDetails);
                wordDetails[0, 0, 1] = currentDetails.getWords();
                if (currentDetails.IsInNT) calcCellStatus = 1;
                if (currentDetails.IsInLXX) calcCellStatus += 2;
                cellStatus[0, 0, 1] = calcCellStatus;
                wordRefs[0, 0, 1] = currentDetails.ReferenceList;
            }
            generalUsage = new frmUsage(false, globalVars);
            generalUsage.setCategoryName(fullCategoryNames[categoryCode]);
            generalUsage.setWordItself(selectedEntry);
            generalUsage.BookList = bookNames;
            generalUsage.displayTable(wordDetails, cellStatus, 3, 1, wordRefs);
            generalUsage.Show();
        }

        private String insertSpaces(int noOfSpaces)
        {
            int idx;
            String toBeReturned = "";

            for (idx = 0; idx < noOfSpaces; idx++)
            {
                toBeReturned += " ";
            }
            return toBeReturned;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            frmOptions optionsForm = new frmOptions(globalVars);
            optionsForm.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            String fileName;
            frmHelp gbswordUseHelp;

            fileName = globalVars.BaseDirectory + globalVars.HelpDirectory + @"\Help.html";
            gbswordUseHelp = new frmHelp(fileName);
            gbswordUseHelp.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutBox;

            aboutBox = new frmAbout();
            aboutBox.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
