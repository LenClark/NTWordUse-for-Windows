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
    public partial class frmUsage : Form
    {
        bool isVerb, onlyDisplayNT = false;
        int noOfTabs, totalRows;
        String categoryName, wordItself;
        SortedList<int, classReference>[,,] currentRefList;
        DataGridView[] dgvCollection;
        classGlobal globalVars;

        /*======================================================================*
         *  The following variables **must** be set before calling displayTable *
         *======================================================================*/
        SortedList<int, classBooks> bookList;

        public SortedList<int, classBooks> BookList { get => bookList; set => bookList = value; }

        public frmUsage()
        {
            InitializeComponent();
        }

        public frmUsage(bool bDisplay, classGlobal inGlobal)
        {
            int idx, widthOfDGV;
            DataGridView currentDGV;
            Font fntBold;

            InitializeComponent();
            globalVars = inGlobal;
            onlyDisplayNT = !globalVars.DoesIncludeLXX;
            if (onlyDisplayNT) pnlKey.Visible = false;
            fntBold = new Font("Times New Roman", 12, FontStyle.Regular);
            isVerb = bDisplay;
            if (bDisplay)
            {
                /*==================================================================*
                 *  noOfTabs = 3 to accommodate verb voices; all others use only 1  *
                 *==================================================================*/
                noOfTabs = 3;
                tabCtrlParadigm.TabPages.Add("Middle");
                tabCtrlParadigm.TabPages.Add("Passive");
                /*==================================================================*
                 *  dgvCollection allows setup separately from display              *
                 *==================================================================*/
                dgvCollection = new DataGridView[3];
                dgvCollection[0] = dgvUsage;
                currentDGV = new DataGridView();
                currentDGV.Name = "dgvMiddle";
                currentDGV.Font = fntBold;
                currentDGV.CellMouseClick += dgvUsage_CellMouseClick;
                tabCtrlParadigm.TabPages[1].Controls.Add(currentDGV);
                dgvCollection[1] = currentDGV;
                currentDGV = new DataGridView();
                currentDGV.Name = "dgvPassive";
                currentDGV.Font = fntBold;
                currentDGV.CellMouseClick += dgvUsage_CellMouseClick;
                tabCtrlParadigm.TabPages[2].Controls.Add(currentDGV);
                dgvCollection[2] = currentDGV;
            }
            else
            {
                noOfTabs = 1;
                dgvCollection = new DataGridView[1];
                dgvCollection[0] = dgvUsage;
            }

            widthOfDGV = dgvUsage.Width;
            for (idx = 0; idx < noOfTabs; idx++)
            {
                if (idx > 0)
                {
                    dgvCollection[idx].Dock = DockStyle.Fill;
                    dgvCollection[idx].ColumnCount = 3;
                }
                dgvCollection[idx].ColumnHeadersVisible = false;
                dgvCollection[idx].RowHeadersVisible = false;
                dgvCollection[idx].Columns[0].Width = (widthOfDGV / 3) - 1;
                dgvCollection[idx].Columns[1].Width = (widthOfDGV / 3) - 1;
                dgvCollection[idx].Columns[2].Width = widthOfDGV - 2 * (widthOfDGV / 3) - 3;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUsage_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int pageIndex;
            String tableContents;
            frmReference currentRef;
//            TabPage activePage;

            if (e.Button == MouseButtons.Right)
            {
                pageIndex = tabCtrlParadigm.SelectedIndex;
                if (e.ColumnIndex > 0)
                {
                    if (dgvCollection[pageIndex].Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
                    tableContents = dgvCollection[pageIndex].Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    currentRef = new frmReference(globalVars);
                    currentRef.BookList = bookList;
                    currentRef.setRefDetail(currentRefList, pageIndex, e.RowIndex, e.ColumnIndex, tableContents, this, ! onlyDisplayNT);
                }
            }
        }

        public void setCategoryName(String value)
        {
            categoryName = value;
            if (noOfTabs == 1)
            {
                tabCtrlParadigm.TabPages[0].Text = categoryName;
            }
            else
            {
                tabCtrlParadigm.TabPages[0].Text = "Verb - Active";
            }
        }

        public void setWordItself(String wordChosen)
        {
            wordItself = wordChosen;
            this.Text = "New Testament Usage - " + categoryName + ": " + wordChosen;
        }

        public void displayTable(String[,,] tableContents, int[,,] cellStatus, int noOfCols, int noOfRows, SortedList<int, classReference>[,,] entryReferences)
        {

            /***************************************************************************
             *                                                                         *
             *  tableContents - a 3-dimensional String array.                          *
             *  Dimensions are:                                                        *
             *  Position 1: Verbs: Mood (0 = Active, 1 = Middle, 2 = Passive)          *
             *              All other categories = 0 (1 and 2 are not used)            *
             *           2: Row (i.e. line on the report)                              *
             *           3: Column                                                     *
             *                                                                         *
             ***************************************************************************/
            int idx, jdx, limit;
            Font normalFont = new Font("Times New Roman", 12F, FontStyle.Regular);
            Font boldFont = new Font("Times New Roman", 12F, FontStyle.Bold);
            Font italicFont = new Font("Times New Roman", 12F, FontStyle.Italic);
            totalRows = noOfRows;
            currentRefList = entryReferences;
            if (isVerb)
            {
                limit = 3;
            }
            else
            {
                limit = 1;
            }
            for (idx = 0; idx < limit; idx++)
            {
                dgvCollection[idx].Rows.Clear();
                dgvCollection[idx].RowCount = noOfRows + 1;
            }
            onlyDisplayNT = !globalVars.DoesIncludeLXX;
            for (jdx = 0; jdx < noOfCols; jdx++)
            {
                dgvUsage.Rows[0].Cells[jdx].Style.Font = boldFont;
                dgvUsage.Rows[0].Cells[jdx].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvUsage.Rows[0].Cells[jdx].Value = tableContents[0, 0, jdx];
                if (isVerb)
                {
                    dgvCollection[1].Rows[0].Cells[jdx].Style.Font = boldFont;
                    dgvCollection[1].Rows[0].Cells[jdx].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCollection[1].Rows[0].Cells[jdx].Value = tableContents[1, 0, jdx];
                    dgvCollection[2].Rows[0].Cells[jdx].Style.Font = boldFont;
                    dgvCollection[2].Rows[0].Cells[jdx].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCollection[2].Rows[0].Cells[jdx].Value = tableContents[2, 0, jdx];
                }
            }
            for (idx = 1; idx < noOfRows; idx++)
            {
                for (jdx = 0; jdx < noOfCols; jdx++)
                {
                    if (jdx == 0)
                    {
                        dgvUsage.Rows[idx].Cells[0].Value = tableContents[0, idx, 0];
                        dgvUsage.Rows[idx].Cells[0].Style.Font = boldFont;
                        if (isVerb)
                        {
                            dgvCollection[1].Rows[idx].Cells[0].Value = tableContents[0, idx, 0];
                            dgvCollection[1].Rows[idx].Cells[0].Style.Font = boldFont;
                            dgvCollection[2].Rows[idx].Cells[0].Value = tableContents[0, idx, 0];
                            dgvCollection[2].Rows[idx].Cells[0].Style.Font = boldFont;
                        }
                    }
                    else
                    {
                        if (onlyDisplayNT)
                        {
                            if ((cellStatus[0, idx, jdx] == 1) || (cellStatus[0, idx, jdx] == 3)) dgvUsage.Rows[idx].Cells[jdx].Value = tableContents[0, idx, jdx];
                        }
                        else dgvUsage.Rows[idx].Cells[jdx].Value = tableContents[0, idx, jdx];
                        dgvUsage.Rows[idx].Cells[jdx].Style.Font = normalFont;
                        if (!onlyDisplayNT)
                        {
                            switch (globalVars.NtOnlyCode)
                            {
                                case 1: if (cellStatus[0, idx, jdx] == 1) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                                case 2: if (cellStatus[0, idx, jdx] == 1) dgvUsage.Rows[idx].Cells[jdx].Style.Font = boldFont; break;
                                case 3:
                                    if (cellStatus[0, idx, jdx] == 1)
                                    {
                                        dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Red;
                                        dgvUsage.Rows[idx].Cells[jdx].Style.Font = boldFont;
                                    }
                                    break;
                                default: if (cellStatus[0, idx, jdx] == 1) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                            }
                            //                            if (cellStatus[0, idx, jdx] == 1) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Red;
                            switch (globalVars.LxxOnlyCode)
                            {
                                case 1: if (cellStatus[0, idx, jdx] == 2) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                                case 2: if (cellStatus[0, idx, jdx] == 2) dgvUsage.Rows[idx].Cells[jdx].Style.Font = italicFont; break;
                                case 3:
                                    if (cellStatus[0, idx, jdx] == 2)
                                    {
                                        dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray;
                                        dgvUsage.Rows[idx].Cells[jdx].Style.Font = italicFont;
                                    }
                                    break;
                                default: if (cellStatus[0, idx, jdx] == 2) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                            }
                            //                            if (cellStatus[0, idx, jdx] == 2) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray;
                            switch (globalVars.NtAndLxxCode)
                            {
                                case 1: if (cellStatus[0, idx, jdx] == 3) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                                case 2: if (cellStatus[0, idx, jdx] == 3) dgvUsage.Rows[idx].Cells[jdx].Style.Font = normalFont; break;
                                default: if (cellStatus[0, idx, jdx] == 3) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                            }
//                            if (cellStatus[0, idx, jdx] == 3) dgvUsage.Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange;
                        }
                        if (isVerb)
                        {
                            if (onlyDisplayNT)
                            {
                                if ((cellStatus[1, idx, jdx] == 1) || (cellStatus[1, idx, jdx] == 3)) dgvCollection[1].Rows[idx].Cells[jdx].Value = tableContents[1, idx, jdx];
                            }
                            else dgvCollection[1].Rows[idx].Cells[jdx].Value = tableContents[1, idx, jdx];
                            dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = normalFont;
                            if (!onlyDisplayNT)
                            {
                                switch (globalVars.NtOnlyCode)
                                {
                                    case 1: if (cellStatus[1, idx, jdx] == 1) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                                    case 2: if (cellStatus[1, idx, jdx] == 1) dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = boldFont; break;
                                    case 3: if (cellStatus[1, idx, jdx] == 1)
                                        {
                                            dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red;
                                            dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = boldFont;
                                        } break;
                                    default: if (cellStatus[1, idx, jdx] == 1) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                                }
                                switch (globalVars.LxxOnlyCode)
                                {
                                    case 1: if (cellStatus[1, idx, jdx] == 2) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                                    case 2: if (cellStatus[1, idx, jdx] == 2) dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = italicFont; break;
                                    case 3: if (cellStatus[1, idx, jdx] == 2)
                                        {
                                            dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray;
                                            dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = italicFont;
                                        }
                                        break;
                                    default: if (cellStatus[1, idx, jdx] == 2) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                                }
                                switch (globalVars.NtAndLxxCode)
                                {
                                    case 1: if (cellStatus[1, idx, jdx] == 3) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                                    case 2: if (cellStatus[1, idx, jdx] == 3) dgvCollection[1].Rows[idx].Cells[jdx].Style.Font = normalFont; break;
                                    default: if (cellStatus[1, idx, jdx] == 3) dgvCollection[1].Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                                }
                            }
                            if (onlyDisplayNT)
                            {
                                if ((cellStatus[2, idx, jdx] == 1) || (cellStatus[2, idx, jdx] == 3)) dgvCollection[2].Rows[idx].Cells[jdx].Value = tableContents[2, idx, jdx];
                            }
                            else dgvCollection[2].Rows[idx].Cells[jdx].Value = tableContents[2, idx, jdx];
                            dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = normalFont;
                            if (!onlyDisplayNT)
                            {
                                switch (globalVars.NtOnlyCode)
                                {
                                    case 1: if (cellStatus[2, idx, jdx] == 1) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                                    case 2: if (cellStatus[2, idx, jdx] == 1) dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = boldFont; break;
                                    case 3:
                                        if (cellStatus[2, idx, jdx] == 1)
                                        {
                                            dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red;
                                            dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = boldFont;
                                        }
                                        break;
                                    default: if (cellStatus[2, idx, jdx] == 1) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Red; break;
                                }
                                switch (globalVars.LxxOnlyCode)
                                {
                                    case 1: if (cellStatus[2, idx, jdx] == 2) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                                    case 2: if (cellStatus[2, idx, jdx] == 2) dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = italicFont; break;
                                    case 3:
                                        if (cellStatus[2, idx, jdx] == 2)
                                        {
                                            dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray;
                                            dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = italicFont;
                                        }
                                        break;
                                    default: if (cellStatus[2, idx, jdx] == 2) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Gray; break;
                                }
                                switch (globalVars.NtAndLxxCode)
                                {
                                    case 1: if (cellStatus[2, idx, jdx] == 3) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                                    case 2: if (cellStatus[2, idx, jdx] == 3) dgvCollection[2].Rows[idx].Cells[jdx].Style.Font = normalFont; break;
                                    default: if (cellStatus[2, idx, jdx] == 3) dgvCollection[2].Rows[idx].Cells[jdx].Style.ForeColor = Color.Orange; break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int idx, jdx, whichTab;
            String fileName;
            String[] col = new String[3];
            StreamWriter swCSV;

            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                fileName = dlgSave.FileName;
                swCSV = new StreamWriter(fileName);
                if (isVerb)
                {
                    whichTab = tabCtrlParadigm.SelectedIndex;
                }
                else whichTab = 0;

                for (idx = 0; idx < totalRows; idx++)
                {
                    for (jdx = 0; jdx < 3; jdx++)
                    {
                        if (dgvCollection[whichTab].Rows[idx].Cells[jdx].Value == null)
                        {
                            col[jdx] = "";
                        }
                        else
                        {
                            col[jdx] = dgvCollection[whichTab].Rows[idx].Cells[jdx].Value.ToString();
                        }
                    }
                    swCSV.WriteLine(col[0] + "\t" + col[1] + "\t" + col[2]);
                }
                swCSV.Close();
                swCSV.Dispose();
                MessageBox.Show("The " + categoryName + ": " + wordItself + " has been saved as CSV\n\tin: " + fileName, "Save as CSV successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
