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
    public partial class frmXoaLopHocPhan : Form
    {
        string MaLopHocPhan;
        public frmXoaLopHocPhan(string Ma)
        {
            MaLopHocPhan = Ma;
            InitializeComponent();
        }

        private void frmXoaLopHocPhan_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM LOPHOCPHAN WHERE MaLHP='" + MaLopHocPhan + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaLHP.Text = td.Rows[0][0].ToString();
            this.txtTenHP.Text = td.Rows[0][1].ToString();
            this.txtMaGV.Text = td.Rows[0][2].ToString();
            this.txtTenGV.Text = td.Rows[0][3].ToString();
            con.Close();
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
                
                cmd1.CommandText = "DELETE FROM LOPHOCPHAN Where MaLHP='" + MaLopHocPhan + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN XÓA THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                    con.Close();
                    this.Close();
                    frmDSLopHocPhan frm = new frmDSLopHocPhan();
                    frm.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.Show();
        }
    }
}
