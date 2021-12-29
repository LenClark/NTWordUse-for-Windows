
namespace NTWordUse
{
    partial class frmUsage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsage));
            this.pnlBase = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabCtrlParadigm = new System.Windows.Forms.TabControl();
            this.tPgeMain = new System.Windows.Forms.TabPage();
            this.dgvUsage = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.pnlKey = new System.Windows.Forms.Panel();
            this.gbColours = new System.Windows.Forms.GroupBox();
            this.labKeyNTAndLXX = new System.Windows.Forms.Label();
            this.labKeyLXX = new System.Windows.Forms.Label();
            this.labKeyNTOnly = new System.Windows.Forms.Label();
            this.labKeyNTOnly2 = new System.Windows.Forms.Label();
            this.labKeyLXXOnly2 = new System.Windows.Forms.Label();
            this.labKeyBoth2 = new System.Windows.Forms.Label();
            this.labKeyNTOnly3 = new System.Windows.Forms.Label();
            this.labKeyLXXOnly3 = new System.Windows.Forms.Label();
            this.pnlBase.SuspendLayout();
            this.tabCtrlParadigm.SuspendLayout();
            this.tPgeMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsage)).BeginInit();
            this.pnlKey.SuspendLayout();
            this.gbColours.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.btnSave);
            this.pnlBase.Controls.Add(this.btnClose);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBase.Location = new System.Drawing.Point(0, 517);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(769, 28);
            this.pnlBase.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save as CSV";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(682, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabCtrlParadigm
            // 
            this.tabCtrlParadigm.Controls.Add(this.tPgeMain);
            this.tabCtrlParadigm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlParadigm.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlParadigm.Name = "tabCtrlParadigm";
            this.tabCtrlParadigm.SelectedIndex = 0;
            this.tabCtrlParadigm.Size = new System.Drawing.Size(769, 426);
            this.tabCtrlParadigm.TabIndex = 4;
            // 
            // tPgeMain
            // 
            this.tPgeMain.Controls.Add(this.dgvUsage);
            this.tPgeMain.Location = new System.Drawing.Point(4, 22);
            this.tPgeMain.Name = "tPgeMain";
            this.tPgeMain.Padding = new System.Windows.Forms.Padding(3);
            this.tPgeMain.Size = new System.Drawing.Size(761, 400);
            this.tPgeMain.TabIndex = 0;
            this.tPgeMain.Text = "Noun";
            this.tPgeMain.UseVisualStyleBackColor = true;
            // 
            // dgvUsage
            // 
            this.dgvUsage.AllowUserToAddRows = false;
            this.dgvUsage.AllowUserToDeleteRows = false;
            this.dgvUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvUsage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsage.Location = new System.Drawing.Point(3, 3);
            this.dgvUsage.Name = "dgvUsage";
            this.dgvUsage.ReadOnly = true;
            this.dgvUsage.RowTemplate.Height = 24;
            this.dgvUsage.Size = new System.Drawing.Size(755, 394);
            this.dgvUsage.TabIndex = 2;
            this.dgvUsage.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUsage_CellMouseClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "csv";
            this.dlgSave.FileName = "NTUsage.csv";
            this.dlgSave.Filter = "\"Tab delimited CSV\"|*.csv|\"Text file\"|*.txt|\"Doc file\"|*.doc|\"All files\"|*.*";
            this.dlgSave.Title = "Save current word details in CSV format";
            // 
            // pnlKey
            // 
            this.pnlKey.Controls.Add(this.gbColours);
            this.pnlKey.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlKey.Location = new System.Drawing.Point(0, 426);
            this.pnlKey.Name = "pnlKey";
            this.pnlKey.Size = new System.Drawing.Size(769, 91);
            this.pnlKey.TabIndex = 5;
            // 
            // gbColours
            // 
            this.gbColours.Controls.Add(this.labKeyLXXOnly3);
            this.gbColours.Controls.Add(this.labKeyNTOnly3);
            this.gbColours.Controls.Add(this.labKeyBoth2);
            this.gbColours.Controls.Add(this.labKeyLXXOnly2);
            this.gbColours.Controls.Add(this.labKeyNTOnly2);
            this.gbColours.Controls.Add(this.labKeyNTAndLXX);
            this.gbColours.Controls.Add(this.labKeyLXX);
            this.gbColours.Controls.Add(this.labKeyNTOnly);
            this.gbColours.Location = new System.Drawing.Point(12, 6);
            this.gbColours.Name = "gbColours";
            this.gbColours.Size = new System.Drawing.Size(682, 79);
            this.gbColours.TabIndex = 4;
            this.gbColours.TabStop = false;
            this.gbColours.Text = "Forms will be shown as follows: ";
            // 
            // labKeyNTAndLXX
            // 
            this.labKeyNTAndLXX.AutoSize = true;
            this.labKeyNTAndLXX.ForeColor = System.Drawing.Color.Orange;
            this.labKeyNTAndLXX.Location = new System.Drawing.Point(20, 56);
            this.labKeyNTAndLXX.Name = "labKeyNTAndLXX";
            this.labKeyNTAndLXX.Size = new System.Drawing.Size(180, 13);
            this.labKeyNTAndLXX.TabIndex = 6;
            this.labKeyNTAndLXX.Text = "Orange, if found in both NT and LXX";
            // 
            // labKeyLXX
            // 
            this.labKeyLXX.AutoSize = true;
            this.labKeyLXX.ForeColor = System.Drawing.Color.Gray;
            this.labKeyLXX.Location = new System.Drawing.Point(20, 37);
            this.labKeyLXX.Name = "labKeyLXX";
            this.labKeyLXX.Size = new System.Drawing.Size(144, 13);
            this.labKeyLXX.TabIndex = 5;
            this.labKeyLXX.Text = "Grey, if only found in the LXX";
            // 
            // labKeyNTOnly
            // 
            this.labKeyNTOnly.AutoSize = true;
            this.labKeyNTOnly.ForeColor = System.Drawing.Color.Red;
            this.labKeyNTOnly.Location = new System.Drawing.Point(20, 18);
            this.labKeyNTOnly.Name = "labKeyNTOnly";
            this.labKeyNTOnly.Size = new System.Drawing.Size(197, 13);
            this.labKeyNTOnly.TabIndex = 4;
            this.labKeyNTOnly.Text = "Red, if only found in the New Testament";
            // 
            // labKeyNTOnly2
            // 
            this.labKeyNTOnly2.AutoSize = true;
            this.labKeyNTOnly2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labKeyNTOnly2.Location = new System.Drawing.Point(244, 18);
            this.labKeyNTOnly2.Name = "labKeyNTOnly2";
            this.labKeyNTOnly2.Size = new System.Drawing.Size(112, 13);
            this.labKeyNTOnly2.TabIndex = 7;
            this.labKeyNTOnly2.Text = "or, optionally, bold";
            // 
            // labKeyLXXOnly2
            // 
            this.labKeyLXXOnly2.AutoSize = true;
            this.labKeyLXXOnly2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labKeyLXXOnly2.Location = new System.Drawing.Point(244, 37);
            this.labKeyLXXOnly2.Name = "labKeyLXXOnly2";
            this.labKeyLXXOnly2.Size = new System.Drawing.Size(93, 13);
            this.labKeyLXXOnly2.TabIndex = 8;
            this.labKeyLXXOnly2.Text = "or, optionally, italic";
            // 
            // labKeyBoth2
            // 
            this.labKeyBoth2.AutoSize = true;
            this.labKeyBoth2.Location = new System.Drawing.Point(244, 56);
            this.labKeyBoth2.Name = "labKeyBoth2";
            this.labKeyBoth2.Size = new System.Drawing.Size(123, 13);
            this.labKeyBoth2.TabIndex = 9;
            this.labKeyBoth2.Text = "or, optionally, normal text";
            // 
            // labKeyNTOnly3
            // 
            this.labKeyNTOnly3.AutoSize = true;
            this.labKeyNTOnly3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labKeyNTOnly3.ForeColor = System.Drawing.Color.Red;
            this.labKeyNTOnly3.Location = new System.Drawing.Point(394, 18);
            this.labKeyNTOnly3.Name = "labKeyNTOnly3";
            this.labKeyNTOnly3.Size = new System.Drawing.Size(79, 13);
            this.labKeyNTOnly3.TabIndex = 10;
            this.labKeyNTOnly3.Text = "or even both";
            // 
            // labKeyLXXOnly3
            // 
            this.labKeyLXXOnly3.AutoSize = true;
            this.labKeyLXXOnly3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labKeyLXXOnly3.ForeColor = System.Drawing.Color.Gray;
            this.labKeyLXXOnly3.Location = new System.Drawing.Point(394, 37);
            this.labKeyLXXOnly3.Name = "labKeyLXXOnly3";
            this.labKeyLXXOnly3.Size = new System.Drawing.Size(67, 13);
            this.labKeyLXXOnly3.TabIndex = 11;
            this.labKeyLXXOnly3.Text = "or even both";
            // 
            // frmUsage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 545);
            this.Controls.Add(this.tabCtrlParadigm);
            this.Controls.Add(this.pnlKey);
            this.Controls.Add(this.pnlBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUsage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Testament Usage";
            this.pnlBase.ResumeLayout(false);
            this.tabCtrlParadigm.ResumeLayout(false);
            this.tPgeMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsage)).EndInit();
            this.pnlKey.ResumeLayout(false);
            this.gbColours.ResumeLayout(false);
            this.gbColours.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabCtrlParadigm;
        private System.Windows.Forms.TabPage tPgeMain;
        private System.Windows.Forms.DataGridView dgvUsage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Panel pnlKey;
        private System.Windows.Forms.GroupBox gbColours;
        private System.Windows.Forms.Label labKeyNTAndLXX;
        private System.Windows.Forms.Label labKeyLXX;
        private System.Windows.Forms.Label labKeyNTOnly;
        private System.Windows.Forms.Label labKeyLXXOnly3;
        private System.Windows.Forms.Label labKeyNTOnly3;
        private System.Windows.Forms.Label labKeyBoth2;
        private System.Windows.Forms.Label labKeyLXXOnly2;
        private System.Windows.Forms.Label labKeyNTOnly2;
    }
}