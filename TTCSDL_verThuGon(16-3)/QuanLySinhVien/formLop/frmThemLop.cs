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
                // cmd.CommandText = "INSERT INTO LOP VALUES('" + txtMaLop.Text + "','" + txtTenLop.Text + "','"+txtMaLT.Text+ "','" + txtMaGVCN.Text + "')";
                cmd.CommandText = "InsertDataIntoLop";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("MaLop", txtMaLop.Text);
                cmd.Parameters.AddWithValue("TenLop", txtTenLop.Text);
                cmd.Parameters.AddWithValue("MaLopTruong", txtMaLT.Text);
                cmd.Parameters.AddWithValue("MaGVCN", txtMaGVCN.Text);

                cmd.ExecuteNonQuery();
            DialogResult result;
            result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                this.Close();
                frmThemLop frm = new frmThemLop();
                frm.Show();
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLop frm = new frmDSLop();
            frm.Show();
        }
    }
}
