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
    public partial class frmDSSV : Form
    {
        public static string username = string.Empty;
        public static string pass = string.Empty;
        string MaLop;
        public frmDSSV(string Ma)
        {
            MaLop = Ma;
            InitializeComponent();
            DangNhapHeThong DangNhap = new DangNhapHeThong();
            if (DangNhap.checkOnlyRead(username, pass) == true)
            {
                Color hic = Color.FromArgb(54, 54, 54);
                button1.BackColor = hic;
                button1.Enabled = false;
                button2.BackColor = hic;
                button2.Enabled = false;
                button3.BackColor = hic;
                button3.Enabled = false;
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

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";

            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmd.CommandText = "SELECT MaSV,TenSV,CMND,GioiTinh,NgaySinh,QueQuan,SdtSV,TenLop FROM SINHVIEN,LOP WHERE SINHVIEN.MaLop = '" + MaLop + "' and LOP.MaLop = '" + MaLop + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                if (Convert.ToInt16(td.Rows[i][3]) == 0)
                    item.SubItems.Add("Nam");
                else
                    item.SubItems.Add("Nu");
                item.SubItems.Add(td.Rows[i][4].ToString());
                item.SubItems.Add(td.Rows[i][5].ToString());
                item.SubItems.Add(td.Rows[i][6].ToString());
                item.SubItems.Add(td.Rows[i][7].ToString());
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
            connetionString = KetNoi.str;
            //connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            string sql = "Select TenSV from SINHVIEN";

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
            connetionString = KetNoi.str;
            //connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
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
        private void button1_Click(object sender, EventArgs e)
        {
            frmThemSV frmthem = new frmThemSV(MaLop);
            frmthem.Show();
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            frmChonLop frm = new frmChonLop();
            frm.Show();
        }      
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string str;
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;
                frmSuaThongTinSinhVien frm = new frmSuaThongTinSinhVien(str);
                frm.Show();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn một hàng bạn muốn thao tác ^^!", "THÔNG BÁO");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;      // MaSV
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM SinhVien WHERE MaSV='" + str + "'";
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN XOÁ SINH VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("XÓA THÀNH CÔNG", "THÔNG BÁO");
                    this.Close();
                    frmDSSV frm = new frmDSSV(MaLop);
                    frm.Show();
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn một hàng bạn muốn thao tác ^^!", "THÔNG BÁO");
            }

        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                int row = this.listView1.SelectedItems[0].Index;
                str = this.listView1.Items[row].SubItems[0].Text;
                frmChiTietSinhVien frm = new frmChiTietSinhVien(str);
               
                frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn một hàng bạn muốn thao tác ^^!", "THÔNG BÁO");
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
                    cmd.CommandText = "SELECT MaSV,TenSV,CMND,GioiTinh,NgaySinh,QueQuan,SdtSV,TenLop FROM SINHVIEN,LOP where SINHVIEN.MaLop = LOP.MaLop AND SINHVIEN.MaLop = '" + MaLop + "' and LOP.MaLop = '" + MaLop + "' and SINHVIEN.MaSV='" + txtTuKhoa.Text + "'";
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
                            if (Convert.ToInt16(td.Rows[i][3]) == 0)
                                item.SubItems.Add("Nam");
                            else
                                item.SubItems.Add("Nu");
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            item.SubItems.Add(td.Rows[i][6].ToString());
                            item.SubItems.Add(td.Rows[i][7].ToString());
                            listView1.Items.Add(item);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Mã " + txtTuKhoa.Text);
                        Form2_Load(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT MaSV,TenSV,CMND,GioiTinh,NgaySinh,QueQuan,SdtSV,TenLop FROM SINHVIEN,LOP where SINHVIEN.MaLop = LOP.MaLop AND SINHVIEN.MaLop = '" + MaLop + "' and LOP.MaLop = '" + MaLop + "' and SINHVIEN.TenSV like N'%" + txtTuKhoa.Text + "%'";
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
                            if (Convert.ToInt16(td.Rows[i][3]) == 0)
                                item.SubItems.Add("Nam");
                            else
                                item.SubItems.Add("Nu");
                            item.SubItems.Add(td.Rows[i][4].ToString());
                            item.SubItems.Add(td.Rows[i][5].ToString());
                            item.SubItems.Add(td.Rows[i][6].ToString());
                            item.SubItems.Add(td.Rows[i][7].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không Tồn Tại Sinh Viên Có Tên " + txtTuKhoa.Text);
                        Form2_Load(sender, e);
                    }
                    //this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    //this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else { 
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    Form2_Load(sender, e);}
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                Form2_Load( sender,  e);
            }
        }
    }
}
