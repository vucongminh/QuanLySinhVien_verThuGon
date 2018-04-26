﻿using System;
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            //try
            //{
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //int SoTrinh;
            int HocKy;
            //SoTrinh = Convert.ToInt16(txtSoTrinh.Text);
            HocKy = Convert.ToInt16(txtHocKy.Text);
            //cmd.CommandText = "INSERT INTO HOCPHAN VALUES('" + txtMaMonHoc.Text + "',N'" + txtTenMonHoc.Text + "','" + txtMaBoMon.Text + "'," + SoTrinh + "," + HocKy + ")";
            cmd.CommandText = "InsertDataIntoHocPhan";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mahp", txtMaHP.Text);
            cmd.Parameters.AddWithValue("@tenhp", txtTenHP.Text);
            cmd.Parameters.AddWithValue("@mabm", txtMaBoMon.Text);
            cmd.Parameters.AddWithValue("@sotc", txtSoTC.Text);
            cmd.Parameters.AddWithValue("@hocky", HocKy);
            

            cmd.ExecuteNonQuery();
                DialogResult result;
                result = MessageBox.Show("THÊM DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    this.Close();
                    frmThemHocPhan frm = new frmThemHocPhan();
                    frm.Show();
                }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Nhập Liệu Sai !", "Thông Báo");
            //}
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
    }
}