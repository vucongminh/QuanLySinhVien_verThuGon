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
    public partial class frmThemLop : Form
    {

        public frmThemLop()
        {
            InitializeComponent();
        }

        private void frmThemLop_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select MaSV from SINHVIEN where MaSV not in ( select MaLopTruong from LOP where MaLopTruong = SINHVIEN.MaSV)";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbxMaLT.Items.Add(td.Rows[i][0]);
            }

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaGV from GIAOVIEN where MaGV not in ( select MaGVCN from LOP where MaGVCN = GIAOVIEN.MaGV)";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxMaGVCN.Items.Add(td1.Rows[i][0]);
            }

            con.Close();

        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "Select * from LOP where MaLop='" + txtMaLop.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                try
                {
                    if (txtMaLop.Text != "" && txtTenLop.Text != "" && cbxMaGVCN.SelectedItem.ToString() != "" && cbxMaLT.SelectedItem.ToString() != "")
                    {
                        if (td2.Rows.Count == 0)
                        {
                            cmd.CommandText = "InsertDataIntoLop";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("MaLop", txtMaLop.Text);
                            cmd.Parameters.AddWithValue("TenLop", txtTenLop.Text);
                            cmd.Parameters.AddWithValue("MaLopTruong", cbxMaLT.Text);
                            cmd.Parameters.AddWithValue("MaGVCN", cbxMaGVCN.Text);
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI LỚP NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG");
                                this.Close();
                                frmDSLop frm = new frmDSLop();
                                frm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Lớp Bị Trùng Nhé", "Thông Báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                    }
                }
                catch
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLop frm = new frmDSLop();
            frm.Show();
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
