namespace QuanLySinhVien.formDangKi
{
    partial class frmInDangKi
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInDangKi));
            this.banDangKiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetInDangKi1 = new QuanLySinhVien.DataSetInDangKi1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.banDangKiTableAdapter = new QuanLySinhVien.DataSetInDangKi1TableAdapters.BanDangKiTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.banDangKiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetInDangKi1)).BeginInit();
            this.SuspendLayout();
            // 
            // banDangKiBindingSource
            // 
            this.banDangKiBindingSource.DataMember = "BanDangKi";
            this.banDangKiBindingSource.DataSource = this.dataSetInDangKi1;
            // 
            // dataSetInDangKi1
            // 
            this.dataSetInDangKi1.DataSetName = "DataSetInDangKi1";
            this.dataSetInDangKi1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "InDangKi1";
            reportDataSource1.Value = this.banDangKiBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLySinhVien.formDangKi.ReportInDangKi.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(707, 505);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // banDangKiTableAdapter
            // 
            this.banDangKiTableAdapter.ClearBeforeFill = true;
            // 
            // frmInDangKi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 505);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmInDangKi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In Đăng Ký";
            this.Load += new System.EventHandler(this.frmInDangKi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.banDangKiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetInDangKi1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource banDangKiBindingSource;
        private DataSetInDangKi1 dataSetInDangKi1;
        private DataSetInDangKi1TableAdapters.BanDangKiTableAdapter banDangKiTableAdapter;
    }
}