
namespace NTWordUse
{
    partial class frmOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbLXX = new System.Windows.Forms.CheckBox();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbPresentation = new System.Windows.Forms.GroupBox();
            this.gbNTOnly = new System.Windows.Forms.GroupBox();
            this.rbtnNTRed = new System.Windows.Forms.RadioButton();
            this.rbtnNTBold = new System.Windows.Forms.RadioButton();
            this.rbtnNTBoth = new System.Windows.Forms.RadioButton();
            this.gbLXXOnly = new System.Windows.Forms.GroupBox();
            this.rbtnLXXBoth = new System.Windows.Forms.RadioButton();
            this.rbtnLXXItalic = new System.Windows.Forms.RadioButton();
            this.rbtnLXXGrey = new System.Windows.Forms.RadioButton();
            this.gbNTAndLXXX = new System.Windows.Forms.GroupBox();
            this.rbtnBothNormal = new System.Windows.Forms.RadioButton();
            this.rbtnBothOrange = new System.Windows.Forms.RadioButton();
            this.pnlBase.SuspendLayout();
            this.gbPresentation.SuspendLayout();
            this.gbNTOnly.SuspendLayout();
            this.gbLXXOnly.SuspendLayout();
            this.gbNTAndLXXX.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLXX
            // 
            this.cbLXX.AutoSize = true;
            this.cbLXX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbLXX.Checked = true;
            this.cbLXX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLXX.Location = new System.Drawing.Point(12, 12);
            this.cbLXX.Name = "cbLXX";
            this.cbLXX.Size = new System.Drawing.Size(213, 17);
            this.cbLXX.TabIndex = 37;
            this.cbLXX.Text = "Include information from the Septuagint:";
            this.cbLXX.UseVisualStyleBackColor = true;
            this.cbLXX.CheckedChanged += new System.EventHandler(this.cbLXX_CheckedChanged);
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.btnCancel);
            this.pnlBase.Controls.Add(this.btnOK);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBase.Location = new System.Drawing.Point(0, 313);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(407, 30);
            this.pnlBase.TabIndex = 38;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(320, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbPresentation
            // 
            this.gbPresentation.Controls.Add(this.gbNTAndLXXX);
            this.gbPresentation.Controls.Add(this.gbLXXOnly);
            this.gbPresentation.Controls.Add(this.gbNTOnly);
            this.gbPresentation.Location = new System.Drawing.Point(35, 35);
            this.gbPresentation.Name = "gbPresentation";
            this.gbPresentation.Size = new System.Drawing.Size(328, 264);
            this.gbPresentation.TabIndex = 39;
            this.gbPresentation.TabStop = false;
            this.gbPresentation.Text = "How should we distinguish NT and LXX words: ";
            // 
            // gbNTOnly
            // 
            this.gbNTOnly.Controls.Add(this.rbtnNTBoth);
            this.gbNTOnly.Controls.Add(this.rbtnNTBold);
            this.gbNTOnly.Controls.Add(this.rbtnNTRed);
            this.gbNTOnly.Location = new System.Drawing.Point(26, 19);
            this.gbNTOnly.Name = "gbNTOnly";
            this.gbNTOnly.Size = new System.Drawing.Size(285, 79);
            this.gbNTOnly.TabIndex = 0;
            this.gbNTOnly.TabStop = false;
            this.gbNTOnly.Text = "Words found only in the NT: ";
            // 
            // rbtnNTRed
            // 
            this.rbtnNTRed.AutoSize = true;
            this.rbtnNTRed.Checked = true;
            this.rbtnNTRed.Location = new System.Drawing.Point(37, 19);
            this.rbtnNTRed.Name = "rbtnNTRed";
            this.rbtnNTRed.Size = new System.Drawing.Size(128, 17);
            this.rbtnNTRed.TabIndex = 0;
            this.rbtnNTRed.TabStop = true;
            this.rbtnNTRed.Text = "Displayed as Red text";
            this.rbtnNTRed.UseVisualStyleBackColor = true;
            this.rbtnNTRed.CheckedChanged += new System.EventHandler(this.rbtnNT_CheckedChanged);
            // 
            // rbtnNTBold
            // 
            this.rbtnNTBold.AutoSize = true;
            this.rbtnNTBold.Location = new System.Drawing.Point(37, 36);
            this.rbtnNTBold.Name = "rbtnNTBold";
            this.rbtnNTBold.Size = new System.Drawing.Size(145, 17);
            this.rbtnNTBold.TabIndex = 1;
            this.rbtnNTBold.Text = "Displayed as Bold (Black)";
            this.rbtnNTBold.UseVisualStyleBackColor = true;
            this.rbtnNTBold.CheckedChanged += new System.EventHandler(this.rbtnNT_CheckedChanged);
            // 
            // rbtnNTBoth
            // 
            this.rbtnNTBoth.AutoSize = true;
            this.rbtnNTBoth.Location = new System.Drawing.Point(37, 53);
            this.rbtnNTBoth.Name = "rbtnNTBoth";
            this.rbtnNTBoth.Size = new System.Drawing.Size(177, 17);
            this.rbtnNTBoth.TabIndex = 2;
            this.rbtnNTBoth.Text = "Displayed as both Red and Bold";
            this.rbtnNTBoth.UseVisualStyleBackColor = true;
            this.rbtnNTBoth.CheckedChanged += new System.EventHandler(this.rbtnNT_CheckedChanged);
            // 
            // gbLXXOnly
            // 
            this.gbLXXOnly.Controls.Add(this.rbtnLXXBoth);
            this.gbLXXOnly.Controls.Add(this.rbtnLXXItalic);
            this.gbLXXOnly.Controls.Add(this.rbtnLXXGrey);
            this.gbLXXOnly.Location = new System.Drawing.Point(26, 104);
            this.gbLXXOnly.Name = "gbLXXOnly";
            this.gbLXXOnly.Size = new System.Drawing.Size(285, 82);
            this.gbLXXOnly.TabIndex = 1;
            this.gbLXXOnly.TabStop = false;
            this.gbLXXOnly.Text = "Words found only in the Septuagint (LXX): ";
            // 
            // rbtnLXXBoth
            // 
            this.rbtnLXXBoth.AutoSize = true;
            this.rbtnLXXBoth.Location = new System.Drawing.Point(37, 53);
            this.rbtnLXXBoth.Name = "rbtnLXXBoth";
            this.rbtnLXXBoth.Size = new System.Drawing.Size(180, 17);
            this.rbtnLXXBoth.TabIndex = 5;
            this.rbtnLXXBoth.Text = "Displayed as both Grey and Italic";
            this.rbtnLXXBoth.UseVisualStyleBackColor = true;
            this.rbtnLXXBoth.CheckedChanged += new System.EventHandler(this.rbtnLXX_CheckedChanged);
            // 
            // rbtnLXXItalic
            // 
            this.rbtnLXXItalic.AutoSize = true;
            this.rbtnLXXItalic.Location = new System.Drawing.Point(37, 36);
            this.rbtnLXXItalic.Name = "rbtnLXXItalic";
            this.rbtnLXXItalic.Size = new System.Drawing.Size(146, 17);
            this.rbtnLXXItalic.TabIndex = 4;
            this.rbtnLXXItalic.Text = "Displayed as Italic (Black)";
            this.rbtnLXXItalic.UseVisualStyleBackColor = true;
            this.rbtnLXXItalic.CheckedChanged += new System.EventHandler(this.rbtnLXX_CheckedChanged);
            // 
            // rbtnLXXGrey
            // 
            this.rbtnLXXGrey.AutoSize = true;
            this.rbtnLXXGrey.Checked = true;
            this.rbtnLXXGrey.Location = new System.Drawing.Point(37, 19);
            this.rbtnLXXGrey.Name = "rbtnLXXGrey";
            this.rbtnLXXGrey.Size = new System.Drawing.Size(130, 17);
            this.rbtnLXXGrey.TabIndex = 3;
            this.rbtnLXXGrey.TabStop = true;
            this.rbtnLXXGrey.Text = "Displayed as Grey text";
            this.rbtnLXXGrey.UseVisualStyleBackColor = true;
            this.rbtnLXXGrey.CheckedChanged += new System.EventHandler(this.rbtnLXX_CheckedChanged);
            // 
            // gbNTAndLXXX
            // 
            this.gbNTAndLXXX.Controls.Add(this.rbtnBothNormal);
            this.gbNTAndLXXX.Controls.Add(this.rbtnBothOrange);
            this.gbNTAndLXXX.Location = new System.Drawing.Point(26, 192);
            this.gbNTAndLXXX.Name = "gbNTAndLXXX";
            this.gbNTAndLXXX.Size = new System.Drawing.Size(285, 61);
            this.gbNTAndLXXX.TabIndex = 2;
            this.gbNTAndLXXX.TabStop = false;
            this.gbNTAndLXXX.Text = "Words found in both the NT and the Septuagint (LXX): ";
            // 
            // rbtnBothNormal
            // 
            this.rbtnBothNormal.AutoSize = true;
            this.rbtnBothNormal.Location = new System.Drawing.Point(37, 36);
            this.rbtnBothNormal.Name = "rbtnBothNormal";
            this.rbtnBothNormal.Size = new System.Drawing.Size(155, 17);
            this.rbtnBothNormal.TabIndex = 4;
            this.rbtnBothNormal.Text = "Displayed as normal (Black)";
            this.rbtnBothNormal.UseVisualStyleBackColor = true;
            this.rbtnBothNormal.CheckedChanged += new System.EventHandler(this.rbtnBoth_CheckedChanged);
            // 
            // rbtnBothOrange
            // 
            this.rbtnBothOrange.AutoSize = true;
            this.rbtnBothOrange.Checked = true;
            this.rbtnBothOrange.Location = new System.Drawing.Point(37, 19);
            this.rbtnBothOrange.Name = "rbtnBothOrange";
            this.rbtnBothOrange.Size = new System.Drawing.Size(143, 17);
            this.rbtnBothOrange.TabIndex = 3;
            this.rbtnBothOrange.TabStop = true;
            this.rbtnBothOrange.Text = "Displayed as Orange text";
            this.rbtnBothOrange.UseVisualStyleBackColor = true;
            this.rbtnBothOrange.CheckedChanged += new System.EventHandler(this.rbtnBoth_CheckedChanged);
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(407, 343);
            this.ControlBox = false;
            this.Controls.Add(this.gbPresentation);
            this.Controls.Add(this.pnlBase);
            this.Controls.Add(this.cbLXX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NTWordUse Options";
            this.pnlBase.ResumeLayout(false);
            this.gbPresentation.ResumeLayout(false);
            this.gbNTOnly.ResumeLayout(false);
            this.gbNTOnly.PerformLayout();
            this.gbLXXOnly.ResumeLayout(false);
            this.gbLXXOnly.PerformLayout();
            this.gbNTAndLXXX.ResumeLayout(false);
            this.gbNTAndLXXX.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbLXX;
        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbPresentation;
        private System.Windows.Forms.GroupBox gbNTAndLXXX;
        private System.Windows.Forms.RadioButton rbtnBothNormal;
        private System.Windows.Forms.RadioButton rbtnBothOrange;
        private System.Windows.Forms.GroupBox gbLXXOnly;
        private System.Windows.Forms.RadioButton rbtnLXXBoth;
        private System.Windows.Forms.RadioButton rbtnLXXItalic;
        private System.Windows.Forms.RadioButton rbtnLXXGrey;
        private System.Windows.Forms.GroupBox gbNTOnly;
        private System.Windows.Forms.RadioButton rbtnNTBoth;
        private System.Windows.Forms.RadioButton rbtnNTBold;
        private System.Windows.Forms.RadioButton rbtnNTRed;
    }
}