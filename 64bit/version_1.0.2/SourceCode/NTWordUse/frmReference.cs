using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NTWordUse
{
    public partial class frmReference : Form
    {
        int clickedRow = 0, totalRows;
        String controlingWord;
        frmUsage parentUsage;
        SortedList<int, classBooks> bookList;
        classGlobal globalVars;

        public SortedList<int, classBooks> BookList { get => bookList; set => bookList = value; }

        public frmReference(classGlobal inGlobal)
        {
            InitializeComponent();
            globalVars = inGlobal;
            dgvReferences.Columns[2].Width = dgvReferences.Width - (dgvReferences.Columns[0].Width + dgvReferences.Columns[1].Width);
        }

        public void setRefDetail(SortedList<int, classReference>[,,] inputRefs, int tabNo, int rowNo, int colNo, String tabContents, frmUsage callingUsage, bool isLXXDisplayed)
        {
            /*********************************************************************************************************
             *                                                                                                       *
             *                                               setRefDetail                                            *
             *                                               ============                                            *
             *                                                                                                       *
             *  Parameters:                                                                                          *
             *  ==========                                                                                           *
             *                                                                                                       *
             *  inputRefs     This is a 3-d array providing the reference details for each cell of the table         *
             *  tabNo         aka page index; only relevant for verbs, which have separate pages for active, middle  *
             *                and passive voices.  (All other grammatical types have a single page/table).           *
             *  rowNo         Fairly obvious                                                                         *
             *  colNo         Fairly obvious                                                                         *
             *  tabContents   The text for tabNo, rowNo, colNo                                                       *
             *  callingUsage  The address of the frmUsage instance calling this method                               *
             *                                                                                                       *
             *  Other key elements must be set for the instance _before_ calling setRefDetail.  These are:           *
             *                                                                                                       *
             *     bookList                                                                                          *
             *                                                                                                       *
             *********************************************************************************************************/

            bool isNt;
            int idx, noOfRows, bookNo, bookRef, chapNo, VerseNo, prevBookRef = 0, prevChapNo = 0;
            String bookName = "", givenChapterRef = "", givenVerseRef = "";
            classBooks currentBook;

            parentUsage = callingUsage;
            this.Text = "References for: " + tabContents;
            controlingWord = tabContents;
            noOfRows = -1;
            dgvReferences.RowCount = 0;
            foreach (KeyValuePair<int, classReference> refDetail in inputRefs[tabNo, rowNo, colNo])
            {
                isNt = refDetail.Value.IsNT;
                if ((!isLXXDisplayed) && (!isNt)) continue;
                bookNo = refDetail.Value.BookCode;
                chapNo = refDetail.Value.ChapterNo;
                VerseNo = refDetail.Value.VerseNo;
                givenChapterRef = refDetail.Value.GivenChapterRef;
                givenVerseRef = refDetail.Value.GivenVerseRef;
                if (bookNo != prevBookRef)
                {

                    bookList.TryGetValue(bookNo, out currentBook);
                    bookName = currentBook.CommonName;
                    noOfRows++;
                    dgvReferences.RowCount++;
                    dgvReferences.Rows[noOfRows].Cells[0].Value = bookName;
                    prevBookRef = bookNo;
                    prevChapNo = chapNo;
                    if (isNt)
                    {
                        dgvReferences.Rows[noOfRows].Cells[1].Value = chapNo;
                        dgvReferences.Rows[noOfRows].Cells[2].Value = bookName + " " + chapNo.ToString() + ": " + VerseNo.ToString();
                        dgvReferences.Rows[noOfRows].Cells[4].Value = VerseNo.ToString();
                    }
                    else
                    {
                        dgvReferences.Rows[noOfRows].Cells[1].Value = givenChapterRef;
                        dgvReferences.Rows[noOfRows].Cells[2].Value = bookName + " " + givenChapterRef + ": " + givenVerseRef;
                        dgvReferences.Rows[noOfRows].Cells[4].Value = givenVerseRef;
                        dgvReferences.Rows[noOfRows].Cells[2].Style.Font = new Font(dgvReferences.DefaultCellStyle.Font.FontFamily, dgvReferences.DefaultCellStyle.Font.Size, FontStyle.Italic);
                    }
                    dgvReferences.Rows[noOfRows].Cells[3].Value = bookNo;
                }
                else
                {
                    if (chapNo != prevChapNo)
                    {
                        noOfRows++;
                        dgvReferences.RowCount++;
                        prevChapNo = chapNo;
                        if (isNt)
                        {
                            dgvReferences.Rows[noOfRows].Cells[1].Value = chapNo;
                            dgvReferences.Rows[noOfRows].Cells[2].Value = bookName + " " + chapNo.ToString() + ": " + VerseNo.ToString();
                            dgvReferences.Rows[noOfRows].Cells[4].Value = VerseNo.ToString();
                        }
                        else
                        {
                            dgvReferences.Rows[noOfRows].Cells[1].Value = givenChapterRef;
                            dgvReferences.Rows[noOfRows].Cells[2].Value = bookName + " " + givenChapterRef + ": " + givenVerseRef;
                            dgvReferences.Rows[noOfRows].Cells[2].Style.Font = new Font(dgvReferences.DefaultCellStyle.Font.FontFamily, dgvReferences.DefaultCellStyle.Font.Size, FontStyle.Italic);
                            dgvReferences.Rows[noOfRows].Cells[4].Value = givenVerseRef;
                        }
                        dgvReferences.Rows[noOfRows].Cells[3].Value = bookNo;
                    }
                    else
                    {
                        if (isNt)
                        {
                            dgvReferences.Rows[noOfRows].Cells[2].Value = dgvReferences.Rows[noOfRows].Cells[2].Value + ", " + VerseNo.ToString();
                            dgvReferences.Rows[noOfRows].Cells[4].Value = dgvReferences.Rows[noOfRows].Cells[4].Value + ", " + VerseNo.ToString();
                        }
                        else
                        {
                            dgvReferences.Rows[noOfRows].Cells[2].Value = dgvReferences.Rows[noOfRows].Cells[2].Value + ", " + givenVerseRef;
                            dgvReferences.Rows[noOfRows].Cells[4].Value = dgvReferences.Rows[noOfRows].Cells[4].Value + ", " + givenVerseRef;
                        }
                    }
                }
            }
            totalRows = noOfRows;
            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mnuRightShow.Visible = true;
            mnuRightOnlyShow.Visible = false;
            mnuRightKeep.Visible = true;
            this.Close();
            this.Dispose();
        }

        private void btnKeep_Click(object sender, EventArgs e)
        {
            /********************************************************************************************
             *                                                                                          *
             *                                     btnKeep_Click                                        *
             *                                     =============                                        *
             *                                                                                          *
             *  Purpose:                                                                                *
             *  =======                                                                                 *
             *  To display the whole chapter of the selected reference list row, with verses that       *
             *    contain reviewed words in bold font.                                                  *
             *                                                                                          *
             *  Note: the bizarre name is because originally it had additional functionality.           *
             *        Apologies for the lack of logic.                                                  *
             *                                                                                          *
             ********************************************************************************************/

            int idx, currRow, bookNo, chapNo, noOfVerses;
            String chapText = "", verseList, bookName;
            String[] verseArray;
            Char[] splitArray = { ',' };
            classBooks currentBook;
            classChapter currentChapter;
            frmChapter currChapter;
            List<int> verseNos = new List<int>();

            /*--------------------------------------------------------------------------------------------*
             *                                                                                            *
             *          Build the bare text                                                               *
             *                                                                                            *
             *--------------------------------------------------------------------------------------------*/
            // Get the table row that is currently selected
            currRow = dgvReferences.SelectedRows[0].Index;
            // Store the relevant values from that row
            bookNo = Convert.ToInt32(dgvReferences.Rows[currRow].Cells[3].Value);   // Hidden column
            bookList.TryGetValue(bookNo, out currentBook);
            bookName = currentBook.CommonName;
            chapNo = Convert.ToInt32(dgvReferences.Rows[currRow].Cells[1].Value);
            verseList = dgvReferences.Rows[currRow].Cells[4].Value.ToString();      // Hidden column
                                                                                    // Separate each verse (stored in the table as a CSV list; extract and place in a List)
            verseArray = verseList.Split(splitArray);
            noOfVerses = verseArray.Length;
            for (idx = 0; idx < noOfVerses; idx++)
            {
                verseNos.Add(Convert.ToInt32(verseArray[idx]));
            }
            // Now iterate through the raw data to find the relevant chapter
            currentChapter = currentBook.getChapterBySequence(chapNo);
            if (currentChapter != null)
            {
                noOfVerses = currentChapter.NoOfVerses;
                for (idx = 1; idx <= noOfVerses; idx++)
                {
                    if (chapText.Length == 0)
                    {
                        chapText = currentChapter.getVerseRefBySequence(idx) + ": " + currentChapter.getVerseText(idx);
                    }
                    else
                    {
                        chapText += "\n" + currentChapter.getVerseRefBySequence(idx) + ": " + currentChapter.getVerseText(idx);
                    }
                }
            }
            currChapter = new frmChapter();
            currChapter.populateText(chapText, verseNos, bookName, chapNo, parentUsage, this);
            dgvReferences.ContextMenuStrip = null;
        }


        private void mnuRightShow_Click(object sender, EventArgs e)
        {
            btnKeep_Click(btnKeep, e);
            btnKeep_Click(btnKeep, e);
        }

        private void btnClosePrev_Click(object sender, EventArgs e)
        {
            parentUsage.Close();
            this.Close();
            this.Dispose();
        }

        private void dgvReferences_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idx, x = 0, y = 0;

            if (e.Button == MouseButtons.Right)
            {
                dgvReferences.ClearSelection();
                dgvReferences.Rows[e.RowIndex].Selected = true;
 //               dgvReferences.ContextMenuStrip = mnuRight;
                switch (e.ColumnIndex)
                {
                    case 0: x = e.Location.X; break;
                    case 1: x = dgvReferences.Columns[0].Width + e.Location.X; break;
                    case 2: x = dgvReferences.Columns[0].Width + dgvReferences.Columns[1].Width + e.Location.X; break;
                }
                y = 0;
                for (idx = 0; idx < e.RowIndex; idx++)
                {
                    y += dgvReferences.Rows[idx].Height;
                }
                y += e.Location.Y;
                //                mnuRight.Show(dgvReferences, x, y);
                btnKeep_Click(sender, e);
            }
        }

        private void saveCSV_Click(object sender, EventArgs e)
        {
            int idx, jdx;
            String fileName;
            String[] col = new String[3];
            StreamWriter swCSV;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                fileName = dlgSave.FileName;
                swCSV = new StreamWriter(fileName);
                for (idx = 0; idx < totalRows; idx++)
                {
                    for (jdx = 0; jdx < 3; jdx++)
                    {
                        if (dgvReferences.Rows[idx].Cells[jdx].Value == null)
                        {
                            col[jdx] = "";
                        }
                        else
                        {
                            col[jdx] = dgvReferences.Rows[idx].Cells[jdx].Value.ToString();
                        }
                    }
                    swCSV.WriteLine(col[0] + "\t" + col[1] + "\t" + col[2]);
                }
                swCSV.Close();
                swCSV.Dispose();
                MessageBox.Show("The references for the word: " + controlingWord + " have been saved as CSV\n\tin: " + fileName, "Save as CSV successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
