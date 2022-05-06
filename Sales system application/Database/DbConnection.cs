using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_system_application.Database
{
    public class DbConnection
    {
        //
        // Summary:
        //  sales_system database.(relactional database sql server v. 2019)
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PFQ4IML\SQLEXPRESS;Initial Catalog=Sales_System;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        //
        // Summary:
        //   read data from sales_system database
        public DataTable ReadData(string stmt,string message)
        {
            DataTable table = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandText = stmt;
                con.Open();
                table.Load(cmd.ExecuteReader());
                con.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "was successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception M)
            {
                MessageBox.Show(M.Message);
            }
            return table;
        }

        //
        // Summary:
        //   Execute data (Insert Delete Update) In sales_system database
        public bool ExecuteData(string stmt,string message)
        {
            try
            {
                cmd.Connection = con;
                cmd.CommandText = stmt;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                if (message!="")
                {
                    MessageBox.Show(message,"was successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception M)
            {
                MessageBox.Show(M.Message);
                return false;
            }
        }
    }
}


