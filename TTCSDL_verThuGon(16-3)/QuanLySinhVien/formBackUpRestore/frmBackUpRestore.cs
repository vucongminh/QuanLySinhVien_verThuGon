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

namespace QuanLySinhVien
{
    public partial class frmBackUpRestore : Form
    {
        public frmBackUpRestore()
        {
            InitializeComponent();
        }

        private void btnBrowseBU_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackUp.Text = dlg.SelectedPath;
                btnBackUp.Enabled = true;
            }
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBackUp.Text == string.Empty)
                {
                    MessageBox.Show("Bạn cần nhập đường dẫn file back up", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtNameFile.Text == string.Empty)
                {
                    MessageBox.Show("Bạn cần nhập tên file back up", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = KetNoi.str;
                    string query = "BACKUP DATABASE QuanLySV24 TO DISK = '" + txtBackUp.Text + "\\" + txtNameFile.Text + ".bak'";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Back up database thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBackUp.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoatBackUp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseRS_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.bak|*.bak";
            dlg.Title = "Database Restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRestore.Text = dlg.FileName;
                btnRestore.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = KetNoi.str;
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string query1 = string.Format("ALTER DATABASE [QuanLySV24] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.ExecuteNonQuery();

                string query2 = "USE MASTER RESTORE DATABASE [QuanLySV24] FROM DISK ='" + txtRestore.Text + "'WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.ExecuteNonQuery();

                string query3 = string.Format("ALTER DATABASE [QuanLySV24] SET MULTI_USER");
                SqlCommand cmd3 = new SqlCommand(query3, con);
                cmd3.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Restore database thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRestore.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoanRestore_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
