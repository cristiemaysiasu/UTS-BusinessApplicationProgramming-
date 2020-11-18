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
    public partial class FormMenu : Form
    {
        private object dgvData;

        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            LoadDataGridMenu();
        }

        public void LoadDataGridMenu()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT menu.id, menu.name, menu.image, menu.capital, menu.price, unit.label as unit_label, category.label as category_label, menu.description, CAST(CASE WHEN menu.stock= 1 THEN 'AVAILABLE' ELSE 'UNAVAILABLE' END AS text) AS stock " +
                            "FROM Table_Menu as menu, Table_MenuUnit as unit, Table_MenuCategory as category " +
                            "WHERE menu.unit_id = unit.id AND menu.category_id = category.id AND menu.deleted = 0 ORDER BY menu.created_at";
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col is DataGridViewImageColumn)
                            {
                                ((DataGridViewImageColumn)col).ImageLayout = DataGridViewImageCellLayout.Stretch;
                            }
                        }


                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            FormTambahMenu frm = new FormTambahMenu(this);
            frm.Show();
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            FormCategory frm = new FormCategory();
            frm.Show();
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            FormUnit frm = new FormUnit();
            frm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            DataGridViewRow row = senderGrid.Rows[e.RowIndex];
            int id = Int32.Parse(row.Cells["Id"].Value.ToString());

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Delete"))
                {
                    DialogResult result = MessageBox.Show("Are you sure want to delete this Menu ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (var conn = new Connection().CreateAndOpenConnection())
                            {
                                using (var cmd = new SqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandText = "UPDATE Table_Menu SET deleted = 1 WHERE id = @Id";
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

                        MessageBox.Show("Menu deleted Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LoadDataGridMenu();
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Edit"))
                {
                    DialogResult result = MessageBox.Show("Are you sure want to Edit this Menu ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        FormTambahMenu frm = new FormTambahMenu(this, id);
                        frm.Show();
                    }
                }
            }
        }
       

        
    }
}
            
        
    


    
