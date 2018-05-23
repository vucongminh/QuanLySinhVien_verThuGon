using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLySinhVien
{
    class KetNoi
    {
        public static string str = @"Data Source=DESKTOP-R2MHQ1A;Initial Catalog=QuanLySV24;Integrated Security=True";
        //Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySV24;Integrated Security=True

        public string TaoKetNoi()
        {
            return (@"Data Source=DESKTOP-R2MHQ1A;Initial Catalog=QuanLySV24;Integrated Security=True");
        }

        SqlConnection SqlCon;

        public void Connect()
        {
            SqlCon = new SqlConnection(TaoKetNoi());
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
            SqlCon.Open();
        }
        public void DisConnect()
        {
            SqlCon.Close();
        }

        public SqlDataReader ThucThiTraVe1Record(string sql) // phuong thuc lay ra 1 record
        {
            Connect();
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            Connect();
            SqlDataReader a = cmd.ExecuteReader();
            DisConnect();
            cmd.Dispose();
            return a;
        }
    }
}
