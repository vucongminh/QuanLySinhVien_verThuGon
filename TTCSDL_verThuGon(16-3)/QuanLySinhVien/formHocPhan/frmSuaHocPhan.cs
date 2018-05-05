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
    public partial class frmSuaHocPhan : Form
    {
        string MaMonHoc;
        public frmSuaHocPhan(string Ma)
        {
            MaMonHoc = Ma;
            InitializeComponent();
        }

        private void frmSuaMonHoc_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MaHP,TenHP,SoTC,HocKy,MaBM FROM HOCPHAN WHERE MaHP='" + MaMonHoc + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaHP.Text = td.Rows[0][0].ToString();
            this.txtTenHP.Text = td.Rows[0][1].ToString();
            this.txtSoTC.Text = td.Rows[0][2].ToString();
            this.txtHocKy.Text = td.Rows[0][3].ToString();
            this.cbbMaBM.Text = td.Rows[0][4].ToString();

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = KetNoi.str;
            con1.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            cmd1.CommandText = "Select MaBM from BOMON";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbbMaBM.Items.Add(td1.Rows[i][0]);
            }
            con.Close();
            con1.Close();
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
                int HocKy;
                SoTrinh = Convert.ToInt16(txtSoTC.Text);
                HocKy = Convert.ToInt16(txtHocKy.Text);
                string MaBM;
                MaBM = cbbMaBM.SelectedItem.ToString();
                cmd.CommandText = "UPDATE HOCPHAN SET TenHP=N'" + txtTenHP.Text + "',MaBM='" + MaBM + "',SoTC=" + SoTrinh + ",HocKy=" + HocKy + "WHERE MaHP='" + MaMonHoc + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                    this.Close();
                    frmDSHocPhan frm = new frmDSHocPhan();
                    frm.Show();
                }
            }
            catch (Exception)
            {
               
                if (txtMaHP.Text.Length != 6)
                {
                    MessageBox.Show("Mã Học Phần 6 Ký Tự Nhé", "Thông Báo");
                }
              
                else
                {
                    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
                }

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSHocPhan frm = new frmDSHocPhan();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSHocPhan frm = new frmDSHocPhan();
            frm.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
