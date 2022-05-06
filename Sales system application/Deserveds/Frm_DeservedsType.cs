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
    public partial class Frm_DeservedsType : Form
    {
        public Frm_DeservedsType()
        {
            InitializeComponent();
        }

        DbConnection db = new DbConnection();
        DataTable table = new DataTable();
        int row;
        void AutoNumber()
        {
            table.Clear();
            table = db.ReadData("select max(Des_ID) from Deserved_Type", "");
            if ((table.Rows[0][0].ToString()) == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(table.Rows[0][0]) + 1).ToString();
            }
            txtName.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnDeleteAll.Enabled = false;
        }

        private void Frm_Deserveds_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete All Deserved Type?", "Delete Deserved Type", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Deserved_Type", "Deserved Type All Successfuly!");
                AutoNumber();
            }
        }
        private void Shows()
        {
            table.Clear();
            table = db.ReadData("select * from Deserved_Type", "");
            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Please Make sure you add new Deserved Type.");
            }
            else
            {
                txtID.Text = table.Rows[row][0].ToString();
                txtName.Text = table.Rows[row][1].ToString();
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
            table = db.ReadData("select count(Des_ID) from Deserved_Type", "");
            row = Convert.ToInt32(table.Rows[0][0]) - 1;
            Shows();
        }

        private void btnPrevouis_Click(object sender, EventArgs e)
        {
            table.Clear();
            table = db.ReadData("select count(Des_ID) from Deserved_Type", "");
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
            table = db.ReadData("select count(Des_ID) from Deserved_Type", "");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to delete the Deserved Type?", "Delete Deserved Type", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.ReadData("delete from Deserved_Type where Des_ID = " + txtID.Text + "", "Delete Successfuly!");
                AutoNumber();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.ReadData("update Deserved_Type set Name = N'" + txtName.Text + "' where Des_ID = '" + txtID.Text + "'", "OK! Upadated successfuly.");
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter valid Deserved_Type.");
                return;
            }
            db.ExecuteData("insert into Deserved_Type values(" + txtID.Text + ",N'" + txtName.Text + "')", "OK! the info added successfully");
            AutoNumber();
        }
    }
}
