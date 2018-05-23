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
    public partial class frmSuaLopHocPhan : Form
    {
        string MaLopHocPhan;
        public frmSuaLopHocPhan(string Ma)
        {
            MaLopHocPhan = Ma;
            InitializeComponent();
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
                if (cbxMaHP.SelectedItem.ToString() != "" && cbxMaGV.SelectedItem.ToString() != "" && cbxDiaDiem.Text != "" && txtSoTC.Text != "")
                {
                    if (IsNumber(txtSoTC.Text))
                    {
                        int SoTC = Convert.ToInt16(txtSoTC.Text);
                        cmd.CommandText = "UPDATE LOPHOCPHAN SET MaHP='" + cbxMaHP.Text + "',MaGV='" + cbxMaGV.Text + "',DiaDiemTCHP='" + cbxDiaDiem.Text + "',SoTC=" + SoTC + "WHERE MaLHP='" + MaLopHocPhan + "'";
                        DialogResult result;
                        result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                            this.Close();
                            frmDSLopHocPhan frm = new frmDSLopHocPhan();
                            frm.Show();
                        }
                        con.Close();
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

        private void frmSuaLopHocPhan_Load_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "Select MaHP from HOCPHAN";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxMaHP.Items.Add(td2.Rows[i][0]);
            }

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "Select MaGV from GIAOVIEN";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            for (int i = 0; i < td1.Rows.Count; i++)
            {
                this.cbxMaGV.Items.Add(td1.Rows[i][0]);
            }

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = con;
            cmd3.CommandText = "Select DiaDiemTCHP from LOPHOCPHAN ";
            SqlDataReader rd3;
            rd3 = cmd3.ExecuteReader();
            DataTable td3 = new DataTable();
            td3.Load(rd3);
            for (int i = 0; i < td3.Rows.Count; i++)
            {
                this.cbxDiaDiem.Items.Add(td3.Rows[i][0]);
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MaLHP,MaHP,MaGV,DiaDiemTCHP,SoTC FROM LOPHOCPHAN WHERE MaLHP='" + MaLopHocPhan + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaLHP.Text = td.Rows[0][0].ToString();
            this.cbxMaHP.Text = td.Rows[0][1].ToString();
            this.cbxMaGV.Text = td.Rows[0][2].ToString();
            this.cbxDiaDiem.Text = td.Rows[0][3].ToString();
            this.txtSoTC.Text = td.Rows[0][4].ToString();
            con.Close();
        }

        private void txtMaLHP_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.Show();
        }

        private void txtDiaDiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cbbMaHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenHP,SoTC from HOCPHAN where MaHP = '" + cbxMaHP.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenHP.Text = a["TenHP"].ToString();
                txtSoTC.Text = a[1].ToString();
            }
        }

        private void cbbMaGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbxMaGV.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenGV.Text = a["TenGV"].ToString();

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
