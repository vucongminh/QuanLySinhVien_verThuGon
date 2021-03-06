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
    public partial class frmKetQuaHocTap : Form
    {
        public static string username = string.Empty;
        public static string pass = string.Empty;
        string SinhVien_ID;
        public frmKetQuaHocTap(string MaSinhVien)
        {
            SinhVien_ID = MaSinhVien;
            InitializeComponent();
            DangNhapHeThong DangNhap = new DangNhapHeThong();
            if (DangNhap.checkOnlyRead(username, pass) == true)
            {
                Color hic = Color.FromArgb(54, 54, 54);
                btnThem.BackColor = hic;
                btnThem.Enabled = false;
                btnSua.BackColor = hic;
                btnSua.Enabled = false;
                btnXoa.BackColor = hic;
                btnXoa.Enabled = false;
            }
        }

        private void frmKetQuaHocTap_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT h.TenHP,h.SoTC,b.LanThi,DiemKT,DiemKTGiuaKy,DiemThi,DiemTongKet,GhiChu,s.TenSV,s.MaSV,s.MaLop FROM BANGDIEM as b,LOPHOCPHAN as l,HOCPHAN as h,SINHVIEN as s WHERE s.MaSV ='" + SinhVien_ID + "' and b.MaLHP=l.MaLHP and l.MaHP=h.MaHP and b.MaSV=s.MaSV";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            con.Close();

            KetNoi ax = new KetNoi();
            SqlDataReader rd1 = ax.ThucThiTraVe1Record("Select MaSV,TenSV,MaLop from SINHVIEN where maSV='" + SinhVien_ID + "'");
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                item.SubItems.Add(td.Rows[i][3].ToString());
                item.SubItems.Add(td.Rows[i][4].ToString());
                item.SubItems.Add(td.Rows[i][5].ToString());
                item.SubItems.Add(td.Rows[i][6].ToString());
                item.SubItems.Add(td.Rows[i][7].ToString());
                listView1.Items.Add(item);

            }
            labeTen.Text = td1.Rows[0][1].ToString();
            labelMaSV.Text = td1.Rows[0][0].ToString();
            labelMaLop.Text = td1.Rows[0][2].ToString();


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            this.Close();


        }

        private void listView1_Click(object sender, EventArgs e)
        {
            string TenHocPhan;
            int row;
            row = this.listView1.SelectedItems[0].Index;
            TenHocPhan = this.listView1.Items[row].SubItems[0].Text;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmThemKetQua frm = new frmThemKetQua(SinhVien_ID);
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int LanThi;
                string TenHocPhan;
                string LopHocPhan_ID;
                int row;
                row = this.listView1.SelectedItems[0].Index;
                LanThi = Convert.ToInt16(this.listView1.Items[row].SubItems[2].Text);
                TenHocPhan = this.listView1.Items[row].SubItems[0].Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT l.MaLHP from BANGDIEM as b, LOPHOCPHAN as l,HOCPHAN as h where b.MaSV='" + SinhVien_ID + "'and h.TenHP=N'" + TenHocPhan + "'and b.MaLHP=l.MaLHP and l.MaHP= h.MaHP";
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                DataTable td = new DataTable();
                td.Load(rd);
                LopHocPhan_ID = td.Rows[0][0].ToString();
                this.Close();
                frmSuaKetQua frm = new frmSuaKetQua(SinhVien_ID, LopHocPhan_ID, LanThi);
                frm.Show();
            }
            catch
            {
                MessageBox.Show("Hãy chọn kết quả học tập mà bạn muốn sửa ^^! ", "THÔNG BÁO");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int LanThi;
                string TenHocPhan;
                string LopHocPhan_ID;
                int row;
                row = this.listView1.SelectedItems[0].Index;
                LanThi = Convert.ToInt16(this.listView1.Items[row].SubItems[2].Text);
                TenHocPhan = this.listView1.Items[row].SubItems[0].Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT l.MaLHP from BANGDIEM as b, LOPHOCPHAN as l,HOCPHAN as h where b.MaSV='" + SinhVien_ID + "'and h.TenHP=N'" + TenHocPhan + "'and b.MaLHP=l.MaLHP and l.MaHP= h.MaHP";
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                DataTable td = new DataTable();
                td.Load(rd);
                LopHocPhan_ID = td.Rows[0][0].ToString();
                cmd.CommandText = "DELETE FROM BANGDIEM WHERE MaSV='" + SinhVien_ID + "' AND MaLHP='" + LopHocPhan_ID + "' AND LanThi=" + LanThi + " ";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN XOÁ KẾT QUẢ HỌC TẬP NÀY CỦA SINH VIÊN " + SinhVien_ID + " KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                    this.Close();
                    frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
                    frm.Show();
                }
            }
            catch
            {
                MessageBox.Show("Hãy chọn kết quả học tập mà bạn muốn xóa ^^! ", "THÔNG BÁO");
            }
        }


    }
}
