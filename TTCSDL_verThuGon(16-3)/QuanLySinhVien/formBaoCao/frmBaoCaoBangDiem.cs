using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLySinhVien.formBaoCao
{
    public partial class frmBaoCaoBangDiem : Form
    {
        public frmBaoCaoBangDiem()
        {
            InitializeComponent();
        }

        private void frmBaoCaoBangDiem_Load(object sender, EventArgs e)
        {
            InBangDiem rpt = new InBangDiem();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            //SqlDataAdapter dap = new SqlDataAdapter("Select BANGDIEM.MaSV, SINHVIEN.TenSV, HOCPHAN.MaHP,HOCPHAN.TenHP,DiemKT,DiemKTGiuaKy,DiemThi,DiemTongKet,LanThi from HOCPHAN, SINHVIEN, BANGDIEM, LOPHOCPHAN where SINHVIEN.MaSV = BANGDIEM.MaSV and BANGDIEM.MaLHP = LOPHOCPHAN.MaLHP and LOPHOCPHAN.MaHP = HOCPHAN.MaHP", con);
            SqlDataAdapter dap = new SqlDataAdapter("Select MaSV, MaLHP, DiemKT,DiemKTGiuaKy,DiemThi,DiemTongKet,LanThi,GhiChu,MaHP,NgayNhap from HOCPHAN, BANGDIEM", con);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            rpt.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rpt;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        } 
    }
}
