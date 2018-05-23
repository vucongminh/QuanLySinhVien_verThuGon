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

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = con;
            cmd3.CommandText = "Select MaLopTruong,MaGVCN from LOP where MaLop='" + MaLop + "'";
            SqlDataReader rd3;
            rd3 = cmd3.ExecuteReader();
            DataTable td3 = new DataTable();
            td3.Load(rd3);

            this.cbxMaLT.Items.Add(td3.Rows[0][0]);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "Select MaSV from SINHVIEN where MaSV not in ( select MaLopTruong from LOP)";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxMaLT.Items.Add(td2.Rows[i][0]);
            }

            this.cbxMaGVCN.Items.Add(td3.Rows[0][1]);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaGV from GIAOVIEN where MaGV not in ( select MaGVCN from LOP)";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxMaGVCN.Items.Add(td1.Rows[i][0]);
            }


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenLop, MaLopTruong, SV.TenSV, MaGVCN, GV.TenGV FROM LOP,SINHVIEN SV,GIAOVIEN GV WHERE LOP.MaLopTruong = SV.MaSV AND LOP.MaGVCN = GV.MaGV AND LOP.MaLop='" + MaLop + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);

            this.txtMaLop.Text = MaLop;
            this.txtTenLop.Text = td.Rows[0][0].ToString();
            this.cbxMaLT.Text = td.Rows[0][1].ToString();
            this.txtTenLT.Text = td.Rows[0][2].ToString();
            this.cbxMaGVCN.Text = td.Rows[0][3].ToString();
            this.txtTenGVCN.Text = td.Rows[0][4].ToString();




            con.Close();
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
                if (txtTenLop.Text != "" && cbxMaLT.SelectedItem.ToString() != "" && cbxMaGVCN.SelectedItem.ToString() != "")
                {
                    cmd.CommandText = "UPDATE LOP SET TenLop=N'" + txtTenLop.Text + "',MaLopTruong='" + cbxMaLT.Text + "',MaGVCN='" + cbxMaGVCN.Text + "' WHERE MaLop='" + MaLop + "'";
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
                else
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void cbbMaLT_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenSV from SINHVIEN where MaSV = '" + cbxMaLT.Text + "'";
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
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbxMaGVCN.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenGVCN.Text = a["TenGV"].ToString();

            }
        }
    }
}

