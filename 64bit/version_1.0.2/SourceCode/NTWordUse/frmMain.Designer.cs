
namespace NTWordUse
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlBase = new System.Windows.Forms.Panel();
            this.btnOptions = new System.Windows.Forms.Button();
            this.Conjugate = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.labTextEnteredMsg = new System.Windows.Forms.Label();
            this.labTextEnteredLbl = new System.Windows.Forms.Label();
            this.gbVerbAction = new System.Windows.Forms.GroupBox();
            this.gbMood = new System.Windows.Forms.GroupBox();
            this.labZeroSelect4Lbl = new System.Windows.Forms.Label();
            this.labZeroSelect3Lbl = new System.Windows.Forms.Label();
            this.rbtnPrepositions = new System.Windows.Forms.RadioButton();
            this.rbtnMain = new System.Windows.Forms.RadioButton();
            this.btnSelectMood = new System.Windows.Forms.Button();
            this.gbTenses = new System.Windows.Forms.GroupBox();
            this.labZeroSelect2Lbl = new System.Windows.Forms.Label();
            this.btnTenseSelect = new System.Windows.Forms.Button();
            this.labZeroSelect1Lbl = new System.Windows.Forms.Label();
            this.pnlKeys = new System.Windows.Forms.Panel();
            this.labKeys = new System.Windows.Forms.Label();
            this.gbGrammarClass = new System.Windows.Forms.GroupBox();
            this.lbRootList = new System.Windows.Forms.ListBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.pnlBase.SuspendLayout();
            this.gbVerbAction.SuspendLayout();
            this.gbMood.SuspendLayout();
            this.gbTenses.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.btnAbout);
            this.pnlBase.Controls.Add(this.btnOptions);
            this.pnlBase.Controls.Add(this.Conjugate);
            this.pnlBase.Controls.Add(this.btnHelp);
            this.pnlBase.Controls.Add(this.btnClose);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBase.Location = new System.Drawing.Point(0, 628);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(377, 30);
            this.pnlBase.TabIndex = 3;
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(73, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(54, 23);
            this.btnOptions.TabIndex = 3;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // Conjugate
            // 
            this.Conjugate.Location = new System.Drawing.Point(12, 3);
            this.Conjugate.Name = "Conjugate";
            this.Conjugate.Size = new System.Drawing.Size(54, 23);
            this.Conjugate.TabIndex = 2;
            this.Conjugate.Text = "Analyse";
            this.Conjugate.UseVisualStyleBackColor = true;
            this.Conjugate.Click += new System.EventHandler(this.Conjugate_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(251, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(54, 23);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(311, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(54, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labTextEnteredMsg
            // 
            this.labTextEnteredMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labTextEnteredMsg.AutoSize = true;
            this.labTextEnteredMsg.Location = new System.Drawing.Point(94, 609);
            this.labTextEnteredMsg.Name = "labTextEnteredMsg";
            this.labTextEnteredMsg.Size = new System.Drawing.Size(33, 13);
            this.labTextEnteredMsg.TabIndex = 38;
            this.labTextEnteredMsg.Text = "None";
            // 
            // labTextEnteredLbl
            // 
            this.labTextEnteredLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labTextEnteredLbl.AutoSize = true;
            this.labTextEnteredLbl.Location = new System.Drawing.Point(18, 609);
            this.labTextEnteredLbl.Name = "labTextEnteredLbl";
            this.labTextEnteredLbl.Size = new System.Drawing.Size(70, 13);
            this.labTextEnteredLbl.TabIndex = 37;
            this.labTextEnteredLbl.Text = "Text entered:";
            // 
            // gbVerbAction
            // 
            this.gbVerbAction.Controls.Add(this.gbMood);
            this.gbVerbAction.Controls.Add(this.gbTenses);
            this.gbVerbAction.Location = new System.Drawing.Point(381, 13);
            this.gbVerbAction.Name = "gbVerbAction";
            this.gbVerbAction.Size = new System.Drawing.Size(376, 381);
            this.gbVerbAction.TabIndex = 35;
            this.gbVerbAction.TabStop = false;
            this.gbVerbAction.Text = "Verb Action: ";
            // 
            // gbMood
            // 
            this.gbMood.Controls.Add(this.labZeroSelect4Lbl);
            this.gbMood.Controls.Add(this.labZeroSelect3Lbl);
            this.gbMood.Controls.Add(this.rbtnPrepositions);
            this.gbMood.Controls.Add(this.rbtnMain);
            this.gbMood.Controls.Add(this.btnSelectMood);
            this.gbMood.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMood.Location = new System.Drawing.Point(198, 19);
            this.gbMood.Name = "gbMood";
            this.gbMood.Size = new System.Drawing.Size(164, 278);
            this.gbMood.TabIndex = 1;
            this.gbMood.TabStop = false;
            this.gbMood.Text = "Required mood(s): ";
            // 
            // labZeroSelect4Lbl
            // 
            this.labZeroSelect4Lbl.AutoSize = true;
            this.labZeroSelect4Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labZeroSelect4Lbl.Location = new System.Drawing.Point(15, 213);
            this.labZeroSelect4Lbl.Name = "labZeroSelect4Lbl";
            this.labZeroSelect4Lbl.Size = new System.Drawing.Size(129, 13);
            this.labZeroSelect4Lbl.TabIndex = 5;
            this.labZeroSelect4Lbl.Text = "all moods will be selected.";
            // 
            // labZeroSelect3Lbl
            // 
            this.labZeroSelect3Lbl.AutoSize = true;
            this.labZeroSelect3Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labZeroSelect3Lbl.Location = new System.Drawing.Point(6, 194);
            this.labZeroSelect3Lbl.Name = "labZeroSelect3Lbl";
            this.labZeroSelect3Lbl.Size = new System.Drawing.Size(158, 13);
            this.labZeroSelect3Lbl.TabIndex = 4;
            this.labZeroSelect3Lbl.Text = "If all moods are left unchecked, ";
            // 
            // rbtnPrepositions
            // 
            this.rbtnPrepositions.AutoSize = true;
            this.rbtnPrepositions.Location = new System.Drawing.Point(24, 239);
            this.rbtnPrepositions.Name = "rbtnPrepositions";
            this.rbtnPrepositions.Size = new System.Drawing.Size(122, 23);
            this.rbtnPrepositions.TabIndex = 2;
            this.rbtnPrepositions.TabStop = true;
            this.rbtnPrepositions.Text = "Participles Only";
            this.rbtnPrepositions.UseVisualStyleBackColor = true;
            // 
            // rbtnMain
            // 
            this.rbtnMain.AutoSize = true;
            this.rbtnMain.Checked = true;
            this.rbtnMain.Location = new System.Drawing.Point(24, 20);
            this.rbtnMain.Name = "rbtnMain";
            this.rbtnMain.Size = new System.Drawing.Size(106, 23);
            this.rbtnMain.TabIndex = 1;
            this.rbtnMain.TabStop = true;
            this.rbtnMain.Text = "Main Moods";
            this.rbtnMain.UseVisualStyleBackColor = true;
            // 
            // btnSelectMood
            // 
            this.btnSelectMood.Location = new System.Drawing.Point(60, 43);
            this.btnSelectMood.Name = "btnSelectMood";
            this.btnSelectMood.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMood.TabIndex = 0;
            this.btnSelectMood.Text = "Select All";
            this.btnSelectMood.UseVisualStyleBackColor = true;
            this.btnSelectMood.Click += new System.EventHandler(this.btnSelectMood_Click);
            // 
            // gbTenses
            // 
            this.gbTenses.Controls.Add(this.labZeroSelect2Lbl);
            this.gbTenses.Controls.Add(this.btnTenseSelect);
            this.gbTenses.Controls.Add(this.labZeroSelect1Lbl);
            this.gbTenses.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTenses.Location = new System.Drawing.Point(19, 19);
            this.gbTenses.Name = "gbTenses";
            this.gbTenses.Size = new System.Drawing.Size(164, 262);
            this.gbTenses.TabIndex = 0;
            this.gbTenses.TabStop = false;
            this.gbTenses.Text = "Required tense(s): ";
            // 
            // labZeroSelect2Lbl
            // 
            this.labZeroSelect2Lbl.AutoSize = true;
            this.labZeroSelect2Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labZeroSelect2Lbl.Location = new System.Drawing.Point(15, 232);
            this.labZeroSelect2Lbl.Name = "labZeroSelect2Lbl";
            this.labZeroSelect2Lbl.Size = new System.Drawing.Size(129, 13);
            this.labZeroSelect2Lbl.TabIndex = 3;
            this.labZeroSelect2Lbl.Text = "all tenses will be selected.";
            // 
            // btnTenseSelect
            // 
            this.btnTenseSelect.Location = new System.Drawing.Point(38, 29);
            this.btnTenseSelect.Name = "btnTenseSelect";
            this.btnTenseSelect.Size = new System.Drawing.Size(75, 23);
            this.btnTenseSelect.TabIndex = 0;
            this.btnTenseSelect.Text = "Select All";
            this.btnTenseSelect.UseVisualStyleBackColor = true;
            this.btnTenseSelect.Click += new System.EventHandler(this.btnTenseSelect_Click);
            // 
            // labZeroSelect1Lbl
            // 
            this.labZeroSelect1Lbl.AutoSize = true;
            this.labZeroSelect1Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labZeroSelect1Lbl.Location = new System.Drawing.Point(6, 213);
            this.labZeroSelect1Lbl.Name = "labZeroSelect1Lbl";
            this.labZeroSelect1Lbl.Size = new System.Drawing.Size(155, 13);
            this.labZeroSelect1Lbl.TabIndex = 2;
            this.labZeroSelect1Lbl.Text = "If all tenses are left unchecked,";
            // 
            // pnlKeys
            // 
            this.pnlKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlKeys.Location = new System.Drawing.Point(15, 408);
            this.pnlKeys.Name = "pnlKeys";
            this.pnlKeys.Size = new System.Drawing.Size(347, 189);
            this.pnlKeys.TabIndex = 34;
            // 
            // labKeys
            // 
            this.labKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labKeys.AutoSize = true;
            this.labKeys.Location = new System.Drawing.Point(26, 387);
            this.labKeys.Name = "labKeys";
            this.labKeys.Size = new System.Drawing.Size(279, 13);
            this.labKeys.TabIndex = 33;
            this.labKeys.Text = "Simple keyboard (ignores accents and upper/lower case):";
            // 
            // gbGrammarClass
            // 
            this.gbGrammarClass.Location = new System.Drawing.Point(191, 13);
            this.gbGrammarClass.Name = "gbGrammarClass";
            this.gbGrammarClass.Size = new System.Drawing.Size(175, 365);
            this.gbGrammarClass.TabIndex = 32;
            this.gbGrammarClass.TabStop = false;
            this.gbGrammarClass.Text = "Grammatical Categories:  ";
            // 
            // lbRootList
            // 
            this.lbRootList.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRootList.FormattingEnabled = true;
            this.lbRootList.ItemHeight = 19;
            this.lbRootList.Location = new System.Drawing.Point(15, 13);
            this.lbRootList.Name = "lbRootList";
            this.lbRootList.Size = new System.Drawing.Size(170, 365);
            this.lbRootList.Sorted = true;
            this.lbRootList.TabIndex = 31;
            this.lbRootList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbRootList_MouseDoubleClick);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbout.Location = new System.Drawing.Point(191, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(54, 23);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 658);
            this.Controls.Add(this.labTextEnteredMsg);
            this.Controls.Add(this.labTextEnteredLbl);
            this.Controls.Add(this.gbVerbAction);
            this.Controls.Add(this.pnlKeys);
            this.Controls.Add(this.labKeys);
            this.Controls.Add(this.gbGrammarClass);
            this.Controls.Add(this.lbRootList);
            this.Controls.Add(this.pnlBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyse details of verbs, nouns, etc.";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.pnlBase.ResumeLayout(false);
            this.gbVerbAction.ResumeLayout(false);
            this.gbMood.ResumeLayout(false);
            this.gbMood.PerformLayout();
            this.gbTenses.ResumeLayout(false);
            this.gbTenses.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Button Conjugate;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labTextEnteredMsg;
        private System.Windows.Forms.Label labTextEnteredLbl;
        private System.Windows.Forms.GroupBox gbVerbAction;
        private System.Windows.Forms.GroupBox gbMood;
        private System.Windows.Forms.Label labZeroSelect4Lbl;
        private System.Windows.Forms.Label labZeroSelect3Lbl;
        private System.Windows.Forms.RadioButton rbtnPrepositions;
        private System.Windows.Forms.RadioButton rbtnMain;
        private System.Windows.Forms.Button btnSelectMood;
        private System.Windows.Forms.GroupBox gbTenses;
        private System.Windows.Forms.Label labZeroSelect2Lbl;
        private System.Windows.Forms.Button btnTenseSelect;
        private System.Windows.Forms.Label labZeroSelect1Lbl;
        private System.Windows.Forms.Panel pnlKeys;
        private System.Windows.Forms.Label labKeys;
        private System.Windows.Forms.GroupBox gbGrammarClass;
        private System.Windows.Forms.ListBox lbRootList;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnAbout;
    }
}

