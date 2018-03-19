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
    public partial class frmSuaLopHocPhan : Form
    {
        string MaLopHocPhan;
        public frmSuaLopHocPhan(string Ma)
        {
            MaLopHocPhan = Ma;
            InitializeComponent();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                int SoTrinh;
               
                SoTrinh = Convert.ToInt16(txtSoTC.Text);
                
                cmd.CommandText = "UPDATE LOPHOCPHAN SET MaHP='" + txtMaHP.Text + "',MaGV='" + txtMaGV.Text + "',DiaDiemTCHP='" + txtDiaDiem.Text + "',SoTC=" + SoTrinh + "WHERE MaLHP='" + MaLopHocPhan + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
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

        private void frmSuaLopHocPhan_Load_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MaLHP,MaHP,MaGV,DiaDiemTCHP,SoTC FROM LOPHOCPHAN WHERE MaLHP='" + MaLopHocPhan + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaLHP.Text = td.Rows[0][0].ToString();
            this.txtMaHP.Text = td.Rows[0][1].ToString();
            this.txtMaGV.Text = td.Rows[0][2].ToString();
            this.txtDiaDiem.Text = td.Rows[0][3].ToString();
            this.txtSoTC.Text = td.Rows[0][4].ToString();
            con.Close();
        }

        private void txtMaLHP_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.Show();
        }

        private void txtDiaDiem_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
