namespace UTS
{
    partial class FormUnit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableMenuUnitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dB_DATADataSetMenuUnit = new UTS.DB_DATADataSetMenuUnit();
            this.table_MenuUnitTableAdapter = new UTS.DB_DATADataSetMenuUnitTableAdapters.Table_MenuUnitTableAdapter();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtBoxUnit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deleted = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableMenuUnitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB_DATADataSetMenuUnit)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Name,
            this.Deleted});
            this.dataGridView1.DataSource = this.tableMenuUnitBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(230, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(332, 237);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tableMenuUnitBindingSource
            // 
            this.tableMenuUnitBindingSource.DataMember = "Table_MenuUnit";
            this.tableMenuUnitBindingSource.DataSource = this.dB_DATADataSetMenuUnit;
            // 
            // dB_DATADataSetMenuUnit
            // 
            this.dB_DATADataSetMenuUnit.DataSetName = "DB_DATADataSetMenuUnit";
            this.dB_DATADataSetMenuUnit.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // table_MenuUnitTableAdapter
            // 
            this.table_MenuUnitTableAdapter.ClearBeforeFill = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.OrangeRed;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(124, 150);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 26);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtBoxUnit
            // 
            this.txtBoxUnit.Location = new System.Drawing.Point(25, 114);
            this.txtBoxUnit.Name = "txtBoxUnit";
            this.txtBoxUnit.Size = new System.Drawing.Size(176, 20);
            this.txtBoxUnit.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Add Unit";
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.DataPropertyName = "deleted";
            this.dataGridViewButtonColumn1.HeaderText = "Delete";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Text = "delete";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // idColumn
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.DataPropertyName = "label";
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // Deleted
            // 
            this.Deleted.DataPropertyName = "deleted";
            this.Deleted.HeaderText = "Deleted";
            this.Deleted.Name = "Deleted";
            this.Deleted.ReadOnly = true;
            this.Deleted.Text = "Delete";
            this.Deleted.UseColumnTextForButtonValue = true;
            // 
            // FormUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(610, 345);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxUnit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.Name = "FormUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormUnit";
            this.Load += new System.EventHandler(this.FormUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableMenuUnitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB_DATADataSetMenuUnit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private DB_DATADataSetMenuUnit dB_DATADataSetMenuUnit;
        private System.Windows.Forms.BindingSource tableMenuUnitBindingSource;
        private DB_DATADataSetMenuUnitTableAdapters.Table_MenuUnitTableAdapter table_MenuUnitTableAdapter;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtBoxUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewButtonColumn Deleted;
    }
}
