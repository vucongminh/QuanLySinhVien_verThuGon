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
    public partial class frmThemSV : Form
    {
        string Lop_ID;
        string hinhanh;
        public frmThemSV(string ma)
        {
            Lop_ID = ma;
            InitializeComponent();
            mskNgaySinh.Mask = "00/00/0000";
            mskNgaySinh.KeyUp += new KeyEventHandler(msDate_KeyUp);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Lop order by MaLop ";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbxTenLop.Items.Add(td.Rows[i][1]);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSSV frm = new frmDSSV(Lop_ID);
            frm.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sex;
            if (radioButton7.Checked == true)
                sex = 0;
            else
                sex = 1;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "select * from SINHVIEN where MaSV='" + txtmaSV.Text + "'";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            try
            {
                if (txtmaSV.Text != "" && txtQueQuan.Text != "" && txtTen.Text != "" && cbxTenLop.SelectedItem.ToString() != "" && txtCMND.Text != "" && (radioButton7.Checked==true||radioButton8.Checked==true))
                {
                    if (td2.Rows.Count == 0)
                    {
                        if (IsNumber(txtCMND.Text))
                        {
                            if (IsNumber(txtSDT.Text))
                            {

                                string TenLop;
                                TenLop = cbxTenLop.SelectedItem.ToString();
                                cmd.CommandText = "select* from Lop where TenLop =N'" + TenLop + "'";
                                SqlDataReader rd;
                                rd = cmd.ExecuteReader();
                                DataTable td = new DataTable();
                                td.Load(rd);
                                string MaLop = td.Rows[0][0].ToString();

                                cmd.CommandText = "insert into SinhVien values ('" + txtmaSV.Text + "',N'" + txtTen.Text + "','" + txtCMND.Text + "'," + sex + ",'" + DateTime.Parse(mskNgaySinh.Text) + "',N'" + txtQueQuan.Text + "','" + txtSDT.Text + "','" + MaLop + "','" + hinhanh + "','')";
                                DialogResult result;
                                result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI SINH VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)
                                {
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("THÊM MỚI THÀNH CÔNG", "THÔNG BÁO");
                                    this.Close();
                                    frmDSSV frm = new frmDSSV(Lop_ID);
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
                            MessageBox.Show("CMND nhập không đúng!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Mã Sinh Viên Bị Trùng !", "Thông Báo");
                    }
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại thông tin nhập..!", "Thông Báo");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kiểm tra lại thông tin nhập..!", "Thông Báo");
            }
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
        void msDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (mskNgaySinh.MaskFull)
            {
                try
                {
                    DateTime.ParseExact(mskNgaySinh.Text, "dd/MM/yyyy", null);
                    
                }
                catch
                {
                    MessageBox.Show("Ngày sinh không hợp lệ");
                    mskNgaySinh.ResetText();
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

        private void mskNgaySinh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
