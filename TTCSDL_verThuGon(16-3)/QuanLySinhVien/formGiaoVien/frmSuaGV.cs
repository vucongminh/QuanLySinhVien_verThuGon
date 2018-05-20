using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class frmSuaGV : Form
    {
        string MaGV;
        public frmSuaGV(string Ma)
        {
            MaGV = Ma;
            InitializeComponent();
        }

        private void frmSuaGV_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenGV, SdtGV, MaBM FROM GIAOVIEN WHERE MaGV='" + MaGV + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);


            this.txtMaGV.Text = MaGV;
            this.txtTenGV.Text = td.Rows[0][0].ToString();
            this.txtSdtGV.Text = td.Rows[0][1].ToString();
            this.cbbMaBM.Text = td.Rows[0][2].ToString();

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = KetNoi.str;
            con1.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            cmd1.CommandText = "Select MaBM from BOMON";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbbMaBM.Items.Add(td1.Rows[i][0]);
            }
            con.Close();
            con1.Close();
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
                if (txtMaGV.Text.Length == 6)
                {
                    if (IsNumber(txtSdtGV.Text))
                    {
                        string MaBM;
                        MaBM = cbbMaBM.SelectedItem.ToString();
                        cmd.CommandText = "UPDATE GIAOVIEN SET TenGV=N'" + txtTenGV.Text + "',SdtGV='" + txtSdtGV.Text + "',MaBM='" + MaBM + "' WHERE MaGV='" + MaGV + "'";
                        DialogResult result;
                        result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
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
                else
                {
                    MessageBox.Show("Mã Giáo Viên 6 Ký Tự Nhé", "Thông Báo");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
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


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSGiaoVien frm = new frmDSGiaoVien();
            frm.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}
