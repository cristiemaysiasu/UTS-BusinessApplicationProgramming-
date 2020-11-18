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
using UTS.Model;

namespace UTS
{
    public partial class FormAdminLogin : Form
    {
        public FormAdminLogin()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = this.txtBoxUsername.Text.Trim();
            string password = this.txtBoxPassword.Text.Trim();
            string passwordHash = Helper.SHA256ComputeHash(password);

            if (String.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username is empty", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBoxUsername.Focus();
            }
            else if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is empty", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBoxPassword.Focus();
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
                            cmd.CommandText = "SELECT username, password FROM Table_Admin WHERE username = @Username";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Username", username);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Admin admin = new Admin(reader["username"].ToString(), reader["password"].ToString());

                                        if (admin.getPassword().Equals(passwordHash))
                                        {
                                            //display new form admin page
                                            FormAdminPage frm = new FormAdminPage(this, admin.getUsername());
                                            MessageBox.Show("Login Success", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.Hide();
                                            frm.Show();
                                        } 
                                        else
                                        {
                                            MessageBox.Show("Wrong Password", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                else
                                {
                                    //conn.Close();
                                    //conn.Open();
                                    //cmd.Connection = conn;
                                    //cmd.CommandText = "INSERT INTO Table_Admin(username, password) VALUES(@Username, @Password)";
                                    //cmd.Parameters.Clear();
                                    //cmd.Parameters.AddWithValue("@Username", username);
                                    //cmd.Parameters.AddWithValue("@Password", passwordHash);
                                    //
                                    //cmd.ExecuteNonQuery();
                                    MessageBox.Show("Username not found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                            conn.Close();
                        }
                    }
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void Reset()
        {
            this.txtBoxUsername.ResetText();
            this.txtBoxPassword.ResetText();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
