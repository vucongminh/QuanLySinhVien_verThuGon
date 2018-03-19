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
    public partial class frmThemKetQua : Form
    {
        string SinhVien_ID;
        public frmThemKetQua(string MaSinhVien)
        {
            SinhVien_ID = MaSinhVien;
            InitializeComponent();
        }

        private void frmThemKetQua_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenHP FROM HocPhan  order by MaHP ";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cboMonHoc.Items.Add(td.Rows[i][0]);
            }                      
            con.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;                     
            string MaLHP = cbLHP.SelectedItem.ToString();
            int LanThi;
            LanThi = Convert.ToInt16(txtLanThi.Text);
            double DiemCC;
            DiemCC = Convert.ToDouble(txtDiemCC.Text);
            double TX;
            TX = Convert.ToDouble(txtDiemTX.Text);
            double Thi;
            Thi = Convert.ToDouble(txtDiemThi.Text);
            double TB;
            TB = Convert.ToDouble(txtDiemTB.Text);
            string GhiChu;
            GhiChu = cbGhiChu.SelectedItem.ToString();
            cmd.CommandText = "INSERT INTO BANGDIEM VALUES('" + SinhVien_ID + "','" +MaLHP+ "','"+DiemCC+"'," + TX + "," + Thi + "," + TB + ","+LanThi+",N'"+GhiChu+"','')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Thêm Dữ Liệu Thành Công!");
            this.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }


        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string TenHP;
            TenHP = cboMonHoc.SelectedItem.ToString();
            cmd.CommandText = "select* from HocPhan where TenHP=N'" + TenHP + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            string MaHP = td.Rows[0][0].ToString();
            cmd.CommandText = "SELECT * FROM LOPHOCPHAN WHERE MaHP='" + MaHP + "'";
            SqlDataReader rd1;
            rd1 = cmd.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbLHP.Items.Add(td1.Rows[i][0]);
            }
            con.Close();
        }
    }
}
