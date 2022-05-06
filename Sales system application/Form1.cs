using Sales_system_application.buys;
using Sales_system_application.Customer_forms;
using Sales_system_application.Deserveds;
using Sales_system_application.Products;
using Sales_system_application.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_system_application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Customers frm = new Frm_Customers();
            frm.ShowDialog();
        }

        private void suppliersInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suppliers_form frm = new Suppliers_form();
            frm.ShowDialog();
        }

        private void deservedsTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DeservedsType frm = new Frm_DeservedsType();
            frm.ShowDialog();
        }

        private void mangerDeservedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Deservds frm = new Frm_Deservds();
            frm.ShowDialog();
        }

        private void deservedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ReportDeserved frm = new Frm_ReportDeserved();
            frm.ShowDialog();
        }

        private void showProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Products frm = new Frm_Products();
            frm.ShowDialog();
        }

        private void manageBuysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Buy frm = new Frm_Buy();
            frm.ShowDialog();
        }
    }
}
