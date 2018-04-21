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
       
        public frmDangKi()
        {
            InitializeComponent();
          

        }


        private void frmDangKi_Load(object sender,EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT distinct  h.MaHP,h.TenHP,h.SoTC,l.MaLHP,l.DiaDiemTCHP FROM LOPHOCPHAN as l,HOCPHAN as h,BANGDIEM as b where l.MaHP=h.MaHP and l.MaLHP not in (select MaLHP from bangdiem where MaSV='SV0001')order by h.TenHP";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            con.Close();
            gv1.DataSource = td;
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

            foreach (DataGridViewRow item in gv1.Rows)
            {
                item.Cells[5].Value = false;
            }


            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            buttonColumn.HeaderText = "Delete";
            buttonColumn.Name = "button";
            buttonColumn.Text = "Xóa";
            buttonColumn.UseColumnTextForButtonValue=true;

           gv2.Columns.Add(buttonColumn);

        }

        private void gv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 5)
            {
                if ((bool)gv1.Rows[e.RowIndex].Cells[5].Value == false) {
                    int n = gv2.Rows.Add();
                    gv1.Rows[e.RowIndex].Cells[5].Value = true;
                    gv1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                    DataGridViewRow ThemMoi = gv1.CurrentRow;
                    for (int i = 0; i < ThemMoi.Cells.Count - 1; i++)
                    {
                        gv2.Rows[n].Cells[i].Value = ThemMoi.Cells[i].Value.ToString();
                    }
                }
               
            }
        }

        private void gv2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                gv2.Rows[e.RowIndex].Visible = false;
                string value1 = gv2.Rows[e.RowIndex].Cells[3].Value.ToString();
               for(int i = 0; i < gv1.Rows.Count; i++)
                {
                    if (value1 == gv1.Rows[i].Cells[3].Value.ToString())
                    {
                        gv1.Rows[i].Cells[5].Value = false;
                        gv1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }
    }
}
