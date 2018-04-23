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
    public partial class frmChonLop : Form
    {
        public frmChonLop()
        {
            InitializeComponent();
        }

        private void Lop_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM LOP";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cboDSLop.Items.Add(td.Rows[i][1]);
            }
            con.Close();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDS_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                string TenLop;
                TenLop = cboDSLop.SelectedItem.ToString();
                cmd.CommandText = "SELECT MaLop FROM LOP WHERE TenLop=N'" + TenLop + "'";
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                DataTable td = new DataTable();
                td.Load(rd);
                string MaLop;
                MaLop = td.Rows[0][0].ToString();
                con.Close();
                frmDSSV frmDS = new frmDSSV(MaLop);
                frmDS.Show();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Tên lớp không được để trống !!.\n Vui lòng chọn tên lớp ^^!", "THÔNG BÁO");
            }
           

        }

       
    }
}
