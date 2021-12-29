
namespace NTWordUse
{
    partial class frmProgress
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
            this.labPatientLbl = new System.Windows.Forms.Label();
            this.labInitialLbl = new System.Windows.Forms.Label();
            this.progInitial = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labPatientLbl
            // 
            this.labPatientLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labPatientLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatientLbl.ForeColor = System.Drawing.Color.Red;
            this.labPatientLbl.Location = new System.Drawing.Point(12, 60);
            this.labPatientLbl.Name = "labPatientLbl";
            this.labPatientLbl.Size = new System.Drawing.Size(426, 23);
            this.labPatientLbl.TabIndex = 21;
            this.labPatientLbl.Text = "Please be Patient";
            this.labPatientLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labInitialLbl
            // 
            this.labInitialLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labInitialLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labInitialLbl.Location = new System.Drawing.Point(12, 27);
            this.labInitialLbl.Name = "labInitialLbl";
            this.labInitialLbl.Size = new System.Drawing.Size(426, 23);
            this.labInitialLbl.TabIndex = 20;
            this.labInitialLbl.Text = "Initialising the Application";
            this.labInitialLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progInitial
            // 
            this.progInitial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progInitial.Location = new System.Drawing.Point(12, 99);
            this.progInitial.Maximum = 68;
            this.progInitial.Name = "progInitial";
            this.progInitial.Size = new System.Drawing.Size(426, 23);
            this.progInitial.Step = 1;
            this.progInitial.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progInitial.TabIndex = 19;
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 148);
            this.ControlBox = false;
            this.Controls.Add(this.labPatientLbl);
            this.Controls.Add(this.labInitialLbl);
            this.Controls.Add(this.progInitial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NTWordUse - Initialisation Progress";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labPatientLbl;
        private System.Windows.Forms.Label labInitialLbl;
        private System.Windows.Forms.ProgressBar progInitial;
    }
}