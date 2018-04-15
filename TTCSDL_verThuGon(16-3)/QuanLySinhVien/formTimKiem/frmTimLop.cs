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
    public partial class frmTimLop : Form
    {
        public frmTimLop()
        {
            InitializeComponent();
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
        private void frmTimLop_Load(object sender, EventArgs e)
        {
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtTuKhoa.Text = "Ví Dụ: HTTT14 / Nguyễn Văn A";

            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void getData(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            string sql = "Select TenLop from LOP";

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
            connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            string sql = "Select distinct MaLop from LOP  ";

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
                    cmd.CommandText = "Select distinct LOP.MaLop,TenLop,MaSV,TenSV,MaGV,TenGV from SINHVIEN, GIAOVIEN, LOP where LOP.MaLopTruong=SINHVIEN.MaSV and SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV AND LOP.MaLop='" + txtTuKhoa.Text + "'";
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
                        item.SubItems.Add(td.Rows[0][4].ToString());
                        item.SubItems.Add(td.Rows[0][5].ToString());
                        listView1.Items.Add(item);

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Mã " + txtTuKhoa.Text);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "Select distinct LOP.MaLop,TenLop,MaSV,TenSV,MaGV,TenGV from SINHVIEN, GIAOVIEN, LOP where LOP.MaLopTruong=SINHVIEN.MaSV and SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV and LOP.TenLop like N'%" + txtTuKhoa.Text + "%'";
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
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            listView1.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Tên " + txtTuKhoa.Text);
                    }
                    //this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    //this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
            }
            else
                MessageBox.Show("Hãy Nhập Từ Khóa !");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;
                this.Close();
                frmSuaLop frm = new frmSuaLop(str);
                //frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy Chọn Lớp Muốn Xem !", "Thông Báo");
            }
        }



        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData2(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }
    }
}