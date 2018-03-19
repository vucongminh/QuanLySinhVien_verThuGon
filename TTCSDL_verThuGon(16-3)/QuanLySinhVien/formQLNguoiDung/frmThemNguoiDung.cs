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
    public partial class frmThemNguoiDung : Form
    {
        string hinhAnh;
        public frmThemNguoiDung()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDanhSachNguoiDung frm = new frmDanhSachNguoiDung();
            frm.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT count(*) FROM QuanLyNguoiDung where tendangnhap='"+txtTenDangNhap.Text+"'";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            int a = int.Parse(td1.Rows[0][0].ToString());
            if (a!=0)
            {
                MessageBox.Show("Tên đăng nhập này đã có người sử dụng !", "THÔNG BÁO");
            }else if (txtTenDangNhap.Text=="")
            {
                MessageBox.Show("Bạn cần nhập đủ các trường bắt buộc !", "THÔNG BÁO");
            }
            else if (txtMatKhau.Text=="")
            {
                MessageBox.Show("Bạn cần nhập đủ các trường bắt buộc !", "THÔNG BÁO");
            }
            else { 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string QuyenHan;
            QuyenHan = this.cboQuyenHan.SelectedItem.ToString();
            cmd.CommandText = "INSERT INTO QuanLyNguoiDung VALUES('" + txtTenDangNhap.Text + "','" + txtMatKhau.Text + "','" + QuyenHan + "','"+hinhAnh+"','"+txtGmail.Text+"')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Thêm Dữ Liệu Thành Công");
            this.Close();
            frmDanhSachNguoiDung frm = new frmDanhSachNguoiDung();
            frm.Show();               
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void frmThemNguoiDung_Load(object sender, EventArgs e)
        {
            this.ptAvatar.Image = new Bitmap(Application.StartupPath + @"\hinhanh\vodien.jpg");
            ptAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
    }

