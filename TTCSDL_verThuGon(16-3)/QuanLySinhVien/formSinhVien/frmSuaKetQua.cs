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
        frmTinhDiemTB frm1 = new frmTinhDiemTB();
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
            cmd.CommandText = "SELECT h.TenHP,b.LanThi,DiemKT,DiemKTGiuaKy,DiemThi,DiemTongKet,GhiChu FROM BANGDIEM as b,LOPHOCPHAN as l,HOCPHAN as h WHERE b.MaSV ='" + SinhVien_ID + "' and b.LanThi ='"+LanThi+"' and b.MaLHP='"+LopHocPhan_ID+"'and l.MaLHP=b.MaLHP and l.MaHP=h.MaHP";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMonHoc.Text = td.Rows[0][0].ToString();
            cmd.CommandText = "SELECT * FROM LOPHOCPHAN as l,HocPhan as h WHERE l.MaHP=h.MaHP and h.TenHp=N'" + txtMonHoc.Text + "'";
            SqlDataReader rd1;
            rd1 = cmd.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbLHP.Items.Add(td1.Rows[i][0]);
            }
            this.cbLHP.Text = LopHocPhan_ID;
            this.txtLanThi.Text = td.Rows[0][1].ToString();
            this.txtDiemCC.Text = td.Rows[0][2].ToString();
            this.txtDiemTX.Text = td.Rows[0][3].ToString();
            this.txtDiemThi.Text = td.Rows[0][4].ToString();
            this.txtDiemTB.Text = td.Rows[0][5].ToString();
            this.cbGhiChu.Text = td.Rows[0][6].ToString();
            con.Close();
            frm1.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frm1.Close();
            frmKetQuaHocTap frm = new frmKetQuaHocTap(SinhVien_ID);
            frm.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
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
                    if (cbLHP.SelectedItem.ToString() != "" && txtLanThi.Text != "" && txtDiemCC.Text != "" && txtDiemTX.Text != "" && txtDiemThi.Text != "" && txtDiemTB.Text != "" && cbGhiChu.SelectedItem.ToString() != "")
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
                                                            cmd.CommandText = "UPDATE BANGDIEM SET MaLHP='" + MaLHP + "',DiemKT=" + DiemCC + ",DiemKTGiuaKy=" + TX + ",DiemThi=" + Thi + ",DiemTongKet=" + TB + ",Lanthi=" + LanThi + ",Ghichu =N'" + GhiChu + "' WHERE MaSV='" + SinhVien_ID + "' AND MaLHP='" + LopHocPhan_ID + "' AND LanThi=" + LanThi + "";
                                                            DialogResult result;
                                                            result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI KẾT QUẢ HỌC TẬP CHO SINH VIÊN NÀY KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                                            if (result == DialogResult.Yes)
                                                            {
                                                                cmd.ExecuteNonQuery();
                                                                MessageBox.Show("CẬP NHẬT THÀNH CÔNG!");
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

        private void cbLHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

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
