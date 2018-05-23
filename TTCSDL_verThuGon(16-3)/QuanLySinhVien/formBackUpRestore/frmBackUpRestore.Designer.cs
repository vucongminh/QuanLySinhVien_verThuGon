namespace QuanLySinhVien
{
    partial class frmBackUpRestore
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNameFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnThoatBackUp = new System.Windows.Forms.Button();
            this.btnBackUp = new System.Windows.Forms.Button();
            this.btnBrowseBU = new System.Windows.Forms.Button();
            this.txtBackUp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnThoanRestore = new System.Windows.Forms.Button();
            this.btnBrowseRS = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.txtRestore = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNameFile);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnThoatBackUp);
            this.groupBox1.Controls.Add(this.btnBackUp);
            this.groupBox1.Controls.Add(this.btnBrowseBU);
            this.groupBox1.Controls.Add(this.txtBackUp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BackUp DB";
            // 
            // txtNameFile
            // 
            this.txtNameFile.Location = new System.Drawing.Point(85, 59);
            this.txtNameFile.Name = "txtNameFile";
            this.txtNameFile.Size = new System.Drawing.Size(317, 20);
            this.txtNameFile.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tên File";
            // 
            // btnThoatBackUp
            // 
            this.btnThoatBackUp.Location = new System.Drawing.Point(421, 88);
            this.btnThoatBackUp.Name = "btnThoatBackUp";
            this.btnThoatBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnThoatBackUp.TabIndex = 2;
            this.btnThoatBackUp.Text = "Thoát";
            this.btnThoatBackUp.UseVisualStyleBackColor = true;
            this.btnThoatBackUp.Click += new System.EventHandler(this.btnThoatBackUp_Click);
            // 
            // btnBackUp
            // 
            this.btnBackUp.Enabled = false;
            this.btnBackUp.Location = new System.Drawing.Point(421, 59);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(75, 23);
            this.btnBackUp.TabIndex = 3;
            this.btnBackUp.Text = "BackUp";
            this.btnBackUp.UseVisualStyleBackColor = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // btnBrowseBU
            // 
            this.btnBrowseBU.Location = new System.Drawing.Point(421, 30);
            this.btnBrowseBU.Name = "btnBrowseBU";
            this.btnBrowseBU.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBU.TabIndex = 2;
            this.btnBrowseBU.Text = "Thêm";
            this.btnBrowseBU.UseVisualStyleBackColor = true;
            this.btnBrowseBU.Click += new System.EventHandler(this.btnBrowseBU_Click);
            // 
            // txtBackUp
            // 
            this.txtBackUp.Enabled = false;
            this.txtBackUp.Location = new System.Drawing.Point(85, 30);
            this.txtBackUp.Name = "txtBackUp";
            this.txtBackUp.Size = new System.Drawing.Size(317, 20);
            this.txtBackUp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đường Dẫn";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnThoanRestore);
            this.groupBox2.Controls.Add(this.btnBrowseRS);
            this.groupBox2.Controls.Add(this.btnRestore);
            this.groupBox2.Controls.Add(this.txtRestore);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(526, 135);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Restore DB";
            // 
            // btnThoanRestore
            // 
            this.btnThoanRestore.Location = new System.Drawing.Point(421, 96);
            this.btnThoanRestore.Name = "btnThoanRestore";
            this.btnThoanRestore.Size = new System.Drawing.Size(75, 23);
            this.btnThoanRestore.TabIndex = 4;
            this.btnThoanRestore.Text = "Thoát";
            this.btnThoanRestore.UseVisualStyleBackColor = true;
            this.btnThoanRestore.Click += new System.EventHandler(this.btnThoanRestore_Click);
            // 
            // btnBrowseRS
            // 
            this.btnBrowseRS.Location = new System.Drawing.Point(421, 36);
            this.btnBrowseRS.Name = "btnBrowseRS";
            this.btnBrowseRS.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseRS.TabIndex = 4;
            this.btnBrowseRS.Text = "Thêm";
            this.btnBrowseRS.UseVisualStyleBackColor = true;
            this.btnBrowseRS.Click += new System.EventHandler(this.btnBrowseRS_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(421, 65);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 5;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // txtRestore
            // 
            this.txtRestore.Enabled = false;
            this.txtRestore.Location = new System.Drawing.Point(85, 36);
            this.txtRestore.Name = "txtRestore";
            this.txtRestore.Size = new System.Drawing.Size(317, 20);
            this.txtRestore.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đường Dẫn";
            // 
            // frmBackUpRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 281);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "frmBackUpRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BackUp & Restore";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNameFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThoatBackUp;
        private System.Windows.Forms.Button btnBackUp;
        private System.Windows.Forms.Button btnBrowseBU;
        private System.Windows.Forms.TextBox txtBackUp;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnThoanRestore;
        private System.Windows.Forms.Button btnBrowseRS;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.TextBox txtRestore;
        private System.Windows.Forms.Label label2;
    }
}