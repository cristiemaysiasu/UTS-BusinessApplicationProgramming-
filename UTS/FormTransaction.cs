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
using UTS.Lib;

namespace UTS
{
    public partial class FormTransaction : Form
    {

        public FormTransaction()
        {
            InitializeComponent();
        }

        private void LoadGridData()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT id, table_id, created_at, total_item, total_qty, total_purchase, request_checkout " +
                            "FROM Table_Order " +
                            "WHERE checkout = 0 ORDER BY request_checkout DESC, created_at";
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                            if ((bool)row.Cells["RequestCheckout"].Value == true)
                            {
                                ((DataGridViewDisableButtonCell)row.Cells["Checkout"]).Enabled = true;
                            }
                            else
                            {
                                ((DataGridViewDisableButtonCell)row.Cells["Checkout"]).Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FormTransaction_Load(object sender, EventArgs e)
        {
            AddButtonToOrderGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void AddButtonToOrderGrid()
        {
            DataGridViewDisableButtonColumn btnDeleteCol = new DataGridViewDisableButtonColumn();
            btnDeleteCol.Name = "Checkout";
            btnDeleteCol.Text = "Checkout";
            btnDeleteCol.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(btnDeleteCol);

            DataGridViewButtonColumn btnDetailCol = new DataGridViewButtonColumn();
            btnDetailCol.Name = "Details";
            btnDetailCol.Text = "Details";
            btnDetailCol.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(btnDetailCol);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Checkout"))
                {
                    FormBilling frm = new FormBilling(this, Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()), Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["TableId"].Value.ToString()), true);
                    frm.Show();
                    this.Enabled = false;
                }
                if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Details"))
                {
                    FormBilling frm = new FormBilling(this, Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value.ToString()), Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["TableId"].Value.ToString()), true, true);
                    frm.Show();
                    this.Enabled = false;
                }
            }
            LoadGridData();
        }
    }
}
