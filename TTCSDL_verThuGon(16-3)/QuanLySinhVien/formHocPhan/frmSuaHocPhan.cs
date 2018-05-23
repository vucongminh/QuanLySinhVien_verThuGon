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
    public partial class frmSuaHocPhan : Form
    {
        string MaMonHoc;
        public frmSuaHocPhan(string Ma)
        {
            MaMonHoc = Ma;
            InitializeComponent();
        }

        private void frmSuaMonHoc_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaBM from BOMON";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxMaBM.Items.Add(td1.Rows[i][0]);
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MaHP,TenHP,SoTC,HocKy,MaBM FROM HOCPHAN WHERE MaHP='" + MaMonHoc + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaHP.Text = td.Rows[0][0].ToString();
            this.txtTenHP.Text = td.Rows[0][1].ToString();
            this.txtSoTC.Text = td.Rows[0][2].ToString();
            this.txtHocKy.Text = td.Rows[0][3].ToString();
            this.cbxMaBM.Text = td.Rows[0][4].ToString();

            con.Close();
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
                if (txtTenHP.Text != "" && txtSoTC.Text != "" && txtHocKy.Text != "" && cbxMaBM.SelectedItem.ToString() != "")
                {
                    if (IsNumber(txtSoTC.Text))
                    {
                        int SoTC = Convert.ToInt16(txtSoTC.Text);
                        if (IsNumber(txtHocKy.Text))
                        {
                            int HocKy = Convert.ToInt16(txtHocKy.Text);
                            string MaBM;
                            MaBM = cbxMaBM.SelectedItem.ToString();
                            cmd.CommandText = "UPDATE HOCPHAN SET TenHP=N'" + txtTenHP.Text + "',MaBM='" + MaBM + "',SoTC=" + SoTC + ",HocKy=" + HocKy + "WHERE MaHP='" + MaMonHoc + "'";
                            DialogResult result;
                            result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                                this.Close();
                                frmDSHocPhan frm = new frmDSHocPhan();
                                frm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Học kỳ nhập không đúng!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số tín chỉ nhập không đúng!");
                    }

                }
                else
                {
                    MessageBox.Show("Bạn phải nhập đủ các trường bắt buộc!", "Thông Báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSHocPhan frm = new frmDSHocPhan();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSHocPhan frm = new frmDSHocPhan();
            frm.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbbMaBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenBM from BOMON where MaBM = '" + cbxMaBM.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenBM.Text = a["TenBM"].ToString();

            }
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
