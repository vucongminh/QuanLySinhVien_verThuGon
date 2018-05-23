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
    public partial class frmThemBoMon : Form
    {
        public frmThemBoMon()
        {
            InitializeComponent();
        }

        private void frmThemKhoa_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaGV from GIAOVIEN where MaGV not in ( select MaChuNhiemBM from BOMON where MaChuNhiemBM = GIAOVIEN.MaGV)";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxMaCNBM.Items.Add(td1.Rows[i][0]);
            }
            con.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSBoMon frm = new frmDSBoMon();
            frm.Show();
        }

        private void btnThemKhoa_Click(object sender, EventArgs e)
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
                cmd2.CommandText = "Select * from BOMON where MaBM='" + txtMaBM.Text + "'";
                SqlDataReader rd2;
                rd2 = cmd2.ExecuteReader();
                DataTable td2 = new DataTable();
                td2.Load(rd2);
                try
                {
                    if (txtMaBM.Text != "" && txtTenBM.Text != "" && cbxMaCNBM.SelectedItem.ToString() != "")
                    {
                        if (td2.Rows.Count == 0)
                        {
                            if (txtMaBM.Text.Length == 6)
                            {
                                cmd.CommandText = "InsertDataIntoBoMon";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("MaBM", txtMaBM.Text);
                                cmd.Parameters.AddWithValue("TenBM", txtTenBM.Text);
                                cmd.Parameters.AddWithValue("MaChuNhiemBM", cbxMaCNBM.Text);
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI BỘ MÔN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG");
                                    this.Close();
                                    frmDSBoMon frm = new frmDSBoMon();
                                    frm.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã Bộ Môn 6 Ký Tự Nhé", "Thông Báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã Bộ Môn Bị Trùng Nhé", "Thông Báo");
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



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbbMaCNBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectString = "select TenGV from GIAOVIEN where MaGV = '" + cbxMaCNBM.Text + "'";
            a = kn.ThucThiTraVe1Record(selectString);
            while (a.Read())
            {
                txtTenCNBM.Text = a["TenGV"].ToString();

            }
        }
    }
}
