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
    public partial class frmDSLopHocPhan : Form
    {

        public static string username = string.Empty;
        public static string pass = string.Empty;
        public frmDSLopHocPhan()
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

        private void frmDSLopHocPhan_Load_1(object sender, EventArgs e)
        {
            this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtTuKhoa.Text = "Ví Dụ: LHP001 / Kỹ Thuật Lập Trình";

            txtTuKhoa.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTuKhoa.AutoCompleteSource = AutoCompleteSource.CustomSource;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MaLHP,LOPHOCPHAN.MaHP,TenHP,LOPHOCPHAN.SoTC,DiaDiemTCHP,TenGV FROM LOPHOCPHAN,HOCPHAN,GIAOVIEN where HOCPHAN.MaHP=LOPHOCPHAN.MaHP and LOPHOCPHAN.MaGV=GIAOVIEN.MaGV";
            //cmd.CommandText = "SELECT MaLHP FROM LOPHOCPHAN ";
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
                item.SubItems.Add(td.Rows[i][4].ToString());
                item.SubItems.Add(td.Rows[i][5].ToString());
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
            string sql = "Select TenHP from HOCPHAN";

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
                MessageBox.Show(ex.ToString());
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
            string sql = "Select distinct MaLHP from LOPHOCPHAN  ";

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
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            this.Close();
            frmThemLopHocPhan frm = new frmThemLopHocPhan();
            frm.Show();
        }

        private void listView1_Click_1(object sender, EventArgs e)
        {
            string str;
            int row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;
            frmSuaLopHocPhan frm = new frmSuaLopHocPhan(str);

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
            string str;
            int row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;
            this.Close();
            frmSuaLopHocPhan frm = new frmSuaLopHocPhan(str);
            frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy Chọn Lớp Học Phần Muốn Sửa !", "Thông Báo");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
            int row = this.listView1.SelectedItems[0].Index;
            str = this.listView1.Items[row].SubItems[0].Text;
            this.Close();
            frmXoaLopHocPhan frm = new frmXoaLopHocPhan(str);
            frm.Show();
        }
            catch (Exception)
            {
                MessageBox.Show("Hãy Chọn Lớp Học Phần Muốn Xóa !", "Thông Báo");
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
            if (txtTuKhoa.Text != " " && txtTuKhoa.Text != "Ví Dụ: LHP001 / Kỹ Thuật Lập Trình")
            {

                if (KiemTra() == 1)
                {
                    cmd.CommandText = "SELECT MaLHP,LOPHOCPHAN.MaHP,TenHP,LOPHOCPHAN.SoTC,DiaDiemTCHP,TenGV FROM LOPHOCPHAN,HOCPHAN,GIAOVIEN where HOCPHAN.MaHP=LOPHOCPHAN.MaHP and LOPHOCPHAN.MaGV=GIAOVIEN.MaGV and LOPHOCPHAN.MaLHP='" + txtTuKhoa.Text + "'";
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
                        MessageBox.Show("Không Tồn Tại Lớp Học Phần Có Mã " + txtTuKhoa.Text);
                        frmDSLopHocPhan_Load_1(sender, e);
                    }
                    this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    this.txtTuKhoa.Text = "Ví Dụ: LHP001 / Kỹ Thuật Lập Trình";
                }
                else if (KiemTra() == 2)
                {
                    cmd.CommandText = "SELECT MaLHP,LOPHOCPHAN.MaHP,TenHP,LOPHOCPHAN.SoTC,DiaDiemTCHP,TenGV FROM LOPHOCPHAN,HOCPHAN,GIAOVIEN where HOCPHAN.MaHP=LOPHOCPHAN.MaHP and LOPHOCPHAN.MaGV=GIAOVIEN.MaGV and HOCPHAN.TenHP like N'%" + txtTuKhoa.Text + "%'";
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
                        MessageBox.Show("Không Tồn Tại Lớp Học Phần Có Tên " + txtTuKhoa.Text);
                        frmDSLopHocPhan_Load_1(sender, e);
                    }
                    //this.txtTuKhoa.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
                    //this.txtTuKhoa.Text = "Ví Dụ: SV0000 / Nguyễn Văn A";
                }
                else
                {
                    MessageBox.Show("Hãy Chọn Chức Năng Tìm Kiếm !");
                    frmDSLopHocPhan_Load_1(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Hãy Nhập Từ Khóa !");
                frmDSLopHocPhan_Load_1(sender, e);
            }
        }
    }
}
