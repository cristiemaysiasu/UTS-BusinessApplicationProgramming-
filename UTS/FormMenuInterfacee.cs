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
using UTS.Lib;

namespace UTS
{
    public partial class FormMenuInterfacee : Form
    {
        private readonly FormBase parentForm;

        private string tableId;

        private int orderId = 0;

        private bool checkout = true;

        public FormMenuInterfacee(FormBase frm, string tableId)
        {
            InitializeComponent();
            this.parentForm = frm;
            this.tableId = tableId;
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
                            "SELECT menu.name, menu.image, menu.capital, menu.price, unit.label as unit_label, category.label as category_label, menu.description, menu.stock " +
                            "FROM Table_Menu as menu, Table_MenuUnit as unit, Table_MenuCategory as category " +
                            "WHERE menu.unit_id = unit.id AND menu.category_id = category.id ORDER BY menu.created_at";
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

        internal void AddDataOrder(int id, string name, int price, int qty)
        {
            if (qty > 0)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView2);
                row.Cells[0].Value = name;
                row.Cells[1].Value = price;
                row.Cells[2].Value = qty;
                row.Cells[3].Value = price * qty;
                row.Cells[4].Value = id;
                row.Cells[5].Value = false;
                dataGridView2.Rows.Add(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormMenuInterfacee_Load(object sender, EventArgs e)
        {
            BindCategoryData();
            LoadDataGrid();
            this.label4.Text = String.Concat(this.label4.Text, this.tableId);
            AddButtonToOrderGrid();
        }

        private void AddButtonToOrderGrid()
        {
            DataGridViewDisableButtonColumn btnDeleteCol = new DataGridViewDisableButtonColumn();
            btnDeleteCol.Name = "Delete";
            btnDeleteCol.Text = "Delete";
            btnDeleteCol.UseColumnTextForButtonValue = true;
            this.dataGridView2.Columns.Add(btnDeleteCol);
        }

        public void ClearOrder()
        {
            this.dataGridView2.Rows.Clear();
            label5.Text = 0.ToString();
            this.checkout = true;
        }

        public void LoadDataGrid()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT id, name, price, description, image " +
                            "FROM Table_Menu " +
                            "WHERE deleted = 0 AND stock = 1 ORDER BY created_at";
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

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT id, name, price, description, image " +
                            "FROM Table_Menu " +
                            "WHERE deleted = 0 AND category_id = @CategoryId AND stock = 1 ORDER BY created_at";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CategoryId", comboBox1.SelectedValue);
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

        private void button5_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            string name = row.Cells[1].Value.ToString();
            int price = (int)row.Cells[2].Value;
            int id = (int)row.Cells["id"].Value;
            Image image = null;
            if (row.Cells[4].Value != DBNull.Value)
            {
                image = RetrieveImage((byte[])row.Cells[4].Value);
            }

            FormAddOrder frm = new FormAddOrder(this, id, name, price, image);
            frm.Show();
            this.Enabled = false;
        }

        private Image RetrieveImage(byte[] value)
        {
            MemoryStream stream = new MemoryStream(value);
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            return img;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.Columns[e.ColumnIndex].Index.ToString());
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && ((DataGridViewDisableButtonCell) senderGrid.Rows[e.RowIndex].Cells["Delete"]).Enabled)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name.Equals("Delete"))
                {
                    DialogResult result = MessageBox.Show("Are you sure want to delete this Menu ?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        dataGridView2.Rows.RemoveAt(e.RowIndex);

                        MessageBox.Show("Menu deleted Successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.checkout = false;
            int itemOrder = 0;
            int totalQty = 0;
            int totalPurchase = 0;
            int totalPrice = 0;
            foreach (DataGridViewRow row in this.dataGridView2.Rows)
            {
                if (!(bool)row.Cells["IsOrdered"].Value)
                {
                    ((DataGridViewDisableButtonCell)row.Cells["Delete"]).Enabled = false;
                    itemOrder++;
                    totalQty += (int)row.Cells["Quantity"].Value;
                    totalPurchase += (int)row.Cells["Subtotal"].Value;
                }
                totalPrice += (int)row.Cells["Subtotal"].Value;
            }
            dataGridView2.Refresh();
            
            if (itemOrder > 0)
            {
                try
                {
                    using (var conn = new Connection().CreateAndOpenConnection())
                    {
                        using (var com = new SqlCommand())
                        {
                            bool newEntry = false;
                            com.Connection = conn;
                            if (orderId == 0)
                            {
                                com.CommandText = "INSERT INTO Table_Order(table_id, total_item, total_qty, total_purchase) output INSERTED.ID VALUES(@TableId, @TotalItem, @TotalQty, @TotalPurchase)";
                                com.Parameters.Clear();
                                com.Parameters.AddWithValue("@TableId", this.tableId);
                                com.Parameters.AddWithValue("@TotalItem", itemOrder);
                                com.Parameters.AddWithValue("@TotalQty", totalQty);
                                com.Parameters.AddWithValue("@TotalPurchase", totalPurchase);

                                this.orderId = (int)com.ExecuteScalar();
                                newEntry = true;
                            }

                            int newItemOrdered = 0;
                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                int price = (int)row.Cells["OrderPrice"].Value;
                                int menuId = (int)row.Cells["MenuId"].Value;
                                int qty = (int)row.Cells["Quantity"].Value;
                                if (!(bool)row.Cells["IsOrdered"].Value)
                                {
                                    com.CommandText =
                                        "SELECT COUNT(*) " +
                                        "FROM Table_Order as order_entity INNER JOIN Table_OrderItem as item " +
                                        "ON order_entity.id = item.parent_id " +
                                        "WHERE item.menu_id = @MenuId AND order_entity.id = @Id";
                                    com.Parameters.Clear();
                                    com.Parameters.AddWithValue("@MenuId", menuId);
                                    com.Parameters.AddWithValue("@Id", this.orderId);

                                    object result = com.ExecuteScalar();
                                    int count = result == DBNull.Value ? 0 : (int)result;

                                    if (count > 0)
                                    {
                                        //update
                                        com.CommandText = "UPDATE Table_OrderItem SET qty = qty + @Qty, subtotal = subtotal + @Total WHERE parent_id = @ParentId AND menu_id = @MenuId";
                                        com.Parameters.Clear();
                                        com.Parameters.AddWithValue("@Qty", qty);
                                        com.Parameters.AddWithValue("@Total", qty * price);
                                        com.Parameters.AddWithValue("@ParentId", orderId);
                                        com.Parameters.AddWithValue("@MenuId", menuId);

                                        com.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        //insert
                                        newItemOrdered++;
                                        com.CommandText = "INSERT INTO Table_OrderItem(menu_id, qty, price, subtotal, parent_id) VALUES(@MenuId, @Qty, @Price, @Total, @ParentId)";
                                        com.Parameters.Clear();
                                        com.Parameters.AddWithValue("@MenuId", menuId);
                                        com.Parameters.AddWithValue("@Qty", qty);
                                        com.Parameters.AddWithValue("@Price", price);
                                        com.Parameters.AddWithValue("@Total", qty * price);
                                        com.Parameters.AddWithValue("@ParentId", orderId);

                                        com.ExecuteNonQuery();
                                    }

                                    row.Cells["IsOrdered"].Value = true;
                                }
                            }

                            if (!newEntry)
                            {
                                com.CommandText = "UPDATE Table_Order SET total_item = total_item + @TotalItem, total_purchase = total_purchase + @TotalPurchase, total_qty = total_qty + @TotalQty WHERE id = @OrderId";
                                com.Parameters.AddWithValue("@TotalItem", newItemOrdered);
                                com.Parameters.AddWithValue("@TotalQty", totalQty);
                                com.Parameters.AddWithValue("@TotalPurchase", totalPurchase);
                                com.Parameters.AddWithValue("@OrderId", orderId);

                                com.ExecuteNonQuery();
                            }
                        }
                        conn.Close();
                    }
                }
               catch (Exception)
                {
                    throw;
                }
                MessageBox.Show("Order Success.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Order is Empty! Please Order Something.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            label5.Text = totalPrice.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "SELECT TOP 3 menu.id, name, price, description, image " +
                            "FROM Table_Menu as menu INNER JOIN (" +
                            "   SELECT menu.id, SUM(qty) as sum_qty " +
                            "   FROM Table_Menu as menu INNER JOIN Table_OrderItem as item " +
                            "   ON menu.id = item.menu_id GROUP BY menu.id" +
                            ") as filtered " +
                            "ON menu.id = filtered.id " +
                            "WHERE deleted = 0 AND stock = 1 " +
                            "ORDER BY filtered.sum_qty DESC;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CategoryId", comboBox1.SelectedValue);
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.orderId != 0)
            {
                FormBilling frm = new FormBilling(this, orderId, Convert.ToInt32(tableId));
                frm.Show();
                this.Enabled = false;
            }
            else
            {
                MessageBox.Show("No Order Found", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormMenuInterfacee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!checkout)
            {
                MessageBox.Show("Please Checkout before Closing!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }
}
