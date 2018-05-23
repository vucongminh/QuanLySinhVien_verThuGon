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
                this.cbxMaBM.Items.Add(td.Rows[i][0]);
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
                cmd2.CommandText = "Select * from HOCPHAN where MaHP='" + txtMaHP.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                try
                {
                    if (txtMaHP.Text != "" && txtTenHP.Text != "" && txtSoTC.Text != "" && txtHocKy.Text != "" && cbxMaBM.SelectedItem.ToString() != "")
                    {
                        if (td2.Rows.Count == 0)
                        {
                            if (txtMaHP.Text.Length == 6)
                            {
                                if (IsNumber(txtSoTC.Text))
                                {
                                    int SoTC = Convert.ToInt16(txtSoTC.Text);
                                    if (IsNumber(txtHocKy.Text))
                                    {
                                        int HocKy = Convert.ToInt16(txtHocKy.Text);
                                        cmd.CommandText = "InsertDataIntoHocPhan";
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@mahp", txtMaHP.Text);
                                        cmd.Parameters.AddWithValue("@tenhp", txtTenHP.Text);
                                        cmd.Parameters.AddWithValue("@mabm", cbxMaBM.Text);
                                        cmd.Parameters.AddWithValue("@sotc", SoTC);
                                        cmd.Parameters.AddWithValue("@hocky", HocKy);
                                        DialogResult result;
                                        result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI HỌC PHẦN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG");
                                            this.Close();
                                            frmDSHocPhan frm = new frmDSHocPhan();
                                            frm.Show();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Học kỳ nhập không đúng!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Số tín chỉ nhập không đúng!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã Học Phần Phải Đúng 6 Ký Tự Nhé", "Thông Báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Học Phần Bị Trùng Nhé", "Thông Báo");
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
            string selectTring = "select TenBM from BOMON where MaBM = '" + cbxMaBM.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenBM.Text = a["TenBM"].ToString();

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
