using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTS
{
    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tableId = textBox1.Text.Trim();

            if (!String.IsNullOrEmpty(tableId))
            {
                FormMenuInterfacee frm = new FormMenuInterfacee(this, tableId);
                frm.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdminLogin frm = new FormAdminLogin();
            frm.Show();
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            LoadImages();
        }

        private Image RetrieveImage(byte[] value)
        {
            MemoryStream stream = new MemoryStream(value);
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        private void LoadImages()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT TOP 3 image " +
                            "FROM Table_Menu as menu INNER JOIN (" +
                            "   SELECT menu.id, SUM(qty) as sum_qty " +
                            "   FROM Table_Menu as menu INNER JOIN Table_OrderItem as item " +
                            "   ON menu.id = item.menu_id GROUP BY menu.id" +
                            ") as filtered " +
                            "ON menu.id = filtered.id " +
                            "WHERE deleted = 0 " +
                            "ORDER BY filtered.sum_qty DESC;";

                        Image[] imgs = new Image[3];
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                var i = 0;
                                while (reader.Read())
                                {
                                    if (reader["image"] != DBNull.Value)
                                    {
                                        imgs[i] = RetrieveImage((byte[])reader["image"]);
                                    }
                                    else
                                    {
                                        imgs[i] = null;
                                    }
                                    i++;
                                }
                            }
                        }

                        pictureBox1.Image = imgs[0] == null ? null : imgs[0];
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                        pictureBox2.Image = imgs[1] == null ? null : imgs[1];
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                        pictureBox3.Image = imgs[2] == null ? null : imgs[2];
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                int table_id = Convert.ToInt32(textBox1.Text.Trim());

                if (table_id > 15)
                {
                    this.textBox1.Text = 15.ToString();
                }
            }
        }
    }
}
