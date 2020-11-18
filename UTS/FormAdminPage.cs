using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTS
{
    public partial class FormAdminPage : Form
    {
        private readonly FormAdminLogin parentForm;

        private readonly string username;

        public FormAdminPage(FormAdminLogin frm, string username)
        {
            InitializeComponent();
            this.username = username;
            this.parentForm = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            FormMenu frm = new FormMenu();
            frm.TopLevel = false;
            panel1.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void FormAdminPage_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome, " + this.username;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.parentForm.Show();
            this.parentForm.Reset();
        }

        private void FormAdminPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parentForm.Show();
            this.parentForm.Reset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            FormTransaction frm = new FormTransaction();
            frm.TopLevel = false;
            panel1.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            FormSalesReport frm = new FormSalesReport();
            frm.TopLevel = false;
            panel1.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
    }
}
