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
    public partial class frmThemHocPhan : Form
    {
        public frmThemHocPhan()
        {
            InitializeComponent();
        }

        private void frmThemHocPhan_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "Select MaBM from BOMON";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbbMaBM.Items.Add(td.Rows[i][0]);
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
                int HocKy;
                //SoTrinh = Convert.ToInt16(txtSoTrinh.Text);
                HocKy = Convert.ToInt16(txtHocKy.Text);
                //cmd.CommandText = "INSERT INTO HOCPHAN VALUES('" + txtMaMonHoc.Text + "',N'" + txtTenMonHoc.Text + "','" + txtMaBoMon.Text + "'," + SoTrinh + "," + HocKy + ")";
                cmd.CommandText = "InsertDataIntoHocPhan";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mahp", txtMaHP.Text);
                cmd.Parameters.AddWithValue("@tenhp", txtTenHP.Text);
                cmd.Parameters.AddWithValue("@mabm", cbbMaBM.Text);
                cmd.Parameters.AddWithValue("@sotc", txtSoTC.Text);
                cmd.Parameters.AddWithValue("@hocky", HocKy);


                cmd.ExecuteNonQuery();
                DialogResult result;
                result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.Close();
                    frmThemHocPhan frm = new frmThemHocPhan();
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
                cmd2.CommandText = "Select * from HOCPHAN where MaHP='" + txtMaHP.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                if (txtMaHP.Text.Length != 6)
                {
                    MessageBox.Show("Mã Học Phần 6 Ký Tự Nhé", "Thông Báo");
                }
                else if (td2.Rows.Count != 0)
                {
                    MessageBox.Show("Mã Học Phần Bị Trùng Nhé", "Thông Báo");
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

        private void txtMaMonHoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbMaBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenBM from BOMON where MaBM = '" + cbbMaBM.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenBM.Text = a["TenBM"].ToString();

            }
        }
    }
}
