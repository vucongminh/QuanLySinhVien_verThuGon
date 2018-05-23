using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class frmThemGV : Form
    {
        public frmThemGV()
        {
            InitializeComponent();
        }



        private void frmThemGV_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from BOMON";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbxTenBM.Items.Add(td.Rows[i][1]);
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
                cmd2.CommandText = "Select * from GIAOVIEN where MaGV='" + txtMaGV.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                try
                {
                    if (txtMaGV.Text != "" && txtTenGV.Text != "" && cbxTenBM.SelectedItem.ToString() != "")
                    {
                        if (td2.Rows.Count == 0)
                        {
                            if (txtMaGV.Text.Length == 6)
                            {
                                if(txtSdtGV.Text == string.Empty)
                                {
                                    string TenBM;
                                    TenBM = cbxTenBM.SelectedItem.ToString();
                                    cmd.CommandText = "select * from BOMON where TenBM =N'" + TenBM + "'";
                                    SqlDataReader rd;
                                    rd = cmd.ExecuteReader();
                                    DataTable td = new DataTable();
                                    td.Load(rd);
                                    string MaBM = td.Rows[0][0].ToString();
                                    cmd.CommandText = "INSERT INTO GIAOVIEN VALUES('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtSdtGV.Text + "','" + MaBM + "')";
                                    cmd.Parameters.AddWithValue("MaGV", txtMaGV.Text);
                                    cmd.Parameters.AddWithValue("TenGV", txtTenGV.Text);
                                    cmd.Parameters.AddWithValue("SdtGV", txtSdtGV.Text);
                                    cmd.Parameters.AddWithValue("MaBM", cbxTenBM.Text);
                                    DialogResult result;
                                    result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI GIÁO VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (result == DialogResult.Yes)
                                    {
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                        this.Close();
                                        frmDSGiaoVien frm = new frmDSGiaoVien();
                                        frm.Show();
                                    }
                                }
                                else
                                {
                                    if (IsNumber(txtSdtGV.Text) && (txtSdtGV.Text.Length == 10 || txtSdtGV.Text.Length == 11))
                                    {
                                        string TenBM;
                                        TenBM = cbxTenBM.SelectedItem.ToString();
                                        cmd.CommandText = "select * from BOMON where TenBM =N'" + TenBM + "'";
                                        SqlDataReader rd;
                                        rd = cmd.ExecuteReader();
                                        DataTable td = new DataTable();
                                        td.Load(rd);
                                        string MaBM = td.Rows[0][0].ToString();
                                        cmd.CommandText = "INSERT INTO GIAOVIEN VALUES('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtSdtGV.Text + "','" + MaBM + "')";
                                        cmd.Parameters.AddWithValue("MaGV", txtMaGV.Text);
                                        cmd.Parameters.AddWithValue("TenGV", txtTenGV.Text);
                                        cmd.Parameters.AddWithValue("SdtGV", txtSdtGV.Text);
                                        cmd.Parameters.AddWithValue("MaBM", cbxTenBM.Text);
                                        DialogResult result;
                                        result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI GIÁO VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                            this.Close();
                                            frmDSGiaoVien frm = new frmDSGiaoVien();
                                            frm.Show();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("SĐT nhập không đúng!");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã Giáo Viên Phải Đúng 6 Ký Tự Nhé", "Thông Báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Giáo Viên Bị Trùng Nhé", "Thông Báo");
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
            frmDSGiaoVien frm = new frmDSGiaoVien();
            frm.Show();
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
