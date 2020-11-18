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
    public partial class FormAddOrder : Form
    {
        private Image image;

        private int price;

        private string name;

        private int menuId;

        private FormMenuInterfacee parentForm;

        public FormAddOrder(FormMenuInterfacee frm, int menuId, string name, int price, Image image)
        {
            InitializeComponent();
            this.menuId = menuId;
            this.name = name;
            this.price = price;
            this.image = image;
            this.parentForm = frm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.parentForm.Enabled = true;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(textBox1.Text);
            qty++;
            textBox1.Text = qty.ToString();
        }

        private void FormAddOrder_Load(object sender, EventArgs e)
        {
            textBox1.Text = 1.ToString();
            pictureBox1.Image = this.image;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            label3.Text = this.name;
            label4.Text = this.price.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(textBox1.Text);
            if (qty > 0)
            {
                qty--;
            }
            textBox1.Text = qty.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(textBox1.Text);
            this.parentForm.AddDataOrder(menuId, name, price, qty);
            this.parentForm.Enabled = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = 0.ToString();
            }
        }

        private void FormAddOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parentForm.Enabled = true;
        }
    }
}
