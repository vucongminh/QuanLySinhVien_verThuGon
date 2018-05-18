using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QuanLySinhVien.formBaoCao
{
    public partial class frmInBangDiemLop : Form
    {


        public frmInBangDiemLop()
        {
            InitializeComponent();
        }

        private void frmInBangDiemLop_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSetInBangDiemLop.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.DataSetInBangDiemLop.DataTable1);

            
            this.reportViewer1.RefreshReport();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select MaLop from LOP ";
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Load(rd);
            for (int i = 0; i < td.Rows.Count; i++)
            {
                this.cbbMaLop.Items.Add(td.Rows[i][0]);
            }
            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.DataSetInBangDiemLop.Clear();

            this.DataTable2TableAdapter.Fill(this.DataSetInBangDiemLop.DataTable2, cbbMaLop.Text);
            // this.DataTable1TableAdapter.Fill(this.DataSetInBangDiemLop.DataTable1, cbbMaLop.Text);
            this.reportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DataSetInBangDiemLop.Clear();

            this.DataTable1TableAdapter.Fill(this.DataSetInBangDiemLop.DataTable1);
            this.reportViewer1.RefreshReport();
        }

        private void cbbMaLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader a;
            KetNoi kn = new KetNoi();
            string selectTring = "select TenLop from LOP where MaLop = '" + cbbMaLop.Text + "'";
            a = kn.ThucThiTraVe1Record(selectTring);
            while (a.Read())
            {
                txtTenLop.Text = a["TenLop"].ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
