using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class frmTinhDiemTB : Form
    {
        public frmTinhDiemTB()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
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
                                    double TB;
                                    TB = DiemCC * 0.1 + TX * 0.3 + Thi * 0.6;
                                    txtDiemTB.Text = TB.ToString();
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
