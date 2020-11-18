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
using UTS.Enums;

namespace UTS
{
    public partial class FormPayment : Form
    {
        private PaymentMethod method;

        private FormBilling parentForm;

        private int orderId;

        public FormPayment(Form frm, int orderId)
        {
            InitializeComponent();
            this.parentForm = (FormBilling)frm;
            this.orderId = orderId;
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.method = PaymentMethod.CASH;
            this.ChargePayment();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.method = PaymentMethod.DEBIT;
            this.ChargePayment();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.method = PaymentMethod.OVO;
            this.ChargePayment();
        }

        private void ChargePayment()
        {
            try
            {
                using (var conn = new Connection().CreateAndOpenConnection())
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE Table_Order SET checkout = 1, purchase_type = @PaymentMethod WHERE id = @OrderId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PaymentMethod", this.method.ToString());
                        cmd.Parameters.AddWithValue("@OrderId", this.orderId);

                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            this.parentForm.Enabled = true;
            this.parentForm.CloseForm();
            this.Close();

        }
    }
}
