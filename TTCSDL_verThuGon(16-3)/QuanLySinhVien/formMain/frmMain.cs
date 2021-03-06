﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using QuanLySinhVien.formBaoCao;
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
        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChonLop frm2 = new frmChonLop();
            //frm2.MdiParent = this;
            frm2.Show();
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSBoMon frm = new frmDSBoMon();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("BẠN CÓ MUỐN THOÁT CHƯƠNG TRÌNH KHÔNG ? :(", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                MessageBox.Show("XIN CHÀO HẸN GẶP LẠI !", "THÔNG BÁO");
                Application.Exit();
            }

        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmDSLop frm = new frmDSLop();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void họcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSHocPhan frm = new frmDSHocPhan();
            // frm.MdiParent = this;
            frm.Show();
        }

        private void lớpHọcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSLopHocPhan frm = new frmDSLopHocPhan();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmDangNhapAdmin frm = new frmDangNhapAdmin();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void tìmKiếmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimSinhVien frm = new frmTimSinhVien();
            //frm.MdiParent = this;
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
            tìmKiếmLớpToolStripMenuItem.Enabled = true;
            báoCáoToolStripMenuItem.Enabled = true;
            inBảngĐiểmToolStripMenuItem.Enabled = false;
            inBảnĐăngKýToolStripMenuItem.Enabled = false;
            saoLưuDữLiệuToolStripMenuItem.Enabled = true;
            phụcHồiDữLiệuToolStripMenuItem.Enabled = true;
            đăngKýToolStripMenuItem.Enabled = false;
            giáoViênToolStripMenuItem.Enabled = true;
            thôngTinToolStripMenuItem.Enabled = true;
            tìmKiếmToolStripMenuItem.Enabled = true;
            trợGiúpToolStripMenuItem1.Enabled = true;
            inBảngĐIểmLớpToolStripMenuItem.Enabled = true;
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
            tìmKiếmLớpToolStripMenuItem.Enabled = true;
            báoCáoToolStripMenuItem.Enabled = true;
            inBảngĐiểmToolStripMenuItem.Enabled = true;
            inBảnĐăngKýToolStripMenuItem.Enabled = true;
            đăngKýToolStripMenuItem.Enabled = true;
            giáoViênToolStripMenuItem.Enabled = true;
            thôngTinToolStripMenuItem.Enabled = true;
            tìmKiếmToolStripMenuItem.Enabled = true;
            trợGiúpToolStripMenuItem1.Enabled = true;
            inBảngĐIểmLớpToolStripMenuItem.Enabled = false;


        }
        public void MenuForReadWrite()
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
            tìmKiếmLớpToolStripMenuItem.Enabled = true;
            báoCáoToolStripMenuItem.Enabled = true;
            inBảngĐiểmToolStripMenuItem.Enabled = false;
            inBảnĐăngKýToolStripMenuItem.Enabled = false;
            đăngKýToolStripMenuItem.Enabled = false;
            giáoViênToolStripMenuItem.Enabled = true;
            thôngTinToolStripMenuItem.Enabled = true;
            tìmKiếmToolStripMenuItem.Enabled = true;
            trợGiúpToolStripMenuItem1.Enabled = true;
            inBảngĐIểmLớpToolStripMenuItem.Enabled = false;

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
            thôngTinToolStripMenuItem.Enabled = false;
            tìmKiếmToolStripMenuItem.Enabled = false;
            trợGiúpToolStripMenuItem1.Enabled = false;
            inBảngĐIểmLớpToolStripMenuItem.Enabled = false;



        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {

            đăngNhậpToolStripMenuItem.Enabled = true;
            DialogResult result;
            result = MessageBox.Show("BẠN CÓ MUỐN ĐĂNG XUẤT KHÔNG?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("ĐĂNG XUẤT THÀNH CÔNG !", "THÔNG BÁO");
                DisableMenu();
            }


        }


        private void trợGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saoLưuDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackUpRestore frm = new frmBackUpRestore();
            frm.MdiParent = this;
            frm.Show();
        }

        private void phụcHồiDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackUpRestore frm = new frmBackUpRestore();
            frm.MdiParent = this;
            frm.Show();
        }
        private void dDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void đăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangKi frm = new frmDangKi();

            frm.Show();
        }

        private void tìmKiếmLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimLop frm = new frmTimLop();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void tìmKiếmToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void giáoViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSGiaoVien frm = new frmDSGiaoVien();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void inBảngĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInBangDiem frm = new frmInBangDiem();
            // frm.MdiParent = this;
            frm.Show();
        }

        private void inBảnĐăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInDangKi frm = new frmInDangKi();
            //frm.MdiParent = this;
            frm.Show();
        }

        private void inBảngĐIểmLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInBangDiemLop frm = new frmInBangDiemLop();
            frm.Show();
        }
    }
}
