using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class frmThemGV : Form
    {
        public frmThemGV()
        {
            InitializeComponent();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO GIAOVIEN VALUES('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','"+txtSdtGV.Text+ "','" + txtMaBM.Text + "')";
                //cmd.CommandText = "InsertDataIntoLop";
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MaGV", txtMaGV.Text);
                cmd.Parameters.AddWithValue("TenGV", txtTenGV.Text);
                cmd.Parameters.AddWithValue("SdtGV", txtSdtGV.Text);
                cmd.Parameters.AddWithValue("MaBM", txtMaBM.Text);

                cmd.ExecuteNonQuery();
                DialogResult result;
                result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.Close();
                    frmThemGV frm = new frmThemGV();
                    frm.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSGiaoVien frm = new frmDSGiaoVien();
            frm.Show();
        }
    }
}
