using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTWordUse
{
    class classParsedItem
    {
        /******************************************************************************************
         *                                                                                        *
         *                                    classParsedItem                                     *
         *                                    ===============                                     *
         *                                                                                        *
         *  This categorisation is driven by the Parse Code provided in the source data.  A       *
         *  parseCode was generated in the processing of the "raw word" which gives a unique      *
         *  grammatical value to a word based on its parsing.  So, for example, the verb ζητέω    *
         *  used in the form ζητησάτωσαν is provided (in LXX) with the parse code AAD3P.  The     *
         *  variant Ζητησάτωσαν has the same parse code and, therefore, the same numeric value.   *
         *  Each classParseCode instance represents a specific grammatical occurrence but can     *
         *  have multiple "parsedWords" because of variations in spelling, accents and the like.  *
         *                                                                                        *
         ******************************************************************************************/

        bool isInNT = false, isInLXX = false;
        int parseCode;

        List<String> parsedWords = new List<string>();

        /*========================================================================================*
         *                                                                                        *
         *                                     comparator                                         *
         *                                     ==========                                         *
         *                                                                                        *
         *  There are a small number of characters that occur twice in the Unicode representation *
         *  of Greek - once in the lower valued set of characters and once in the higher valued   *
         *  table.  They are all characters with an accute accent.  This dictionary allows us to  *
         *  retrieve the alternative.  So, a comparison of the wholw word must:                   *
         *    a) check each character, to see if it is one with alternative representation;       *
         *    b) if not, simply check that that particular character agrees in both cases;        *
         *    c) if it _is_ one of these characters, check for a match in either representation.  *
         *                                                                                        *
         *========================================================================================*/
        Dictionary<String, String> comparator = new Dictionary<string, string>();

        /*========================================================================================*
         *                                                                                        *
         *                                    referenceList                                       *
         *                                    =============                                       *
         *                                                                                        *
         *  We pick up specific form usage as we scan through the text.  That means that there is *
         *  a book-chapter-verse reference for each occurrence of each form of a word.  Since     *
         *  this instance represents a specific grammatical form of a word (say, the genitive     *
         *  plural of a noun or the 3rd person future indicative active of a verb), then there    *
         *  will be one or more references for that specific grammatical form.  The details will  *
         *  be held in the classReference instance.  The list, referenceList, contains a list of  *
         *  all those instances for the grammatical form.                                         *
         *                                                                                        *
         *  key:   An integer formed from the specific reference so that each different book-     *
         *         chapter-verse will give a different value.  This is purely to ensure a single  *
         *         reference displayed when the form occurs multiple times in a verse.            *
         *  value: The classReference instance address                                            *
         *                                                                                        *
         *========================================================================================*/
        SortedList<int, classReference> referenceList = new SortedList<int, classReference>();

        public bool IsInNT { get => isInNT; set => isInNT = value; }
        public bool IsInLXX { get => isInLXX; set => isInLXX = value; }
        public int ParseCode { get => parseCode; set => parseCode = value; }
        internal SortedList<int, classReference> ReferenceList { get => referenceList; set => referenceList = value; }

        public classParsedItem()
        {
            int idx, arrayLength;
            String[] baseChars = { "Ά", "Έ", "Ή", "Ί", "Ό", "Ύ", "Ώ", "ΐ", "ά", "έ", "ή", "ί", "ΰ", "ό", "ύ", "ώ" };
            String[] upperEquivalents = { "Ά", "Έ", "Ή", "Ί", "Ό", "Ύ", "Ώ", "ΐ", "ά", "έ", "ή", "ί", "ΰ", "ό", "ύ", "ώ" };

            arrayLength = baseChars.Length;
            for( idx = 0; idx < arrayLength; idx++)
            {
                comparator.Add(baseChars[idx], upperEquivalents[idx]);
            }
        }

        public void setWord(String candidateWord)
        {
            bool isMatch = false;

            foreach( String existingWord in parsedWords)
            {
                isMatch = wordMatch(existingWord, candidateWord);
                if (isMatch) return;
            }
            if (! isMatch)
            {
                parsedWords.Add(candidateWord);
            }
        }

        public bool wordMatch( String word1, String word2)
        {
            int idx, wordLength;

            wordLength = word1.Length;
            if (word2.Length != wordLength) return false;
            for( idx = 0; idx < wordLength; idx++ )
            {
                if (!characterMatch(word1.Substring(idx, 1), word2.Substring(idx, 1))) return false;
            }
            return true;
        }

        public bool characterMatch( String char1, String char2 )
        {
            bool isMatch = false;
            String comp1, comp2;

            // is char1 one of the doubly represented characters?
            if (comparator.ContainsKey(char1)) comparator.TryGetValue(char1, out comp1);
            else comp1 = char1;
            // Now for the other
            if (comparator.ContainsKey(char2)) comparator.TryGetValue(char2, out comp2);
            else comp2 = char2;
            // Now we can be sure we're dealing like with like
            if (String.Compare(comp1, comp2) == 0) isMatch = true;
            return isMatch;
        }

        public void addReference(int refCode, int bookNo, int chapNo, int verseNo, String givenChapRef, String givenVerseRef, bool isNT)
        {
            /*============================================================================================*
             *                                                                                            *
             *                                        addReference                                        *
             *                                        ============                                        *
             *                                                                                            *
             *  Parameters:                                                                               *
             *  ==========                                                                                *
             *                                                                                            *
             *  refCode         An integer value that uiquely identifies a book + chapter + verse.        *
             *                    Note that, for the LXX, our artificial chapter and verse sequence is    *
             *                    used and _not_ the actual (given) chapter and verse.                    *
             *  bookCode        The actual book code (starting from 1), where the LXX index follows on    *
             *                    from the last book in the NT                                            *
             *  chapNo          For NT, the chapter given in the source data; for LXX a unique sequence   *
             *  verseNo         For NT, the verse given in the source data; for LXX a unique sequence     *
             *  givenChapRef    Only relevant to LXX: the chapter as given in the source data (NT = "")   *
             *  givenVerseRef   Only relevant to LXX: the verse as given in the source data (NT = "")     *
             *  isNT            Boolean value: true, if the data comes from NT source; otherwise false    *
             *                                                                                            *
             *============================================================================================*/

            classReference newReference;


            if (!referenceList.ContainsKey(refCode))
            {
                newReference = new classReference();
                newReference.BookCode = bookNo;
                newReference.ChapterNo = chapNo;
                newReference.VerseNo = verseNo;
                if (!isNT)
                {
                    newReference.GivenChapterRef = givenChapRef;
                    newReference.GivenVerseRef = givenVerseRef;
                    newReference.IsNT = isNT;
                }
                referenceList.Add(refCode, newReference);
            }
        }

        public String getWords()
        {
            String wordList = "";

            foreach (String individualWord in parsedWords)
            {
                if (wordList.Length == 0)
                {
                    wordList = individualWord;
                }
                else
                {
                    wordList += ", " + individualWord;
                }
            }
            return wordList;
        }
/*
        public SortedList<int, classReferenceDetail> getReferences()
        {
            return refList.getReference();
        } */
    }
}
