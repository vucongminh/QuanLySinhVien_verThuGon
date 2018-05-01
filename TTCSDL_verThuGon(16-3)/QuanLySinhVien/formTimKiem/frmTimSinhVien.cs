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
    public partial class frmTimSinhVien : Form
    {
        public frmTimSinhVien()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void textBox1_Focus(Object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";

        }


        private void SqlCommand(string v, object conn)
        {
            throw new NotImplementedException();
        }

        public int KiemTra()
        {
            if (radioButton1.Checked == true)
                return 1;
            else if (radioButton2.Checked == true)
                return 2;
            else
                return 0;
        }

        private void frmTimSinhVien_Load(object sender, EventArgs e)
        {
            //txtTuKhoa.Text = "Nhập Từ Khoá?";
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";

            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
           
            cmd.CommandText = "Select distinct MaSV,TenSV,TenLop,TenGV from SINHVIEN, GIAOVIEN, LOP where SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV ";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                item.SubItems.Add(td.Rows[i][3].ToString());
                listView1.Items.Add(item);
            }
            con.Close();
        }


        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = KetNoi.str;
            //con.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //listView1.Items.Clear();
            //int check;
            //cmd.CommandText = "select distinct MaSV,TenSV,TenLop,TenGV from SINHVIEN, GIAOVIEN, LOP where SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV or  TenSV like N'%" + txtTuKhoa.Text + "%' or " +
            //        "MaSV like N'%" + txtTuKhoa.Text + "%'";
            //DataTable td = new DataTable();
            //SqlDataReader rd;
            //rd = cmd.ExecuteReader();


            // td.Load(rd);

            //if (td.Rows.Count != 0)
            //{

            //    ListViewItem item = new ListViewItem(td.Rows[0][0].ToString());
            //    item.SubItems.Add(td.Rows[0][1].ToString());
            //    item.SubItems.Add(td.Rows[0][2].ToString());
            //    item.SubItems.Add(td.Rows[0][3].ToString());
            //    listView1.Items.Add(item);

            //}
            //else
            //{
            //    MessageBox.Show("Không Tồn Tại Sinh Viên Có Mã " + txtTuKhoa.Text);
            //}


        }

        private void getData(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            //connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            connetionString = KetNoi.str;
            string sql = "Select TenSV from SINHVIEN ";

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong co ket noi ! ");
            }
        }
        private void getData2(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            //connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            connetionString = KetNoi.str;
            string sql = "Select distinct MaSV from SINHVIEN  ";

            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                connection.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong co ket noi ! ");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Remove();
                i--;
            }
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (txtTuKhoa.Text != " " && txtTuKhoa.Text != "Ví Dụ: SV0000 / Nguyễn Văn A")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "Select distinct MaSV,TenSV,TenLop,TenGV from SINHVIEN, GIAOVIEN, LOP where SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV AND SINHVIEN.MaSV='" + txtTuKhoa.Text + "'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    DataTable td = new DataTable();
                    td.Load(rd);

                    if (td.Rows.Count != 0)
                    {

                        ListViewItem item = new ListViewItem(td.Rows[0][0].ToString());
                        item.SubItems.Add(td.Rows[0][1].ToString());
                        item.SubItems.Add(td.Rows[0][2].ToString());
                        item.SubItems.Add(td.Rows[0][3].ToString());
                        listView1.Items.Add(item);

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Mã " + txtTuKhoa.Text);
                        frmTimSinhVien_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "Select distinct MaSV,TenSV,TenLop,TenGV from SINHVIEN, GIAOVIEN, LOP where SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV AND SINHVIEN.TenSV like N'%" + txtTuKhoa.Text + "%'";
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    DataTable td = new DataTable();
                    td.Load(rd);

                    if (td.Rows.Count != 0)
                    {

                        for (int i = 0; i < td.Rows.Count; i++)
                        {
                            ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                            item.SubItems.Add(td.Rows[i][1].ToString());
                            item.SubItems.Add(td.Rows[i][2].ToString());
                            item.SubItems.Add(td.Rows[i][3].ToString());
                            listView1.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Tên " + txtTuKhoa.Text);
                        frmTimSinhVien_Load(sender, e);
                    }
                    //this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    //this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmTimSinhVien_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmTimSinhVien_Load(sender, e);
            }

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            string str;
            int row;
            row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                int row;
                row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;

                frmChiTietSinhVien frm = new frmChiTietSinhVien(str);
                this.Close();
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy Chọn Sinh Viên Muốn Xem !", "Thông Báo");
            }
        }


        private void txtTuKhoa_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData2(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
