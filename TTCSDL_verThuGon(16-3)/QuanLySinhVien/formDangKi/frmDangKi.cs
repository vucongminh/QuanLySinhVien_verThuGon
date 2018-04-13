using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien.formDangKi
{
    public partial class frmDangKi : Form
    {
        int temp = 1;
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
            for (int i = 0; i < td.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem((i+1).ToString());
                item.SubItems.Add(td.Rows[i][0].ToString());
                item.SubItems.Add(td.Rows[i][1].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                item.SubItems.Add(td.Rows[i][2].ToString());
                item.SubItems.Add(td.Rows[i][3].ToString());
                item.SubItems.Add(td.Rows[i][4].ToString());
                listView2.Items.Add(item);
            }
            con.Close();
        }
        private void listView2_ItemActivate(object sender,EventArgs e)
        {
            ListViewItem item = (ListViewItem)listView2.SelectedItems[0].Clone();
            item.SubItems[0].Text = temp.ToString();
            listView1.Items.Add(item);
            listView2.SelectedItems[0].Remove();
            temp = temp + 1;
                 
        }

        
    }
}
