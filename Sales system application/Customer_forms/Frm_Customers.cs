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

namespace Sales_system_application.Customer_forms
{
    public partial class Frm_Customers : Form
    {
        public Frm_Customers()
        {
            InitializeComponent();
        }
        DbConnection db = new DbConnection();
        DataTable table = new DataTable();
        //
        //  Method Increase The ID By One 
        //  Clear The Textboxes
        //  Allows only New And Add button
        void AutoNumber()
        {
            table.Clear();
            table = db.ReadData("select max(Cust_ID) from Customers","");
            if ((table.Rows[0][0].ToString()) == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(table.Rows[0][0]) + 1).ToString();
            }
            txtName.Clear();
            txtNotes.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSearch.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnDeleteAll.Enabled = false;
        }
        int row;
        private void Shows()
        {
            table.Clear();
            table = db.ReadData("select * from Customers","");
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Please Make sure you add new Customers.");
            }
            else
            {
                txtID.Text = table.Rows[row][0].ToString();
                txtName.Text = table.Rows[row][1].ToString();
                txtAddress.Text = table.Rows[row][2].ToString();
                txtPhone.Text = table.Rows[row][3].ToString();
                txtNotes.Text = table.Rows[row][4].ToString();
            }
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnDeleteAll.Enabled = true;
        }

        private void Frm_Customers_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }
        //Add New Customer
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text =="")
            {
                MessageBox.Show("Enter valid customer name.");
                return;
            }
            db.ExecuteData("insert into Customers values("+txtID.Text+",N'"+txtName.Text+"',N'"+txtAddress.Text+"',N'"+txtPhone.Text+"',N'"+txtNotes.Text+"')", "OK! the info added successfully");
            AutoNumber();
        }
        //New Customer
        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            row = 0;
            Shows();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.ReadData("update Customers set Cust_Name = N'"+txtName.Text+"', Cust_Address = N'"+txtAddress.Text+"', Cust_Phone = N'"+txtPhone.Text+"', Notes = N'"+txtNotes.Text+"' where Cust_ID = '"+txtID.Text+"'","OK! Upadated successfuly.");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Customer?","Delete Customer",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                db.ReadData("delete from Customers where Cust_ID = "+txtID.Text+"", "Delete Successfuly!");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete All Customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Customers", "Delete All Successfuly!");
                AutoNumber();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Cust_ID) from Customers", "");
            row = Convert.ToInt32(table.Rows[0][0]) - 1;
            Shows();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Cust_ID) from Customers", "");
            if (Convert.ToInt32(table.Rows[0][0])-1 == row)
            {
                row=0;
                Shows();
            }
            else
            {
                row++;
                Shows();
            }
            
        }

        private void btnPrevouis_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Cust_ID) from Customers", "");
            if (row ==0)
            {
                row= Convert.ToInt32(table.Rows[0][0]) - 1;
                Shows();
            }
            else
            {
                row --;
                Shows();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable = db.ReadData("select * from Customers where Cust_Name like N'%"+txtSearch.Text+"%' ", "");
            try
            {
                txtID.Text = dataTable.Rows[0][0].ToString();
                txtName.Text = dataTable.Rows[0][1].ToString();
                txtAddress.Text = dataTable.Rows[0][2].ToString();
                txtPhone.Text = dataTable.Rows[0][3].ToString();
                txtNotes.Text = dataTable.Rows[0][4].ToString();
            }
            catch (Exception M)
            {
                MessageBox.Show(M.Message);
            }
        }
    }
}
