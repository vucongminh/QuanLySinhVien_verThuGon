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
    public partial class frmSuaNguoiDung : Form
    {
        string TenDangNhap;
        string MatKhau;
        string hinhAnh;
        public frmSuaNguoiDung(string Ten,string Ma)
        {
            TenDangNhap = Ten;
            MatKhau = Ma;
            InitializeComponent();
        }

        private void frmSuaNguoiDung_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT* FROM QuanLyNguoiDung WHERE TenDangNhap='" + TenDangNhap + "' and MatKhau='" + MatKhau + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtTenDangNhap.Text = td.Rows[0][0].ToString();
            this.txtMatKhau.Text = td.Rows[0][1].ToString();
            this.txtQuyenHan.Text = td.Rows[0][2].ToString();
            this.txtGmail.Text = td.Rows[0][4].ToString();
            string image;
            image = td.Rows[0][3].ToString();
            if (image.Length <= 0)
            {
                this.ptAvatar.Image = new Bitmap(Application.StartupPath + @"\hinhanh\vodien.jpg");
                ptAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                this.ptAvatar.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + image);
                ptAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            con.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDanhSachNguoiDung frm = new frmDanhSachNguoiDung();
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText="UPDATE QuanLyNguoiDung SET QuyenHan='"+txtQuyenHan.Text+ "',hinhanh='" + hinhAnh + "',TenDangNhap=N'" + txtTenDangNhap.Text + "' ,MatKhau='" + txtMatKhau.Text + "',Gmail='" + txtGmail.Text + "' WHERE TenDangNhap='" + TenDangNhap+"' AND MatKhau='"+MatKhau+"'";
            DialogResult result;
            result = MessageBox.Show("Bạn Có Muốn Thay Đổi Thông Tin Không?","Thông Báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa Thông Tin Thành Công");
                this.Close();
                frmDanhSachNguoiDung frm = new frmDanhSachNguoiDung();
                frm.Show();
            }
        }
        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            dl.InitialDirectory = Application.StartupPath + @"hinhanh/";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                hinhAnh = dl.FileName.Substring(dl.FileName.LastIndexOf("\\") + 1, dl.FileName.Length - dl.FileName.LastIndexOf("\\") - 1);
                ptAvatar.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + hinhAnh);
                ptAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void ptAvatar_Click(object sender, EventArgs e)
        {

        }
    }
}
