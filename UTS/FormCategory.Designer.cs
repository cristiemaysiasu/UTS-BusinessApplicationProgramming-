namespace UTS
{
    partial class FormCategory
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtBoxCatergory = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableMenuCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dB_DATADataSetMenuCategory1 = new UTS.DB_DATADataSetMenuCategory1();
            this.table_MenuCategoryTableAdapter = new UTS.DB_DATADataSetMenuCategory1TableAdapters.Table_MenuCategoryTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deleted = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableMenuCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB_DATADataSetMenuCategory1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.OrangeRed;
            this.btnAdd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(122, 106);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBoxCatergory
            // 
            this.txtBoxCatergory.Location = new System.Drawing.Point(47, 80);
            this.txtBoxCatergory.Name = "txtBoxCatergory";
            this.txtBoxCatergory.Size = new System.Drawing.Size(166, 20);
            this.txtBoxCatergory.TabIndex = 1;
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
            this.label,
            this.Deleted});
            this.dataGridView1.DataSource = this.tableMenuCategoryBindingSource;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(281, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(344, 282);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tableMenuCategoryBindingSource
            // 
            this.tableMenuCategoryBindingSource.DataMember = "Table_MenuCategory";
            this.tableMenuCategoryBindingSource.DataSource = this.dB_DATADataSetMenuCategory1;
            // 
            // dB_DATADataSetMenuCategory1
            // 
            this.dB_DATADataSetMenuCategory1.DataSetName = "DB_DATADataSetMenuCategory1";
            this.dB_DATADataSetMenuCategory1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // table_MenuCategoryTableAdapter
            // 
            this.table_MenuCategoryTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Add Category";
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.DataPropertyName = "deleted";
            this.dataGridViewButtonColumn1.HeaderText = "Deleted";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // label
            // 
            this.label.DataPropertyName = "label";
            this.label.HeaderText = "Name";
            this.label.Name = "label";
            this.label.ReadOnly = true;
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
            // FormCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(662, 374);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtBoxCatergory);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormCategory";
            this.Load += new System.EventHandler(this.FormCategory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableMenuCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB_DATADataSetMenuCategory1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtBoxCatergory;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DB_DATADataSetMenuCategory1 dB_DATADataSetMenuCategory1;
        private System.Windows.Forms.BindingSource tableMenuCategoryBindingSource;
        private DB_DATADataSetMenuCategory1TableAdapters.Table_MenuCategoryTableAdapter table_MenuCategoryTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn label;
        private System.Windows.Forms.DataGridViewButtonColumn Deleted;
    }
}