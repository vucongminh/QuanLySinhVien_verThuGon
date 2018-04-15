using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using QuanLySinhVien.formDangKi;
namespace QuanLySinhVien
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            quảnLýNgườiDùngToolStripMenuItem.Enabled = false;
            

        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChonLop frm2 = new frmChonLop();
            frm2.MdiParent = this;
            frm2.Show();
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSBoMon frm = new frmDSBoMon();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmDSLop frm = new frmDSLop();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void họcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSHocPhan frm = new frmDSHocPhan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lớpHọcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmDangNhapAdmin frm = new frmDangNhapAdmin();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tìmKiếmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimSinhVien frm = new frmTimSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNhapHeThong frm = new frmDangNhapHeThong();
            frm.MdiParent = this;
            frm.Show();
            
        }
        public void EnableMenu()
        {
            sinhViênToolStripMenuItem.Enabled = true;
            khoaToolStripMenuItem.Enabled = true;
            lớpToolStripMenuItem.Enabled = true;
            mônHọcToolStripMenuItem.Enabled = true;
            tìmKiếmSinhViênToolStripMenuItem.Enabled = true;
            đăngNhậpToolStripMenuItem.Enabled = false;
            đăngXuấtToolStripMenuItem.Enabled = true;
            quảnLýNgườiDùngToolStripMenuItem.Enabled = true;
            saoLưuDữLiệuToolStripMenuItem.Enabled = true;
            phụcHồiDữLiệuToolStripMenuItem.Enabled = true;
            đăngKýToolStripMenuItem.Enabled = true;
            tìmKiếmLớpToolStripMenuItem.Enabled = true;
            báoCáoToolStripMenuItem.Enabled = true;
            inBảngĐiểmToolStripMenuItem.Enabled = true;
            inBảnĐăngKýToolStripMenuItem.Enabled = true;
            saoLưuDữLiệuToolStripMenuItem.Enabled = true;
            phụcHồiDữLiệuToolStripMenuItem.Enabled = true;
            đăngKýToolStripMenuItem.Enabled = true;
        }
        public void MenuForOnlyRead()
        {
            sinhViênToolStripMenuItem.Enabled = true;
            khoaToolStripMenuItem.Enabled = true;
            lớpToolStripMenuItem.Enabled = true;
            mônHọcToolStripMenuItem.Enabled = true;
            tìmKiếmSinhViênToolStripMenuItem.Enabled = true;
            đăngNhậpToolStripMenuItem.Enabled = false;
            đăngXuấtToolStripMenuItem.Enabled = true;
            quảnLýNgườiDùngToolStripMenuItem.Enabled = false;
            saoLưuDữLiệuToolStripMenuItem.Enabled = false;
            phụcHồiDữLiệuToolStripMenuItem.Enabled = false;
            đăngKýToolStripMenuItem.Enabled = true;
            tìmKiếmLớpToolStripMenuItem.Enabled = true;
            báoCáoToolStripMenuItem.Enabled = true;
            inBảngĐiểmToolStripMenuItem.Enabled = true;
            inBảnĐăngKýToolStripMenuItem.Enabled = true;
            đăngKýToolStripMenuItem.Enabled = true;


        }
        public void DisableMenu()
        {
            quảnLýNgườiDùngToolStripMenuItem.Enabled = false;
            đăngXuấtToolStripMenuItem.Enabled = false;
            sinhViênToolStripMenuItem.Enabled = false;
            khoaToolStripMenuItem.Enabled = false;
            lớpToolStripMenuItem.Enabled = false;
            mônHọcToolStripMenuItem.Enabled = false;
            tìmKiếmSinhViênToolStripMenuItem.Enabled = false;
            saoLưuDữLiệuToolStripMenuItem.Enabled = false;
            phụcHồiDữLiệuToolStripMenuItem.Enabled = false;
            đăngKýToolStripMenuItem.Enabled = false;
            tìmKiếmLớpToolStripMenuItem.Enabled = false; 
            báoCáoToolStripMenuItem.Enabled = false;
            đăngKýToolStripMenuItem.Enabled = false;
            //thôngTinToolStripMenuItem.Enabled = false;
            //tìmKiếmToolStripMenuItem.Enabled = false;
            //trợGiúpToolStripMenuItem1.Enabled = false;


        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableMenu();
            đăngNhậpToolStripMenuItem.Enabled = true;
        }


        private void trợGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saoLưuDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                try
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = KetNoi.str;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "BACKUP DATABASE [QuanLySV24] TO DISK='E:\\QLSV24.bak'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Backup Database Quản Lý Sinh Viên Thành Công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString(),"BACKUP DATABASE");
                    return;
                }
                
            
        }

        private void phụcHồiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = KetNoi.str;
                con.Open();
                string strpath = "E:\\QLSV24.bak";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "USE master RESTORE DATABASE [QuanLySV24] FROM DISK='" + strpath + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Restore Database Quản Lý Sinh Viên Thành Công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR RESTORE DATABASE");
                return;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void dDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangKi frmDangKi = new frmDangKi();
            frmDangKi.Show();

        }

        private void tìmKiếmLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmTimLop frm = new frmTimLop();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
