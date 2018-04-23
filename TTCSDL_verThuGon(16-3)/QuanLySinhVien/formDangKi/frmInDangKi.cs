using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace QuanLySinhVien.formDangKi
{
    public partial class frmInDangKi : Form
    {
        public static string username = string.Empty;
        public frmInDangKi()
        {
            InitializeComponent();
        }

        private void frmInDangKi_Load(object sender, EventArgs e)
        {
            this.banDangKiTableAdapter.Fill(this.dataSetInDangKi1.BanDangKi,username);
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
