using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace NTWordUse
{
    public partial class frmOptions : Form
    {
        int localNtOnly, localLxxOnly, localNtAndLxx;
        classGlobal globalVars;

        public frmOptions( classGlobal inGlobal)
        {
            InitializeComponent();
            globalVars = inGlobal;
            localNtOnly = globalVars.NtOnlyCode;
            localLxxOnly = globalVars.LxxOnlyCode;
            localNtAndLxx = globalVars.NtAndLxxCode;
            switch( localNtOnly )
            {
                case 1: rbtnNTRed.Checked = true; break;
                case 2: rbtnNTBold.Checked = true; break;
                case 3: rbtnNTBoth.Checked = true; break;
                default: break;
            }
            switch (localLxxOnly)
            {
                case 1: rbtnLXXGrey.Checked = true; break;
                case 2: rbtnLXXItalic.Checked = true; break;
                case 3: rbtnLXXBoth.Checked = true; break;
                default: break;
            }
            switch (localNtOnly)
            {
                case 1: rbtnBothOrange.Checked = true; break;
                case 2: rbtnBothNormal.Checked = true; break;
                default: break;
            }
            if (!globalVars.DoesIncludeLXX) cbLXX.Checked = false;
        }

        private void cbLXX_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox lxxUse;

            lxxUse = (CheckBox)sender;
            if (lxxUse.Checked) gbPresentation.Visible = true;
            else gbPresentation.Visible = false;
        }

        private void rbtnNT_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNTRed.Checked) localNtOnly = 1;
            if (rbtnNTBold.Checked) localNtOnly = 2;
            if (rbtnNTBoth.Checked) localNtOnly = 3;
        }

        private void rbtnLXX_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLXXGrey.Checked) localLxxOnly = 1;
            if (rbtnLXXItalic.Checked) localLxxOnly = 2;
            if (rbtnLXXBoth.Checked) localLxxOnly = 3;
        }

        private void rbtnBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBothOrange.Checked) localNtAndLxx = 1;
            if (rbtnBothNormal.Checked) localNtAndLxx = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            globalVars.DoesIncludeLXX = cbLXX.Checked;
            if (cbLXX.Checked) globalVars.updateRegistrySetting(1, 0);
            if (cbLXX.Checked)
            {
                globalVars.NtOnlyCode = localNtOnly;
                globalVars.updateRegistrySetting(localNtOnly, 1);
                globalVars.LxxOnlyCode = localLxxOnly;
                globalVars.updateRegistrySetting(localLxxOnly, 2);
                globalVars.NtAndLxxCode = localNtAndLxx;
                globalVars.updateRegistrySetting(localNtAndLxx, 3);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
