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

            cmd.CommandText = "Select MaBM from BOMON";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbbMaBM.Items.Add(td.Rows[i][0]);
            }
            con.Close();
        }

        private void btnThemLop_Click(object sender, EventArgs e)
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
            if (txtMaGV.Text.Length == 6 )
            {
                if (td2.Rows.Count == 0)
                {
                    string MaBM;
                    MaBM = cbbMaBM.SelectedItem.ToString();
                    cmd.CommandText = "INSERT INTO GIAOVIEN VALUES('" + txtMaGV.Text + "',N'" + txtTenGV.Text + "','" + txtSdtGV.Text + "','" + MaBM + "')";
                    //cmd.CommandText = "InsertDataIntoLop";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MaGV", txtMaGV.Text);
                    cmd.Parameters.AddWithValue("TenGV", txtTenGV.Text);
                    cmd.Parameters.AddWithValue("SdtGV", txtSdtGV.Text);
                    cmd.Parameters.AddWithValue("MaBM", cbbMaBM.Text);
                    cmd.ExecuteNonQuery();
                    DialogResult result;
                    result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        frmThemGV frm = new frmThemGV();
                        frm.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Mã Giáo Viên Bị Trùng Nhé", "Thông Báo");
                }
            }
            else
            {
                MessageBox.Show("Mã Giáo Viên 6 Ký Tự Nhé", "Thông Báo");
            }
            //catch (Exception)
            //{
            //    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            //}
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSGiaoVien frm = new frmDSGiaoVien();
            frm.Show();
        }

        
    }
}
