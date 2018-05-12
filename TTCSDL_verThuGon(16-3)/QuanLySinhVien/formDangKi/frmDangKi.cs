using QuanLySinhVien.formDangKi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace QuanLySinhVien
{
    public partial class frmDangKi : Form
    {
        public static string username = string.Empty;

        public frmDangKi()
        {
            InitializeComponent();
            btnIn.BackColor = Color.Gray;
            btnIn.Enabled = false;

        }


        private void frmDangKi_Load(object sender,EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT distinct  h.MaHP,h.TenHP,h.SoTC,l.MaLHP,l.DiaDiemTCHP FROM LOPHOCPHAN as l,HOCPHAN as h,BANGDIEM as b where l.MaHP=h.MaHP and  l.MaHP not in (select LOPHOCPHAN.MaHP from bangdiem,LOPHOCPHAN where bangdiem.MaLHP=LOPHOCPHAN.MaLHP and BANGDIEM.MaSV='"+username+"') and l.MaHP not in (select LOPHOCPHAN.MaHP from DANGKI,LOPHOCPHAN where DANGKI.MaLHP=LOPHOCPHAN.MaLHP and DANGKI.MaSV='"+username+"') order by h.TenHP";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT TenSV,MaLop from SINHVIEN where MaSv='" + username + "'";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);

            con.Close();
            gv1.DataSource = td;
            //gv1.Columns["MaHP"].SortMode=DataGridViewColumnSortMode.NotSortable;
            //gv1.Columns["TenHP"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //gv1.Columns["SoTC"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //gv1.Columns["MaLHP"].SortMode = DataGridViewColumnSortMode.NotSortable;
            //gv1.Columns["DiaDiemTCHP"].SortMode = DataGridViewColumnSortMode.NotSortable;
            foreach (DataGridViewColumn column in gv1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            gv1.Columns[0].HeaderText = "Mã Học Phần";
            gv1.Columns[1].HeaderText = "Tên Học Phần";
            gv1.Columns[2].HeaderText = "Số TC";
            gv1.Columns[3].HeaderText = "Mã Lớp Học Phần";
            gv1.Columns[4].HeaderText = "Địa Điểm";


            DataGridViewCheckBoxColumn checkcolumn1 = new DataGridViewCheckBoxColumn();
            checkcolumn1.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.DisplayedCells;
            checkcolumn1.CellTemplate = new DataGridViewCheckBoxCell();
            checkcolumn1.HeaderText = "Đăng Kí";
            checkcolumn1.Name = "CheckBoxes";
            gv1.Columns.Add(checkcolumn1);
            gv2.Columns.Add("MaMH", "Mã Học Phần");
            gv2.Columns.Add("TenHP", "Tên Học Phần");
            gv2.Columns.Add("SoTC", "Số TC");
            gv2.Columns.Add("MaLHP", "Mã Lớp Học Phần");
            gv2.Columns.Add("DiaDiem", "Địa Điểm");

            
            lblTen.Text = td1.Rows[0][0].ToString();
            lblLop.Text = td1.Rows[0][1].ToString();
            lblMa.Text = username;

            foreach (DataGridViewRow item in gv1.Rows)
            {
                item.Cells[5].Value = false;
            }


            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            buttonColumn.HeaderText = "Delete";
            buttonColumn.Name = "button";
            buttonColumn.Text = "Xóa";
            buttonColumn.UseColumnTextForButtonValue = true;

            gv2.Columns.Add(buttonColumn);

        }

        private void gv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 5 && e.RowIndex>=0)
            {
                if ((bool)gv1.Rows[e.RowIndex].Cells[5].Value == false)
                {
                    int n = gv2.Rows.Add();
                    gv1.Rows[e.RowIndex].Cells[5].Value = true;
                    gv1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;

                    DataGridViewRow ThemMoi = gv1.CurrentRow;
                    for (int i = 0; i < ThemMoi.Cells.Count - 1; i++)
                    {
                        gv2.Rows[n].Cells[i].Value = ThemMoi.Cells[i].Value.ToString();
                    }

                    string valueMaHP = gv1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for(int i = 0; i < gv1.Rows.Count; i++)
                    {
                        if (gv1.Rows[i].Cells[0].Value.ToString() == valueMaHP&& i!=e.RowIndex)
                        {                           
                            gv1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                            gv1.Rows[i].Cells[5].Value = true;

                        }
                    }

                }

            }
        }

        private void gv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex>=0)
            {
               
                string value1 = gv2.Rows[e.RowIndex].Cells[0].Value.ToString();
               for(int i = 0; i < gv1.Rows.Count; i++)
                {
                    if (value1 == gv1.Rows[i].Cells[0].Value.ToString())
                    {
                        gv1.Rows[i].Cells[5].Value = false;
                        gv1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        
                    }
                }
                gv2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmInDangKi frm = new frmInDangKi();
            frm.Show();
        }

        private void btnGhiNhan_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            if (gv2.Rows.Count > 0)
            {
                DialogResult result;
                result = MessageBox.Show("BẠN CÓ MUỐN GHI NHẬN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < gv2.Rows.Count; i++)
                    {

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "Insert into DANGKI Values('" + username + "','" + gv2.Rows[i].Cells[3].Value.ToString() + "','','')";
                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                    MessageBox.Show("GHI NHẬN THÀNH CÔNG", "THÔNG BÁO");
                    btnIn.Enabled = true;
                    btnIn.BackColor = Color.Snow;
                }
            }
            else
            {
                MessageBox.Show("BẠN CHƯA CHỌN MÔN HỌC NÀO");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
