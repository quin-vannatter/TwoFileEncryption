namespace FileBuilderV2
{
    partial class FileHider
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butClose = new System.Windows.Forms.Button();
            this.labInfo = new System.Windows.Forms.Label();
            this.pBpercent = new System.Windows.Forms.ProgressBar();
            this.butStart = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FileBuilderV2.Properties.Resources._4T9zBpp8c;
            this.pictureBox1.Location = new System.Drawing.Point(131, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // butClose
            // 
            this.butClose.Location = new System.Drawing.Point(312, 271);
            this.butClose.Name = "butClose";
            this.butClose.Size = new System.Drawing.Size(75, 23);
            this.butClose.TabIndex = 1;
            this.butClose.Text = "Close";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new System.EventHandler(this.butClose_Click);
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.Location = new System.Drawing.Point(9, 9);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(58, 13);
            this.labInfo.TabIndex = 2;
            this.labInfo.Text = "information";
            // 
            // pBpercent
            // 
            this.pBpercent.Location = new System.Drawing.Point(12, 300);
            this.pBpercent.Name = "pBpercent";
            this.pBpercent.Size = new System.Drawing.Size(375, 23);
            this.pBpercent.TabIndex = 3;
            // 
            // butStart
            // 
            this.butStart.Location = new System.Drawing.Point(150, 271);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 23);
            this.butStart.TabIndex = 4;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(231, 271);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 5;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // FileHider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 335);
            this.ControlBox = false;
            this.Controls.Add(this.labInfo);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butStart);
            this.Controls.Add(this.pBpercent);
            this.Controls.Add(this.butClose);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FileHider";
            this.Text = "File Hider";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butClose;
        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.ProgressBar pBpercent;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Button butCancel;
    }
}

