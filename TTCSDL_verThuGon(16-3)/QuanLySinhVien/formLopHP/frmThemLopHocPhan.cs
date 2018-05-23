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
                this.cbxMaHP.Items.Add(td.Rows[i][0]);
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
                this.cbxMaGV.Items.Add(td1.Rows[i][0]);
            }

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "Select DiaDiemTCHP from LOPHOCPHAN ";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxDiaDiem.Items.Add(td2.Rows[i][0]);
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

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = con;
                cmd2.CommandText = "Select * from LOPHOCPHAN where MaLHP='" + txtMaLHP.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                try
                {
                    if (txtMaLHP.Text != "" && cbxMaHP.SelectedItem.ToString() != "" && cbxMaGV.SelectedItem.ToString() != "" && cbxDiaDiem.Text != "" && txtSoTC.Text != "")
                    {
                        if (td2.Rows.Count == 0)
                        {
                            if (txtMaLHP.Text.Length == 6)
                            {
                                if (IsNumber(txtSoTC.Text))
                                {
                                    int SoTC = Convert.ToInt16(txtSoTC.Text);
                                    cmd.CommandText = "InsertDataIntoLopHocPhan";
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@malhp", txtMaLHP.Text);
                                    cmd.Parameters.AddWithValue("@mahp", cbxMaHP.Text);
                                    cmd.Parameters.AddWithValue("@magv", cbxMaGV.Text);
                                    cmd.Parameters.AddWithValue("@diadiem", cbxDiaDiem.Text);
                                    cmd.Parameters.AddWithValue("@sotc", SoTC);
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI LỚP HỌC PHẦN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG");
                                        this.Close();
                                        frmDSLopHocPhan frm = new frmDSLopHocPhan();
                                        frm.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Số tín chỉ nhập không đúng!");
                                }
                            }

                            else
                            {
                                MessageBox.Show("Mã Lớp Học Phần Phải Đúng 6 Ký Tự Nhé", "Thông Báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Lớp Học Phần Bị Trùng Nhé", "Thông Báo");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                    }
                }
                catch
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            string selectTring = "select TenHP,SoTC from HOCPHAN where MaHP = '" + cbxMaHP.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenHP.Text = a["TenHP"].ToString();
                txtSoTC.Text = a[1].ToString();
            }
        }

        private void cbbMaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbxMaGV.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenGV.Text = a["TenGV"].ToString();

            }
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
