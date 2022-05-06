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
    public partial class Frm_Deservds : Form
    {
        public Frm_Deservds()
        {
            InitializeComponent();
        }
        DbConnection db = new DbConnection();
        DataTable table = new DataTable();
        int row;
        void AutoNumber()
        {
            table.Clear();
            table = db.ReadData("select max(Des_ID) from Deserved", "");
            if ((table.Rows[0][0].ToString()) == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(table.Rows[0][0]) + 1).ToString();
            }
            nudPrice.Value = 1;
            dtpDate.Text = DateTime.Now.ToShortDateString();
            txtNotes.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnDeleteAll.Enabled = false;
        }
        private void Getdata()
        {
            cbxType.DataSource = db.ReadData("select * from Deserved_Type", "");
            cbxType.DisplayMember = "Name";
            cbxType.ValueMember = "Des_ID";
        }

        private void Frm_Deservds_Load(object sender, EventArgs e)
        {
            AutoNumber();
            Getdata();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbxType.Items.Count<=0)
            {
                MessageBox.Show("Select Please items.");
                return;
            }
            string d = dtpDate.Value.ToString("dd/MM/yyyy");
            db.ExecuteData("insert into Deserved values(" + txtID.Text + "," + nudPrice.Text + ",N'" + d + "',N'" + txtNotes.Text + "'," + cbxType.SelectedValue + ")", "OK! the info added successfully");
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
            Getdata();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete All Deserveds?", "Delete Deserved", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Deserved", "Delete All Successfuly!");
                AutoNumber();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Supplier?", "Delete Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Deserved where Des_ID = " + txtID.Text + "", "Delete Successfuly!");
                AutoNumber();
            }
        }
        private void Shows()
        {
            table.Clear();
            table = db.ReadData("select * from Deserved", "");
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Please Make sure you add new Deserved.");
            }
            else
            {
                try
                {
                    txtID.Text = table.Rows[row][0].ToString();
                    nudPrice.Value = Convert.ToDecimal(table.Rows[row][1].ToString());
                    this.Text = table.Rows[row][2].ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    dtpDate.Value = dt;
                    txtNotes.Text = table.Rows[row][3].ToString();
                    cbxType.SelectedValue = Convert.ToDecimal(table.Rows[row][4].ToString());
                }
                catch (Exception M)
                {
                    MessageBox.Show(M.Message);
                }
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
            table = db.ReadData("select count(Des_ID) from Deserved", "");
            row = Convert.ToInt32(table.Rows[0][0]) - 1;
            Shows();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Des_ID) from Deserved", "");
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

        private void btnPrevouis_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Des_ID) from Deserved", "");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string d = dtpDate.Value.ToString("dd/MM/yyyy");
            db.ReadData("update Deserved set Price = " + nudPrice.Value + ", Date = '" + d+ "', Notes = N'" + txtNotes.Text + "', Type_ID = N'" + cbxType.SelectedValue + "' where Des_ID = '" + txtID.Text + "'", "OK! Upadated successfuly.");
            AutoNumber();
        }
    }
}
