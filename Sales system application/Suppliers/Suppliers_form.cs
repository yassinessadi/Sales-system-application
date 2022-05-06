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

namespace Sales_system_application.Suppliers
{
    public partial class Suppliers_form : Form
    {
        public Suppliers_form()
        {
            InitializeComponent();
        }

        private void Suppliers_form_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }
        DbConnection db = new DbConnection();
        DataTable table = new DataTable();
        int row;
        //
        //  Method Increase The ID By One 
        //  Clear The Textboxes
        //  Allows only New And Add button
        void AutoNumber()
        {
            table.Clear();
            table = db.ReadData("select max(Sup_ID) from Suppliers", "");
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
        //filter 
        private void Shows()
        {
            table.Clear();
            table = db.ReadData("select * from Suppliers", "");
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Please Make sure you add new Suppliers.");
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

        private void btnFirst_Click(object sender, EventArgs e)
        {
            row = 0;
            Shows();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Sup_ID) from Suppliers", "");
            row = Convert.ToInt32(table.Rows[0][0]) - 1;
            Shows();
        }

        private void btnPrevouis_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Sup_ID) from Suppliers", "");
            if (row == 0)
            {
                row = Convert.ToInt32(table.Rows[0][0]) - 1;
                Shows();
            }
            else
            {
                row--;
                Shows();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Sup_ID) from Suppliers", "");
            if (Convert.ToInt32(table.Rows[0][0]) - 1 == row)
            {
                row = 0;
                Shows();
            }
            else
            {
                row++;
                Shows();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable = db.ReadData("select * from Suppliers where Sup_Name like N'%" + txtSearch.Text + "%' ", "");
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete All Suppliers?", "Delete Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Suppliers", "Delete All Successfuly!");
                AutoNumber();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Supplier?", "Delete Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Suppliers where Sup_ID = " + txtID.Text + "", "Delete Successfuly!");
                AutoNumber();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.ReadData("update Suppliers set Sup_Name = N'" + txtName.Text + "', Sup_Address = N'" + txtAddress.Text + "', Sup_Phone = N'" + txtPhone.Text + "', Notes = N'" + txtNotes.Text + "' where Sup_ID = '" + txtID.Text + "'", "OK! Upadated successfuly.");
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter valid Supplier name.");
                return;
            }
            db.ExecuteData("insert into Suppliers values(" + txtID.Text + ",N'" + txtName.Text + "',N'" + txtAddress.Text + "',N'" + txtPhone.Text + "',N'" + txtNotes.Text + "')", "OK! the info added successfully");
            AutoNumber();
        }
    }
}
