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
    public partial class frmSuaBoMon : Form
    {
        string MaBM;
        public frmSuaBoMon(string Ma)
        {
            MaBM = Ma;
            InitializeComponent();
        }

        private void frmSuaKhoa_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT MaChuNhiemBM FROM BOMON WHERE MaBM='" + MaBM + "'";
            SqlDataReader rd1;
            rd1 = cmd1.ExecuteReader();
            DataTable td1 = new DataTable();
            td1.Load(rd1);
            this.cbxMaCNBM.Items.Add(td1.Rows[0][0]);

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandText = "Select MaGV from GIAOVIEN where MaGV not in ( select MaChuNhiemBM from BOMON)";
            SqlDataReader rd2;
            rd2 = cmd2.ExecuteReader();
            DataTable td2 = new DataTable();
            td2.Load(rd2);
            for (int i = 0; i < td2.Rows.Count; i++)
            {
                this.cbxMaCNBM.Items.Add(td2.Rows[i][0]);
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TenBM,MaChuNhiemBM,TenGV FROM BOMON BM,GIAOVIEN GV WHERE BM.MaChuNhiemBM=GV.MaGV AND BM.MaBM='" + MaBM + "'";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            this.txtMaBM.Text = MaBM;
            this.txtTenBM.Text = td.Rows[0][0].ToString();
            this.cbxMaCNBM.Text = td.Rows[0][1].ToString();
            this.txtTenCNBM.Text = td.Rows[0][2].ToString();
            con.Close();
        }

        private void btnSuaKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                if (txtTenBM.Text != "" && cbxMaCNBM.SelectedItem.ToString() != "")
                {
                    cmd.CommandText = "UPDATE BOMON SET TenBM=N'" + txtTenBM.Text + "',MaChuNhiemBM='" + cbxMaCNBM.Text + "' WHERE MaBM='" + MaBM + "'";
                    DialogResult result;
                    result = MessageBox.Show("BẠN CÓ MUỐN THAY ĐỔI THÔNG TIN KHÔNG?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("CẬP NHẬT DỮ LIỆU THÀNH CÔNG", "THÔNG BÁO");
                        this.Close();
                        frmDSBoMon frm = new frmDSBoMon();
                        frm.Show();
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
            frmDSBoMon frm = new frmDSBoMon();
            frm.Show();
        }

        private void cbbMaCNBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenGV from GIAOVIEN where MaGV = '" + cbxMaCNBM.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenCNBM.Text = a["TenGV"].ToString();
            }
        }
    }
}
