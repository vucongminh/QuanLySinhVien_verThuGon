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
        frmTinhDiemTB frm1 = new frmTinhDiemTB();
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
            cmd.CommandText = "SELECT TenHP FROM HocPhan WHERE MaHP NOT IN ( SELECT LHP.MaHP FROM dbo.BANGDIEM BD,dbo.LOPHOCPHAN LHP WHERE BD.MaLHP=LHP.MaLHP AND BD.MaSV='" + SinhVien_ID + "')";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cboMonHoc.Items.Add(td.Rows[i][0]);
            }
            con.Close();
            frm1.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                try
                {
                    if (cboMonHoc.SelectedItem.ToString() != "" && cbLHP.SelectedItem.ToString() != "" && txtLanThi.Text != "" && txtDiemCC.Text != "" && txtDiemTX.Text != "" && txtDiemThi.Text != "" && txtDiemTB.Text != "" && cbGhiChu.SelectedItem.ToString() != "")
                    {
                        string GhiChu;
                        GhiChu = cbGhiChu.SelectedItem.ToString();
                        string MaLHP = cbLHP.SelectedItem.ToString();
                        if (IsNumber(txtLanThi.Text))
                        {
                            int LanThi;
                            LanThi = Convert.ToInt16(txtLanThi.Text);
                            if (IsNumber(txtDiemCC.Text))
                            {
                                double DiemCC;
                                DiemCC = Convert.ToDouble(txtDiemCC.Text);
                                if (DiemCC >= 0 && DiemCC <= 10)
                                {
                                    if (IsNumber(txtDiemTX.Text))
                                    {
                                        double TX;
                                        TX = Convert.ToDouble(txtDiemTX.Text);
                                        if (TX >= 0 && TX <= 10)
                                        {
                                            if (IsNumber(txtDiemThi.Text))
                                            {
                                                double Thi;
                                                Thi = Convert.ToDouble(txtDiemThi.Text);
                                                if (Thi >= 0 && Thi <= 10)
                                                {
                                                    if (IsNumber(txtDiemTB.Text))
                                                    {
                                                        double TB;
                                                        TB = Convert.ToDouble(txtDiemTB.Text);
                                                        if (TB >= 0 && TB <= 10)
                                                        {
                                                            cmd.CommandText = "INSERT INTO BANGDIEM VALUES('" + SinhVien_ID + "','" + MaLHP + "','" + DiemCC + "'," + TX + "," + Thi + "," + TB + "," + LanThi + ",N'" + GhiChu + "','')";
                                                            DialogResult result;
                                                            result = MessageBox.Show("BẠN CÓ MUỐN THÊM MỚI KẾT QUẢ HỌC TẬP CHO SINH VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                            if (result == DialogResult.Yes)
                                                            {
                                                                cmd.ExecuteNonQuery();
                                                                MessageBox.Show("Thêm Dữ Liệu Thành Công!");
                                                                this.Close();
                                                                frm1.Close();
                                                                frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
                                                                frm.Show();
                                                            }
                                                            con.Close();
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Điểm trung bình chỉ có giá trị trong [0,10]");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Điểm trung binh nhập không đúng!");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Điểm thi chỉ có giá trị trong [0,10]");
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Điểm thi nhập không đúng!");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Điểm thường xuyên chỉ có giá trị trong [0,10]");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Điểm thường xuyên nhập không đúng!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Điểm chuyên cần chỉ có giá trị trong [0,10]");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Điểm chuyên cần nhập không đúng!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lần thi nhập không đúng!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                    }
                }
                catch
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frm1.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }


        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TenHP;
            TenHP = cboMonHoc.SelectedItem.ToString();
            cbLHP.Items.Clear();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from HocPhan where TenHP=N'" + TenHP + "'";
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
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
