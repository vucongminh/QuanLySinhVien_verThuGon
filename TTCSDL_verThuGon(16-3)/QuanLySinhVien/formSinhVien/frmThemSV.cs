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
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try { 
            string TenLop;
            TenLop = cbxTenLop.SelectedItem.ToString();
            cmd.CommandText = "select* from Lop where TenLop =N'" + TenLop + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            string MaLop = td.Rows[0][0].ToString();
            if (radioButton7.Checked == true)
                sex = 0;
            else
                sex = 1;
            cmd.CommandText = "insert into SinhVien values ('"+txtmaSV.Text+"',N'"+txtTen.Text+"','" + txtCMND.Text + "'," + sex + ",'" + DateTime.Parse(mskNgaySinh.Text) + "',N'" + txtQueQuan.Text + "','" + txtSDT.Text + "','" + MaLop + "','" + hinhanh + "','')";
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
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Kiểm tra lại thông tin nhập..Có thể bạn quên chưa nhập 1 trường nào đó ^^!", "Thông Báo !");
            }
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

        private void mskNgaySinh_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
