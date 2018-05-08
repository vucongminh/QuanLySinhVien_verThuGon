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
    public partial class frmSuaLop : Form
    {

        string MaLop;
        public frmSuaLop(string Ma)
        {

            MaLop = Ma;
            InitializeComponent();
        }

        public void frmSuaLop_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenLop, MaLopTruong, MaGVCN FROM LOP WHERE MaLop='" + MaLop + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);

            this.txtMaLop.Text = MaLop;
            this.txtTenLop.Text = td.Rows[0][0].ToString();
            this.cbbMaLT.Text = td.Rows[0][1].ToString();
            this.cbbMaGVCN.Text = td.Rows[0][2].ToString();

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = KetNoi.str;
            con1.Open();
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con1;
            cmd2.CommandText = "Select MaSV from SINHVIEN where MaSV not in ( select MaLopTruong from LOP where MaLopTruong = SINHVIEN.MaSV)";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbbMaLT.Items.Add(td2.Rows[i][0]);
            }

            SqlConnection con2 = new SqlConnection();
            con2.ConnectionString = KetNoi.str;
            con2.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con2;
            cmd1.CommandText = "Select MaGV from GIAOVIEN where MaGV not in ( select MaGVCN from LOP where MaGVCN = GIAOVIEN.MaGV)";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbbMaGVCN.Items.Add(td1.Rows[i][0]);
            }

            con.Close();
            con1.Close();
            con2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLop frm = new frmDSLop();
            frm.Show();
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

            cmd.CommandText = "UPDATE LOP SET TenLop=N'" + txtTenLop.Text + "',MaLopTruong='" + cbbMaLT.Text + "',MaGVCN='" + cbbMaGVCN.Text + "' WHERE MaLop='" + MaLop + "'";
            DialogResult result;
            result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                this.Close();
                frmDSLop frm = new frmDSLop();
                frm.Show();
            }
        }
            catch (Exception)
            {
              

                if (txtMaLop.Text.Length != 6 )
                {
                    MessageBox.Show("Mã Lớp 6 Ký Tự Nhé !", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
                }
            }
        }

        private void cbbMaLT_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenSV from SINHVIEN where MaSV = '" + cbbMaLT.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenLT.Text = a["TenSV"].ToString();

            }
        }

        private void cbbMaGVCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbbMaGVCN.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenGVCN.Text = a["TenGV"].ToString();

            }
        }
    }
    }

