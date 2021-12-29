using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTWordUse
{
    public partial class frmChapter : Form
    {
        frmUsage parentUsage;
        frmReference parentRef;

        public frmChapter()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void populateText(String content, List<int> referencedVerses, String bookName, int chapNo, frmUsage callingUsage, frmReference callingRef)
        {
            int nStart, nEnd;
            Font fntBold;

            parentUsage = callingUsage;
            parentRef = callingRef;
            this.Text = "Text of " + bookName + " " + chapNo.ToString();
            fntBold = new Font(rtxtChapter.Font.Name, 12, FontStyle.Bold);
            // First: simply store the bare text
            rtxtChapter.Text = content;
            // Now we cycle through the text to find the selected verses
            foreach (int verseNo in referencedVerses)
            {
                nStart = rtxtChapter.Text.IndexOf(verseNo.ToString() + ": ");
                if (nStart > -1)
                {
                    nEnd = rtxtChapter.Text.IndexOf('\n', nStart);
                    if (nEnd == -1) nEnd = rtxtChapter.Text.Length - 1;
                    rtxtChapter.SelectionStart = nStart;
                    rtxtChapter.SelectionLength = nEnd - nStart;
                    rtxtChapter.SelectionFont = fntBold;
                }
            }
            Show();
        }

        public String retrieveText()
        {
            return rtxtChapter.Rtf;
        }

        public void repopulateText(String content)
        {
            rtxtChapter.Rtf = content;
        }

        private void btnBackToBase_Click(object sender, EventArgs e)
        {
            parentUsage.Close();
            parentRef.Close();
            Close();
        }
    }
}
