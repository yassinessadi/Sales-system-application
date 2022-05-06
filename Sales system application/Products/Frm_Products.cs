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

namespace Sales_system_application.Products
{
    public partial class Frm_Products : Form
    {
        public Frm_Products()
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
            table = db.ReadData("select max(Pro_ID) from Products", "");
            if ((table.Rows[0][0].ToString()) == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(table.Rows[0][0]) + 1).ToString();
            }
            txtName.Clear();
            txtSearch.Clear();
            txtBarcode.Clear();
            nudLD.Value = 0;
            try
            {
                Getdata();
                cbxProSearch.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
            nudPriceBuy.Value = 1;
            nudPriceSale.Value = 1;
            nudQty.Value = 1;
            nudRA.Value = 0;

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
            table = db.ReadData("select * from Products", "");
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Please Make sure you add new Products.");
            }
            else
            {
                txtID.Text = table.Rows[row][0].ToString();
                txtName.Text = table.Rows[row][1].ToString();
                nudQty.Value = Convert.ToDecimal(table.Rows[row][2].ToString());
                nudPriceBuy.Value = Convert.ToDecimal(table.Rows[row][3].ToString());
                nudPriceSale.Value = Convert.ToDecimal(table.Rows[row][4].ToString());
                txtBarcode.Text = table.Rows[row][5].ToString();
                nudLD.Value = Convert.ToDecimal(table.Rows[row][6].ToString());
                nudRA.Value = Convert.ToDecimal(table.Rows[row][7].ToString());
            }
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnDeleteAll.Enabled = true;
        }

        private void Frm_Products_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Pro_ID) from Products", "");
            row = Convert.ToInt32(table.Rows[0][0]) - 1;
            Shows();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            row = 0;
            Shows();
        }

        private void btnPrevouis_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Pro_ID) from Products", "");
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
            table = db.ReadData("select count(Pro_ID) from Products", "");
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
        private void Getdata()
        {
            cbxProSearch.DataSource = db.ReadData("select * from Products", "");
            cbxProSearch.DisplayMember = "Pro_Name";
            cbxProSearch.ValueMember = "Pro_ID";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable = db.ReadData("select * from Products where Pro_Name like N'%" + txtSearch.Text + "%' ", "");
            if (dataTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Product!.");
                return;
            }
            try
            {
                txtID.Text = dataTable.Rows[0][0].ToString();
                txtName.Text = dataTable.Rows[0][1].ToString();
                nudQty.Value = Convert.ToDecimal(dataTable.Rows[0][2].ToString());
                nudPriceBuy.Value = Convert.ToDecimal(dataTable.Rows[0][3].ToString());
                nudPriceSale.Value = Convert.ToDecimal(dataTable.Rows[0][4].ToString());
                txtBarcode.Text = dataTable.Rows[0][5].ToString();
                nudLD.Value = Convert.ToDecimal(dataTable.Rows[0][6].ToString());
                nudRA.Value = Convert.ToDecimal(dataTable.Rows[0][7].ToString());
            }
            catch (Exception M)
            {
                MessageBox.Show(M.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter valid customer name.");
                return;
            }

            if (nudRA.Value>=nudPriceSale.Value)
            {
                MessageBox.Show("can't not be to max discount greater then sale price.");
                return;
            }
            db.ExecuteData("insert into Products values(" + txtID.Text + ",N'" + txtName.Text + "',"+nudQty.Value+","+nudPriceBuy.Value+","+nudPriceSale.Value+ ",N'" + txtBarcode.Text + "',"+nudLD.Value+","+nudRA.Value+")", "OK! the info added successfully");
            AutoNumber();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete All Products?", "Delete Products", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Products", "Delete All Successfuly!");
                AutoNumber();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Products?", "Delete Products", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Products where Pro_ID = " + txtID.Text + "", "Delete Successfuly!");
                AutoNumber();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter valid customer name.");
                return;
            }

            if (nudRA.Value >= nudPriceSale.Value)
            {
                MessageBox.Show("can't not be to max discount greater then sale price.");
                return;
            }
            db.ExecuteData("update Products set Pro_Name = N'" + txtName.Text + "',Qty =" + nudQty.Value + ",Buy_Price = " + nudPriceBuy.Value + ",Sale_Price = " + nudPriceSale.Value + ",Brarcode = N'" + txtBarcode.Text + "',MinQty = " + nudLD.Value + ",MaxDiscount = " + nudRA.Value + " where Pro_ID = " + txtID.Text + " ", "OK! the info update successfully");
        }

        private void cbxProSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxProSearch.Items.Count >= 1)
            {
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                dataTable = db.ReadData("select * from Products where Pro_ID = " + cbxProSearch.SelectedValue + " ", "");
                try
                {
                    txtID.Text = dataTable.Rows[0][0].ToString();
                    txtName.Text = dataTable.Rows[0][1].ToString();
                    nudQty.Value = Convert.ToDecimal(dataTable.Rows[0][2].ToString());
                    nudPriceBuy.Value = Convert.ToDecimal(dataTable.Rows[0][3].ToString());
                    nudPriceSale.Value = Convert.ToDecimal(dataTable.Rows[0][4].ToString());
                    txtBarcode.Text = dataTable.Rows[0][5].ToString();
                    nudLD.Value = Convert.ToDecimal(dataTable.Rows[0][6].ToString());
                    nudRA.Value = Convert.ToDecimal(dataTable.Rows[0][7].ToString());
                }
                catch (Exception M)
                {
                    MessageBox.Show(M.Message);
                }
            }
        }
    }
}
