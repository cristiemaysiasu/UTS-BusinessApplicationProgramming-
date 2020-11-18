using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;

namespace UTS
{
    public partial class FormTambahMenu : Form
    {
        private readonly int id;

        private readonly FormMenu parentForm;

        public FormTambahMenu(FormMenu frm, int id = 0)
        {
            InitializeComponent();
            this.parentForm = frm;
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            int capital = int.Parse(textBox6.Text);
            int price = int.Parse(textBox5.Text);
            int unitId = (int)cmbBoxUnit.SelectedValue;
            int categoryId = (int)comboBox1.SelectedValue;
            string description = textBox2.Text.Trim();
            int stock = (int)cmdBoxStock.SelectedValue;

            if (id == 0)
            {
                try
                {
                    using (var conn = new Connection().CreateAndOpenConnection())
                    {
                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "INSERT INTO Table_Menu(name, capital, price, unit_id, category_id, description, stock, image)" +
                                "VALUES (@Name, @Capital, @Price, @UnitId, @CategoryId, @Description, @Stock, @Image)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Capital", capital);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@UnitId", unitId);
                            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@Stock", stock);

                            MemoryStream stream;
                            if (pictureBox1.Image != null)
                            {
                                stream = new MemoryStream();
                                pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
                                byte[] imageArray = new byte[stream.Length];
                                stream.Position = 0;
                                stream.Read(imageArray, 0, imageArray.Length);
                                cmd.Parameters.Add("@Image", SqlDbType.Binary).Value = imageArray;
                            }
                            else
                            {
                                cmd.Parameters.Add("@Image", SqlDbType.Binary).Value = DBNull.Value;
                            }

                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Menu Addedd Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            this.parentForm.LoadDataGridMenu();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    using (var conn = new Connection().CreateAndOpenConnection())
                    {
                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE Table_Menu SET name = @Name, capital = @Capital, price = @Price, unit_id = @UnitId, category_id = @CategoryId, description = @Description, stock = @Stock, image = @Image " +
                                "WHERE id = @Id";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Capital", capital);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@UnitId", unitId);
                            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@Stock", stock);
                            cmd.Parameters.AddWithValue("@Id", id);

                            MemoryStream stream;
                            if (pictureBox1.Image != null)
                            {
                                stream = new MemoryStream();
                                pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
                                byte[] imageArray = new byte[stream.Length];
                                stream.Position = 0;
                                stream.Read(imageArray, 0, imageArray.Length);
                                cmd.Parameters.Add("@Image", SqlDbType.Binary).Value = imageArray;
                            }
                            else
                            {
                                cmd.Parameters.Add("@Image", SqlDbType.Binary).Value = DBNull.Value;
                            }

                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Menu Updated Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            this.parentForm.LoadDataGridMenu();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void refreshPage()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
            this.comboBox1.SelectedIndex = 0;
            this.cmbBoxUnit.SelectedIndex = 0;
            this.cmdBoxStock.SelectedIndex = 0;
            this.pictureBox1.InitialImage = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormTambahMenu_Load(object sender, EventArgs e)
        {
            BindCategoryData();
            BindUnitData();
            cmdBoxStock.DataSource = Enum.GetValues(typeof(Enums.Stock));
            if (id == 0)
            {
                refreshPage();
                this.label1.Text = "Add Menu";
            }
            else
            {
                this.label1.Text = "Edit Menu";
                LoadMenuData();
            }
        }

        private void LoadMenuData()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM Table_Menu WHERE id = @Id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", this.id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    this.textBox1.Text = reader["name"].ToString();
                                    this.textBox2.Text = reader["description"].ToString();
                                    this.textBox5.Text = reader["price"].ToString();
                                    this.textBox6.Text = reader["capital"].ToString();
                                    this.comboBox1.SelectedValue = Int32.Parse(reader["category_id"].ToString());
                                    this.cmbBoxUnit.SelectedValue = Int32.Parse(reader["unit_id"].ToString());
                                    this.cmdBoxStock.SelectedIndex = (bool)reader["stock"] ? 1 : 0;
                                    if (reader["image"] != DBNull.Value)
                                    {
                                        this.pictureBox1.Image = DisplayImage((byte[])reader["image"]);
                                        this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    }
                                }
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Image DisplayImage(byte[] blob)
        {
            MemoryStream stream = new MemoryStream();
            byte[] idata = blob;
            stream.Write(idata, 0, Convert.ToInt32(idata.Length));
            Bitmap bitmap = new Bitmap(stream, false);
            return bitmap;
        }

        private void BindUnitData()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        string query = "SELECT id, label FROM Table_MenuUnit WHERE deleted = 0 ORDER BY id";
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        cmbBoxUnit.DisplayMember = "label";
                        cmbBoxUnit.ValueMember = "id";
                        cmbBoxUnit.DataSource = dataset.Tables[0];
                        cmbBoxUnit.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BindCategoryData()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        string query = "SELECT id, label FROM Table_MenuCategory WHERE deleted = 0 ORDER BY id";
                        cmd.Connection = conn;
                        cmd.CommandText = query;
                        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        comboBox1.DisplayMember = "label";
                        comboBox1.ValueMember = "id";
                        comboBox1.DataSource = dataset.Tables[0];
                        comboBox1.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBox6_KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog uploader = new OpenFileDialog();
                uploader.Filter = "Image Files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png";
                if (uploader.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(uploader.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.refreshPage();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
