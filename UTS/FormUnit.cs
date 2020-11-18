using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTS
{
    public partial class FormUnit : Form
    {

        DataTable Unit = new DataTable();

        SqlDataAdapter UnitAdapter;

        public FormUnit()
        {
            InitializeComponent();
        }

        private void FormUnit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dB_DATADataSetMenuUnit.Table_MenuUnit' table. You can move, or remove it, as needed.
            this.table_MenuUnitTableAdapter.Fill(this.dB_DATADataSetMenuUnit.Table_MenuUnit);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string label = txtBoxUnit.Text.Trim();
            if (!String.IsNullOrEmpty(label))
            {
                string uniqueId = String.Concat(label.ToLower().Where(c => !Char.IsWhiteSpace(c)));
                try
                {
                    using (var conn = new Connection().CreateAndOpenConnection())
                    { 
                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT COUNT(id) FROM Table_MenuUnit WHERE CONVERT(VARCHAR,unique_id) = @UniqueId AND deleted = 0";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@UniqueId", uniqueId);

                            object result = cmd.ExecuteScalar();
                            int count = result == DBNull.Value ? 0 : (int)result;

                            if (count == 0)
                            {
                                cmd.CommandText = "INSERT INTO Table_MenuUnit(unique_id, label) VALUES(@UniqueId, @Label)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@UniqueId", uniqueId);
                                cmd.Parameters.AddWithValue("@Label", label);

                                cmd.ExecuteNonQuery();

                                refreshGridView();
                                this.txtBoxUnit.Text = "";
                                MessageBox.Show("Unit Added Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unit already Exist!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            else
            {
                MessageBox.Show("Unit cannot be empty.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void refreshGridView()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    string query = "SELECT id, label FROM Table_MenuUnit WHERE deleted = 0";
                    UnitAdapter = new SqlDataAdapter(query, conn);
                    SqlCommandBuilder builder = new SqlCommandBuilder(UnitAdapter);
                    Unit.Clear();
                    UnitAdapter.Fill(Unit);

                    if (Unit.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = Unit;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            DataGridViewRow row = senderGrid.Rows[e.RowIndex];
            int id = Int32.Parse(row.Cells["id"].Value.ToString());

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Deleted"))
                {
                    DialogResult result = MessageBox.Show("Are you sure want to delete this Unit ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (var conn = new Connection().CreateAndOpenConnection())
                            {
                                using (var cmd = new SqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandText = "UPDATE Table_MenuUnit SET deleted = 1 WHERE id = @Id";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@Id", id);
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        MessageBox.Show("Unit deleted Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.refreshGridView();
                    }
                }
            }
        }
        
                
        }
    }

