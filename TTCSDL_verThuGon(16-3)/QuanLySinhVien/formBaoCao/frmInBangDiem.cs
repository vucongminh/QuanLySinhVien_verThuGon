﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien.formBaoCao
{
    public partial class frmInBangDiem : Form
    {
        public static string username = string.Empty;
        public frmInBangDiem()
        {
            InitializeComponent();
        }

        private void frmInBangDiem_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetInBangDiem1.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.DataSetInBangDiem1.DataTable1,username);

            this.reportViewer1.RefreshReport();
        }
    }
}
