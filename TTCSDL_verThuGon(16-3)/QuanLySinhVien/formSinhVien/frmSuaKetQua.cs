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
    public partial class frmSuaKetQua : Form
    {
        int LanThi;
        string SinhVien_ID;
        string LopHocPhan_ID;
        public frmSuaKetQua(string MaSinhVien,string MaLHP,int lanthi)

        {
            LanThi = lanthi;
            LopHocPhan_ID = MaLHP;
            SinhVien_ID = MaSinhVien;           
            InitializeComponent();
        }

        private void frmSuaKetQua_Load(object sender, EventArgs e)
        {
           
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT h.TenHP,h.SoTC,b.LanThi,DiemKT,DiemKTGiuaKy,DiemThi,DiemTongKet,GhiChu FROM BANGDIEM as b,LOPHOCPHAN as l,HOCPHAN as h WHERE b.MaSV ='" + SinhVien_ID + "' and b.LanThi ='"+LanThi+"' and b.MaLHP='"+LopHocPhan_ID+"'and l.MaLHP=b.MaLHP and l.MaHP=h.MaHP";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.cboMonHoc.Text = td.Rows[0][0].ToString();
            this.cbLHP.Text = LopHocPhan_ID;
            this.txtLanThi.Text = td.Rows[0][2].ToString();
            this.txtDiemCC.Text = td.Rows[0][3].ToString();
            this.txtDiemTX.Text = td.Rows[0][4].ToString();
            this.txtDiemThi.Text = td.Rows[0][5].ToString();
            this.txtDiemTB.Text = td.Rows[0][6].ToString();
            this.cbGhiChu.Text = td.Rows[0][7].ToString();                   
            cmd.CommandText = "SELECT * FROM LOPHOCPHAN as l,HocPhan as h WHERE l.MaHP=h.MaHP and h.TenHp=N'" + cboMonHoc.Text + "'";
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try {
            string MaLHP;
            MaLHP=cbLHP.SelectedItem.ToString();
            float diemCC = float.Parse(txtDiemCC.Text);
            float diemTX = float.Parse(txtDiemTX.Text);
            float diemThi = float.Parse(txtDiemThi.Text);
            float diemTB = float.Parse(txtDiemTB.Text);
            string GhiChu = cbGhiChu.SelectedItem.ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;           
            cmd.CommandText = "UPDATE BANGDIEM SET MaLHP='" + MaLHP + "',DiemKT=" + diemCC + ",DiemKTGiuaKy="+diemTX+",DiemThi="+diemThi+",DiemTongKet="+diemTB+",Lanthi="+txtLanThi.Text+",Ghichu =N'"+GhiChu+"' WHERE MaSV='" + SinhVien_ID+ "' AND MaLHP='" + LopHocPhan_ID + "' AND LanThi="+LanThi+"";
            DialogResult result;
            result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("CẬP NHẬT THÀNH CÔNG", "THÔNG BÁO");
            }
            this.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Đảm bảo rằng bạn đã nhập đủ các trường", "THÔNG BÁO");
            }


        }

        private void cbLHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
