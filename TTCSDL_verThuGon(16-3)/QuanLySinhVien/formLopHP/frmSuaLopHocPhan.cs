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
                
                cmd.CommandText = "UPDATE LOPHOCPHAN SET MaHP='" + cbbMaHP.Text + "',MaGV='" + cbbMaGV.Text + "',DiaDiemTCHP='" + txtDiaDiem.Text + "',SoTC=" + SoTrinh + "WHERE MaLHP='" + MaLopHocPhan + "'";
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
                if (txtMaLHP.Text.Length != 6)
                {
                    MessageBox.Show("Mã Lớp Học Phần 6 Ký Tự Nhé !", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
                }
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
            this.cbbMaHP.Text = td.Rows[0][1].ToString();
            this.cbbMaGV.Text = td.Rows[0][2].ToString();
            this.txtDiaDiem.Text = td.Rows[0][3].ToString();
            this.txtSoTC.Text = td.Rows[0][4].ToString();
            con.Close();


            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = KetNoi.str;
            con1.Open();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con1;
            cmd2.CommandText = "Select MaHP from HOCPHAN";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbbMaHP.Items.Add(td2.Rows[i][0]);
            }

            SqlConnection con2 = new SqlConnection();
            con2.ConnectionString = KetNoi.str;
            con2.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con2;
            cmd1.CommandText = "Select MaGV from GIAOVIEN";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbbMaGV.Items.Add(td1.Rows[i][0]);
            }

            con.Close();
            con1.Close();
            con2.Close();
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbbMaHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenHP from HOCPHAN where MaHP = '" + cbbMaHP.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenHP.Text = a["TenHP"].ToString();

            }
        }

        private void cbbMaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbbMaGV.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenGV.Text = a["TenGV"].ToString();

            }
        }
    }
}
