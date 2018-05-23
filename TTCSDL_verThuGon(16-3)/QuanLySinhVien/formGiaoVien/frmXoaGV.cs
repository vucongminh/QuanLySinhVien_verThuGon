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
    public partial class frmXoaGV : Form
    {
        string MaGV;
        public frmXoaGV(string Ma)
        {
            
            MaGV = Ma;
            InitializeComponent();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "DELETE FROM GIAOVIEN Where MaGV='" + MaGV + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN XÓA THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                    con.Close();
                    this.Close();
                    frmDSGiaoVien frm = new frmDSGiaoVien();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmXoaGV_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenGV FROM GIAOVIEN WHERE MaGV='" + MaGV + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaLop.Text = MaGV;
            this.txtTenLop.Text = td.Rows[0][0].ToString();
            con.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSGiaoVien frm = new frmDSGiaoVien();
            frm.Show();
        }
    }
}
