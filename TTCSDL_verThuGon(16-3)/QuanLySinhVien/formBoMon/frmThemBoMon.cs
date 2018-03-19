using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Data.SqlClient;

namespace QuanLySinhVien
{
    public partial class frmThemBoMon : Form
    {
        public frmThemBoMon()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSBoMon frm = new frmDSBoMon();
            frm.Show();
        }

        private void btnThemKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                //cmd.CommandText = "INSERT INTO BOMON VALUES('" + txtMaKhoa.Text + "','" + txtTenKhoa.Text + "','" + txtMaCNBM.Text + "')";
                cmd.CommandText = "InsertDataIntoBoMon";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MaBM", txtMaKhoa.Text);
                cmd.Parameters.AddWithValue("TenBM", txtTenKhoa.Text);
                cmd.Parameters.AddWithValue("MaChuNhiemBM", txtMaCNBM.Text);
                cmd.ExecuteNonQuery();
                DialogResult result;
                result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.Close();
                    frmThemBoMon frm = new frmThemBoMon();
                    frm.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            }
        }

        private void frmThemKhoa_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
