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
    public partial class frmThemLopHocPhan : Form
    {
        public frmThemLopHocPhan()
        {
            InitializeComponent();
        }

        private void frmThemLopHocPhan_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select MaHP from HOCPHAN ";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbbMaHP.Items.Add(td.Rows[i][0]);
            }

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaGV from GIAOVIEN ";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbbMaGV.Items.Add(td1.Rows[i][0]);
            }

            con.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                //int SoTrinh;
                //SoTrinh = Convert.ToInt16(txtSoTC.Text);
                //cmd.CommandText = "INSERT INTO LOPHOCPHAN VALUES('" + txtMaLHP.Text + "','" + txtMaHP.Text + "','" + txtMaGV.Text + "','" + txtDiaDiem.Text + "'," + SoTrinh + ")";
                cmd.CommandText = "InsertDataIntoLopHocPhan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MaLHP", txtMaLHP.Text);
                cmd.Parameters.AddWithValue("MaHP", cbbMaHP.Text);
                cmd.Parameters.AddWithValue("MaGV", cbbMaGV.Text);
                cmd.Parameters.AddWithValue("DiaDiemTCHP", txtDiaDiem.Text);
                cmd.Parameters.AddWithValue("SoTC", txtSoTC.Text);

                cmd.ExecuteNonQuery();
                DialogResult result;
                result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.Close();
                    frmThemLopHocPhan frm = new frmThemLopHocPhan();
                    frm.Show();
                }
            }
            catch (Exception)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "Select * from LOPHOCPHAN where MaLHP='" + txtMaLHP.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (td2.Rows.Count != 0)
                {
                    MessageBox.Show("Mã Lớp Học Phần Bị Trùng Nhé", "Thông Báo");
                }
                else if (txtMaLHP.Text.Length != 6)
                {
                    MessageBox.Show("Mã Lớp Học Phần 6 Ký Tự Nhé", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
                }
            }
        }


        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.Show();
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
