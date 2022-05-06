using Sales_system_application.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_system_application.Deserveds
{
    public partial class Frm_ReportDeserved : Form
    {
        public Frm_ReportDeserved()
        {
            InitializeComponent();
        }
        DbConnection db = new DbConnection();
        DataTable table = new DataTable();
        private void Frm_ReportDeserved_Load(object sender, EventArgs e)
        {
            dtpDateFrom.Text = DateTime.Now.ToShortDateString();
            dtpDateTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string d;
            string d1;
            d = dtpDateFrom.Value.ToString("yyyy-MM-dd");
            d1 = dtpDateTo.Value.ToString("yyyy-MM-dd");
            table.Clear();
            table = db.ReadData("select Deserved.Des_ID as 'ID' , Deserved.Price as 'Paid price', Deserved.Date as 'Date paid',Deserved.Notes as 'Notes',Deserved_Type.Name as 'Deserved Type' from Deserved,Deserved_Type where Deserved.Type_ID=Deserved_Type.Des_ID and convert(date,Date,105) between '"+d+"' and '"+d1+"'", "");
            if (table.Rows.Count >= 1)
            {
                decimal sum = 0;
                dgvFilter.DataSource = table;
                for (int i = 0; i <= table.Rows.Count -1; i++)
                {
                    sum += Convert.ToDecimal(table.Rows[i][1]);
                }
                txtTotal.Text = Math.Round( sum, 2).ToString();
            }
            else
            {
                txtTotal.Text ="0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Deserved?", "Delete Deserved", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string d;
                string d1;
                d = dtpDateFrom.Value.ToString("yyyy-MM-dd");
                d1 = dtpDateTo.Value.ToString("yyyy-MM-dd");
                db.ReadData("delete from Deserved where convert(date,Date,105) between '" + d + "' and '" + d1 + "'", "Delete Successfuly!");
            }
        }
    }
}
