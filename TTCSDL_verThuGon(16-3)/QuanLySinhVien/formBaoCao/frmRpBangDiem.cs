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
    public partial class frmRpBangDiem : Form
    {
        public frmRpBangDiem()
        {
            InitializeComponent();
        }
        protected void textBox1_Focus(Object sender, EventArgs e)
        {
            txtMaSV.Text = "";

        }
        private void getData(AutoCompleteStringCollection dataCollection)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            connetionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True";
            string sql = "Select MaSV from SINHVIEN";

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

        public void laydulieuchocombobox()
        {

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = KetNoi.str;
            con1.Open();
           
            SqlCommand cmd = new SqlCommand("SELECT MaLop FROM LOP ",con1);
            SqlDataAdapter ap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ap.Fill(ds, "LOP");
            cbbMaLop.DataSource = ds.Tables[0];
            cbbMaLop.DisplayMember = "MaLop";
            cbbMaLop.ValueMember = "MaLop";          
         
        }

        private void frmRpBangDiem_Load(object sender, EventArgs e)
        {
            this.txtMaSV.GotFocus += new EventHandler(textBox1_Focus); // enter event==get focus event 
            this.txtMaSV.Text = "Ví Dụ: SV0001";
            txtMaSV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtMaSV.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            txtMaSV.AutoCompleteCustomSource = DataCollection;

            this.laydulieuchocombobox();

            rpBangDiem rpt = new rpBangDiem();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlDataAdapter dap = new SqlDataAdapter("SELECT SINHVIEN.TenSV, BANGDIEM.MaSV, SINHVIEN.MaLop, HOCPHAN.TenHP, LOPHOCPHAN.MaHP, BANGDIEM.DiemKT, BANGDIEM.DiemKTGiuaKy, BANGDIEM.DiemThi,BANGDIEM.DiemTongKet, BANGDIEM.LanThi, BANGDIEM.GhiChu FROM BANGDIEM INNER JOIN LOPHOCPHAN ON BANGDIEM.MaLHP = LOPHOCPHAN.MaLHP INNER JOIN SINHVIEN ON BANGDIEM.MaSV = SINHVIEN.MaSV INNER JOIN LOP ON SINHVIEN.MaSV = LOP.MaLopTruong AND SINHVIEN.MaLop = LOP.MaLop INNER JOIN HOCPHAN ON LOPHOCPHAN.MaHP = HOCPHAN.MaHP", con);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            rpt.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rpt;
            con.Close();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnMaSV_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtMaSV.Text == "Ví Dụ: SV0001")
            {
                //MessageBox.Show("Bạn Chưa Nhập Mã Sinh Viên");
                this.frmRpBangDiem_Load(sender, e);
            }
            else
            {
                rpBangDiem rpt = new rpBangDiem();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlDataAdapter dap = new SqlDataAdapter("SELECT SINHVIEN.TenSV, BANGDIEM.MaSV, SINHVIEN.MaLop, HOCPHAN.TenHP, LOPHOCPHAN.MaHP, BANGDIEM.DiemKT, BANGDIEM.DiemKTGiuaKy, BANGDIEM.DiemThi,BANGDIEM.DiemTongKet, BANGDIEM.LanThi, BANGDIEM.GhiChu FROM BANGDIEM INNER JOIN LOPHOCPHAN ON BANGDIEM.MaLHP = LOPHOCPHAN.MaLHP INNER JOIN SINHVIEN ON BANGDIEM.MaSV = SINHVIEN.MaSV INNER JOIN LOP ON SINHVIEN.MaSV = LOP.MaLopTruong AND SINHVIEN.MaLop = LOP.MaLop INNER JOIN HOCPHAN ON LOPHOCPHAN.MaHP = HOCPHAN.MaHP where BANGDIEM.MaSV ='" + txtMaSV.Text + "' ", con);
                DataSet ds = new DataSet();
                dap.Fill(ds);
                rpt.SetDataSource(ds.Tables[0]);
                crystalReportViewer1.ReportSource = rpt;
            }
        }

        private void cbbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMaLop_Click(object sender, EventArgs e)
        {
           
            rpBangDiem rpt = new rpBangDiem();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlDataAdapter dap = new SqlDataAdapter("SELECT SINHVIEN.TenSV, BANGDIEM.MaSV, SINHVIEN.MaLop, HOCPHAN.TenHP, LOPHOCPHAN.MaHP, BANGDIEM.DiemKT, BANGDIEM.DiemKTGiuaKy, BANGDIEM.DiemThi,BANGDIEM.DiemTongKet, BANGDIEM.LanThi, BANGDIEM.GhiChu FROM BANGDIEM INNER JOIN LOPHOCPHAN ON BANGDIEM.MaLHP = LOPHOCPHAN.MaLHP INNER JOIN SINHVIEN ON BANGDIEM.MaSV = SINHVIEN.MaSV INNER JOIN LOP ON SINHVIEN.MaSV = LOP.MaLopTruong AND SINHVIEN.MaLop = LOP.MaLop INNER JOIN HOCPHAN ON LOPHOCPHAN.MaHP = HOCPHAN.MaHP where SINHVIEN.MaLop ='" + cbbMaLop.SelectedValue.ToString() + "' ", con);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            rpt.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = rpt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
