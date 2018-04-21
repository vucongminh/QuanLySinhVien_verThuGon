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
    public partial class frmSuaGV : Form
    {
        string MaGV;
        public frmSuaGV(string Ma)
        {
            MaGV = Ma;
            InitializeComponent();
        }

        private void frmSuaGV_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenGV, SdtGV, MaBM FROM GIAOVIEN WHERE MaGV='" + MaGV + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaGV.Text = MaGV;
            this.txtTenGV.Text = td.Rows[0][0].ToString();
            this.txtSdtGV.Text = td.Rows[0][1].ToString();
            this.txtMaBM.Text = td.Rows[0][2].ToString();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE GIAOVIEN SET TenGV=N'" + txtTenGV.Text + "',SdtGV='" + txtSdtGV.Text + "',MaBM='" + txtMaBM.Text + "' WHERE MaGV='" + MaGV + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                    this.Close();
                    frmDSGiaoVien frm = new frmDSGiaoVien();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        
    }
}
