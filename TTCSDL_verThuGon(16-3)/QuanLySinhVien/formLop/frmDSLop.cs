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
    public partial class frmDSLop : Form
    {
        public static string username = string.Empty;
        public static string pass = string.Empty;
        public frmDSLop()
        {          
            InitializeComponent();
            DangNhapHeThong DangNhap = new DangNhapHeThong();
            if (DangNhap.checkOnlyRead(username, pass) == true)
            {
                Color hic = Color.FromArgb(54, 54, 54);
                btnSua.BackColor = hic;
                btnSua.Enabled = false;
                btnThem.BackColor = hic;
                btnThem.Enabled = false;
                btnXoa.BackColor = hic;
                btnXoa.Enabled = false;
            }
        }

        protected void textBox1_Focus(Object sender, EventArgs e)
        {
            txtTuKhoa.Text = "";

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

        private void frmDSLop_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtTuKhoa.Text = "Ví Dụ: HTTT14 / Hệ Thống Thông Tin";

            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT Lop.MaLop,TenLop,TenSV,TenGV FROM LOP ,SINHVIEN , GIAOVIEN  where LOP.MaLopTruong=SINHVIEN.MaSV and LOP.MaGVCN=GIAOVIEN.MaGV ";
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            string str;
            int row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;
            frmSuaLop frm = new frmSuaLop(str);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmThemLop frm = new frmThemLop();
            //frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
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
            catch (Exception )
            {
                MessageBox.Show("Hãy Chọn Lớp Muốn Sửa !","Thông Báo");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
            string str;
            int row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;
            this.Close();
            frmXoaLop frm = new frmXoaLop(str);
                //frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hay Chon Sinh Vien Muon Sua !", "Thong Bao");
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
            if (txtTuKhoa.Text != " " && txtTuKhoa.Text != "Ví Dụ: HTTT14 / Hệ Thống Thông Tin")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "Select distinct LOP.MaLop,TenLop,TenSV,TenGV from SINHVIEN, GIAOVIEN, LOP where LOP.MaLopTruong=SINHVIEN.MaSV and SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV AND LOP.MaLop='" + txtTuKhoa.Text + "'";
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
                        frmDSLop_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    this.txtTuKhoa.Text = "Ví Dụ: HTTT14 / Hệ Thống Thông Tin";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "Select distinct LOP.MaLop,TenLop,TenSV,TenGV from SINHVIEN, GIAOVIEN, LOP where LOP.MaLopTruong=SINHVIEN.MaSV and SINHVIEN.MaLop = LOP.MaLop and LOP.MaGVCN = GIAOVIEN.MaGV and LOP.TenLop like N'%" + txtTuKhoa.Text + "%'";
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
                        frmDSLop_Load(sender, e);
                    }
                    //this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    //this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmDSLop_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmDSLop_Load(sender, e);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData2(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            txtTuKhoa.AutoCompleteCustomSource = DataCollection;
        }
    }
    
}
