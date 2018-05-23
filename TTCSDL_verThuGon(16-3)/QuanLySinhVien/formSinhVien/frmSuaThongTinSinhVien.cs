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
    public partial class frmSuaThongTinSinhVien : Form
    {
        string SinhVien_ID;
        string hinhanh;
        public frmSuaThongTinSinhVien(string MaSinhVien)
        {
            SinhVien_ID = MaSinhVien;
            InitializeComponent();
            maskedTextBox1.Mask = "00/00/0000";
            maskedTextBox1.KeyUp += new KeyEventHandler(msDate_KeyUp);
        }

        private void frmSuaThongTinSinhVien_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT * FROM Lop order by MaLop ";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxTenLop.Items.Add(td1.Rows[i][1]);
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT SinhVien.*,Lop.TenLop FROM SinhVien,Lop WHERE MaSV='" + SinhVien_ID + "' and SinhVien.MaLop=Lop.MaLop";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaSinhVien.Text = td.Rows[0][0].ToString();
            this.txtCMND.Text = td.Rows[0][2].ToString();
            DateTime ngay = DateTime.Parse(td.Rows[0][4].ToString());
            this.maskedTextBox1.Text = ngay.ToString("dd/MM/yyyy");
            this.txtQueQuan.Text = td.Rows[0][5].ToString();
            this.txtSDT.Text = td.Rows[0][6].ToString();
            this.txtTen.Text = td.Rows[0][1].ToString();
            this.cbxTenLop.Text = td.Rows[0][10].ToString();
            hinhanh = td.Rows[0][8].ToString();
            if (hinhanh.Length <= 0)
            {
                this.pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\vodien.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                this.pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + hinhanh);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            int sex;
            sex = Convert.ToInt16(td.Rows[0][3]);
            if (sex == 1)
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
            con.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmChiTietSinhVien frm = new frmChiTietSinhVien(SinhVien_ID);
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int sex;
            if (radioButton1.Checked == true)
                sex = 1;
            else
                sex = 0;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                if (txtMaSinhVien.Text != "" && txtQueQuan.Text != "" && txtTen.Text != "" && cbxTenLop.SelectedItem.ToString() != "" && txtCMND.Text != "" && (radioButton1.Checked == true || radioButton2.Checked == true))
                {
                    if (IsNumber(txtCMND.Text) && txtCMND.Text.Length == 9)
                    {
                        if (IsNumber(txtSDT.Text) && (txtSDT.Text.Length == 10 || txtSDT.Text.Length == 11))
                        {
                            try
                            {
                                cmd.CommandText = "select MaLop from Lop where TenLop =N'" + cbxTenLop.Text + "'";
                                SqlDataReader rd;
                                rd = cmd.ExecuteReader();
                                DataTable td = new DataTable();
                                td.Load(rd);
                                string MaLop = td.Rows[0][0].ToString();
                                string[] arr = maskedTextBox1.Text.Split('/');
                                string dt = arr[1] + "/" + arr[0] + "/" + arr[2];
                                cmd.CommandText = "UPDATE SinhVien SET CMND='" + txtCMND.Text + "',GioiTinh=" + sex + ",NgaySinh='" + DateTime.Parse(dt.ToString()) + "',QueQuan=N'" + txtQueQuan.Text + "',SdtSV='" + txtSDT.Text + "',TenSV=N'" + txtTen.Text + "',MaLop='" + MaLop + "',HinhAnh='" + hinhanh + "'WHERE MaSV='" + SinhVien_ID + "'";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
                                }
                                con.Close();
                                this.Close();
                                frmChiTietSinhVien frm = new frmChiTietSinhVien(SinhVien_ID);
                                frm.Show();
                            }
                            catch
                            {
                                MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                            }
                        }
                        else
                        {
                            MessageBox.Show("SĐT nhập không đúng!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("CMND nhập không đúng!");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo !");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo !");

            }

        }

        private void btnHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            dl.InitialDirectory = Application.StartupPath + @"hinhanh/";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                hinhanh = dl.FileName.Substring(dl.FileName.LastIndexOf("\\") + 1, dl.FileName.Length - dl.FileName.LastIndexOf("\\") - 1);
                pictureBox1.Image = new Bitmap(Application.StartupPath + @"\hinhanh\" + hinhanh);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnKetQuaHocTap_Click(object sender, EventArgs e)
        {
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }
        void msDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (maskedTextBox1.MaskFull)
            {
                try
                {
                    DateTime.ParseExact(maskedTextBox1.Text, "dd/MM/yyyy", null);

                }
                catch
                {
                    MessageBox.Show("Ngày sinh không hợp lệ");
                    maskedTextBox1.ResetText();
                }
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
    }
}
