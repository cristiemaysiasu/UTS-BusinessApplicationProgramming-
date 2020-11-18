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
    public partial class FormSalesReport : Form
    {
        public FormSalesReport()
        {
            InitializeComponent();
        }

        private void FormSalesReport_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT id, table_id, modify_at, total_item, total_qty, total_purchase, purchase_type " +
                            "FROM Table_Order " +
                            "WHERE checkout = 1 ORDER BY created_at";
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }
    }
}
