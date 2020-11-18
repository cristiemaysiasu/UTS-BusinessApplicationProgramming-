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
    public partial class FormBilling : Form
    {
        private Form parentForm;

        private int orderId;

        private bool isAdmin;

        private int tableId;

        private bool isViewOnly;

        public FormBilling(Form frm, int orderId, int tableId, bool isAdmin = false, bool isViewOnly = false)
        {
            InitializeComponent();
            this.parentForm = frm;
            this.orderId = orderId;
            this.tableId = tableId;
            this.isAdmin = isAdmin;
            this.isViewOnly = isViewOnly;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.parentForm.Enabled = true;
            this.Close();
        }

        private void FormBilling_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
            if (isAdmin)
            {
                button1.Text = "Checkout";
                if (isViewOnly)
                {
                    button1.Visible = false;
                    button2.Text = "Back";
                }
            }
            DisplayLabel();
        }

        private void DisplayLabel()
        {
            int total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToInt32(row.Cells["Subtotal"].Value.ToString());
            }
            label1.Text = "Order ID: " + this.orderId.ToString();
            label2.Text = "Tanggal: " + DateTime.Now.ToString();
            label5.Text = "Nomor Meja: " + this.tableId.ToString();
            label4.Text = total.ToString();
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
                            "SELECT menu.name as menu_name, qty, item.price, item.subtotal " +
                            "FROM Table_OrderItem as item INNER JOIN Table_Menu as menu ON item.menu_id = menu.id " +
                            "WHERE parent_id = @OrderId ORDER BY item.created_at";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@OrderId", this.orderId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                            //row.Cells["Subtotal"].Value = Convert.ToInt32(row.Cells["Price"].Value.ToString()) * Convert.ToInt32(row.Cells["Qty"].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void CloseForm()
        {
            this.parentForm.Enabled = true;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                FormPayment frm = new FormPayment(this, orderId);
                frm.Show();
                this.Enabled = false;
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
                            cmd.CommandText = "UPDATE Table_Order SET request_checkout = 1 WHERE id = @OrderId";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@OrderId", this.orderId);

                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("You have requested to checkout. Thank you!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        ClearDataGrid();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void ClearDataGrid()
        {
            parentForm.Enabled = true;
            ((FormMenuInterfacee)parentForm).ClearOrder();
            ((FormMenuInterfacee)parentForm).LoadDataGrid();
            this.Close();
        }

        private void FormBilling_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.parentForm.Enabled = true;
        }
    }
}
