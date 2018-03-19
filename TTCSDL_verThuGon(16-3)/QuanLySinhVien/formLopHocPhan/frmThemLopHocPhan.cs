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
                cmd.Parameters.AddWithValue("MaHP", txtMaHP.Text);
                cmd.Parameters.AddWithValue("MaGV", txtMaGV.Text);
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
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            }
        }


        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.Show();
        }

        private void frmThemLopHocPhan_Load(object sender, EventArgs e)
        {

        }
    }
}
