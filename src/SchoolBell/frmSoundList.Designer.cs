namespace SchoolBell
{
    partial class frmSoundList
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
            this.btImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btClose = new System.Windows.Forms.Button();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.colnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnPlay = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(3, 3);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(177, 35);
            this.btImport.TabIndex = 0;
            this.btImport.Text = "Import Sound/Music";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "*Double click following sound/music to select.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btClose);
            this.panel1.Controls.Add(this.btImport);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 66);
            this.panel1.TabIndex = 2;
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(186, 3);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(110, 35);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "Cancel";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            this.dgv1.AllowUserToResizeColumns = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colnName,
            this.colnPlay,
            this.colnDelete});
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(0, 66);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 30;
            this.dgv1.Size = new System.Drawing.Size(627, 674);
            this.dgv1.TabIndex = 3;
            this.dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellContentClick);
            this.dgv1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellDoubleClick);
            // 
            // colnName
            // 
            this.colnName.HeaderText = "Sound/Music List";
            this.colnName.Name = "colnName";
            this.colnName.ReadOnly = true;
            this.colnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colnName.Width = 400;
            // 
            // colnPlay
            // 
            this.colnPlay.HeaderText = "Play";
            this.colnPlay.Name = "colnPlay";
            this.colnPlay.ReadOnly = true;
            this.colnPlay.Text = "Play";
            this.colnPlay.UseColumnTextForButtonValue = true;
            // 
            // colnDelete
            // 
            this.colnDelete.HeaderText = "Delete";
            this.colnDelete.Name = "colnDelete";
            this.colnDelete.ReadOnly = true;
            this.colnDelete.Text = "Delete";
            this.colnDelete.UseColumnTextForButtonValue = true;
            // 
            // frmSoundList
            // 
            this.ClientSize = new System.Drawing.Size(627, 740);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSoundList";
            this.Text = "Sound List";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSoundList_FormClosing);
            this.Load += new System.EventHandler(this.frmSoundList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnName;
        private System.Windows.Forms.DataGridViewButtonColumn colnPlay;
        private System.Windows.Forms.DataGridViewButtonColumn colnDelete;
    }
}
