namespace SchoolBell
{
    partial class frmMain2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.miSave = new System.Windows.Forms.ToolStripMenuItem();
            this.miSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDays = new System.Windows.Forms.Panel();
            this.btMonday = new System.Windows.Forms.Button();
            this.btTuesday = new System.Windows.Forms.Button();
            this.btWednesday = new System.Windows.Forms.Button();
            this.btThursday = new System.Windows.Forms.Button();
            this.btFriday = new System.Windows.Forms.Button();
            this.btSaturday = new System.Windows.Forms.Button();
            this.btSunday = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lbNextBell = new System.Windows.Forms.Label();
            this.lbCurrentDay = new System.Windows.Forms.Label();
            this.lbSchedule = new System.Windows.Forms.Label();
            this.cbScheduleType = new System.Windows.Forms.ComboBox();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btAdd = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlay = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colSoundFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSelect = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbFile = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.pnlDays.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoad,
            this.miSave,
            this.miSep1,
            this.miExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(37, 20);
            this.miFile.Text = "&File";
            // 
            // miLoad
            // 
            this.miLoad.Name = "miLoad";
            this.miLoad.Size = new System.Drawing.Size(154, 22);
            this.miLoad.Text = "Load Settings...";
            this.miLoad.Click += new System.EventHandler(this.miLoad_Click);
            // 
            // miSave
            // 
            this.miSave.Name = "miSave";
            this.miSave.Size = new System.Drawing.Size(154, 22);
            this.miSave.Text = "Save Settings...";
            this.miSave.Click += new System.EventHandler(this.miSave_Click);
            // 
            // miSep1
            // 
            this.miSep1.Name = "miSep1";
            this.miSep1.Size = new System.Drawing.Size(151, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(154, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miHelp
            // 
            this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
            this.miHelp.Name = "miHelp";
            this.miHelp.Size = new System.Drawing.Size(44, 20);
            this.miHelp.Text = "&Help";
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(116, 22);
            this.miAbout.Text = "About...";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // pnlDays
            // 
            this.pnlDays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlDays.Controls.Add(this.btMonday);
            this.pnlDays.Controls.Add(this.btTuesday);
            this.pnlDays.Controls.Add(this.btWednesday);
            this.pnlDays.Controls.Add(this.btThursday);
            this.pnlDays.Controls.Add(this.btFriday);
            this.pnlDays.Controls.Add(this.btSaturday);
            this.pnlDays.Controls.Add(this.btSunday);
            this.pnlDays.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDays.Location = new System.Drawing.Point(0, 24);
            this.pnlDays.Name = "pnlDays";
            this.pnlDays.Padding = new System.Windows.Forms.Padding(6);
            this.pnlDays.Size = new System.Drawing.Size(1000, 52);
            this.pnlDays.TabIndex = 1;
            // 
            // btMonday
            // 
            this.btMonday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMonday.Location = new System.Drawing.Point(9, 9);
            this.btMonday.Name = "btMonday";
            this.btMonday.Size = new System.Drawing.Size(130, 36);
            this.btMonday.TabIndex = 0;
            this.btMonday.Tag = "1";
            this.btMonday.Text = "Monday";
            this.btMonday.UseVisualStyleBackColor = false;
            this.btMonday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btTuesday
            // 
            this.btTuesday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btTuesday.Location = new System.Drawing.Point(145, 9);
            this.btTuesday.Name = "btTuesday";
            this.btTuesday.Size = new System.Drawing.Size(130, 36);
            this.btTuesday.TabIndex = 1;
            this.btTuesday.Tag = "2";
            this.btTuesday.Text = "Tuesday";
            this.btTuesday.UseVisualStyleBackColor = false;
            this.btTuesday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btWednesday
            // 
            this.btWednesday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btWednesday.Location = new System.Drawing.Point(281, 9);
            this.btWednesday.Name = "btWednesday";
            this.btWednesday.Size = new System.Drawing.Size(130, 36);
            this.btWednesday.TabIndex = 2;
            this.btWednesday.Tag = "3";
            this.btWednesday.Text = "Wednesday";
            this.btWednesday.UseVisualStyleBackColor = false;
            this.btWednesday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btThursday
            // 
            this.btThursday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThursday.Location = new System.Drawing.Point(417, 9);
            this.btThursday.Name = "btThursday";
            this.btThursday.Size = new System.Drawing.Size(130, 36);
            this.btThursday.TabIndex = 3;
            this.btThursday.Tag = "4";
            this.btThursday.Text = "Thursday";
            this.btThursday.UseVisualStyleBackColor = false;
            this.btThursday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btFriday
            // 
            this.btFriday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFriday.Location = new System.Drawing.Point(553, 9);
            this.btFriday.Name = "btFriday";
            this.btFriday.Size = new System.Drawing.Size(130, 36);
            this.btFriday.TabIndex = 4;
            this.btFriday.Tag = "5";
            this.btFriday.Text = "Friday";
            this.btFriday.UseVisualStyleBackColor = false;
            this.btFriday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btSaturday
            // 
            this.btSaturday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSaturday.Location = new System.Drawing.Point(689, 9);
            this.btSaturday.Name = "btSaturday";
            this.btSaturday.Size = new System.Drawing.Size(130, 36);
            this.btSaturday.TabIndex = 5;
            this.btSaturday.Tag = "6";
            this.btSaturday.Text = "Saturday";
            this.btSaturday.UseVisualStyleBackColor = false;
            this.btSaturday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // btSunday
            // 
            this.btSunday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSunday.Location = new System.Drawing.Point(825, 9);
            this.btSunday.Name = "btSunday";
            this.btSunday.Size = new System.Drawing.Size(130, 36);
            this.btSunday.TabIndex = 6;
            this.btSunday.Tag = "7";
            this.btSunday.Text = "Sunday";
            this.btSunday.UseVisualStyleBackColor = false;
            this.btSunday.Click += new System.EventHandler(this.btDay_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lbNextBell);
            this.pnlHeader.Controls.Add(this.lbCurrentDay);
            this.pnlHeader.Controls.Add(this.lbSchedule);
            this.pnlHeader.Controls.Add(this.cbScheduleType);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 76);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 44);
            this.pnlHeader.TabIndex = 2;
            // 
            // lbNextBell
            // 
            this.lbNextBell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNextBell.AutoSize = true;
            this.lbNextBell.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNextBell.Location = new System.Drawing.Point(640, 13);
            this.lbNextBell.Name = "lbNextBell";
            this.lbNextBell.Size = new System.Drawing.Size(77, 17);
            this.lbNextBell.TabIndex = 3;
            this.lbNextBell.Text = "Next bell: -";
            // 
            // lbCurrentDay
            // 
            this.lbCurrentDay.AutoSize = true;
            this.lbCurrentDay.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentDay.Location = new System.Drawing.Point(13, 13);
            this.lbCurrentDay.Name = "lbCurrentDay";
            this.lbCurrentDay.Size = new System.Drawing.Size(95, 17);
            this.lbCurrentDay.TabIndex = 0;
            this.lbCurrentDay.Text = "Current Day: -";
            // 
            // lbSchedule
            // 
            this.lbSchedule.AutoSize = true;
            this.lbSchedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSchedule.Location = new System.Drawing.Point(300, 14);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.Size = new System.Drawing.Size(58, 15);
            this.lbSchedule.TabIndex = 1;
            this.lbSchedule.Text = "Schedule:";
            // 
            // cbScheduleType
            // 
            this.cbScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScheduleType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbScheduleType.FormattingEnabled = true;
            this.cbScheduleType.Location = new System.Drawing.Point(370, 10);
            this.cbScheduleType.Name = "cbScheduleType";
            this.cbScheduleType.Size = new System.Drawing.Size(240, 23);
            this.cbScheduleType.TabIndex = 2;
            this.cbScheduleType.SelectedIndexChanged += new System.EventHandler(this.cbScheduleType_SelectedIndexChanged);
            // 
            // pnlAdd
            // 
            this.pnlAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlAdd.Controls.Add(this.btAdd);
            this.pnlAdd.Controls.Add(this.btSave);
            this.pnlAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdd.Location = new System.Drawing.Point(0, 120);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.pnlAdd.Size = new System.Drawing.Size(1000, 44);
            this.pnlAdd.TabIndex = 3;
            // 
            // btAdd
            // 
            this.btAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAdd.Location = new System.Drawing.Point(13, 5);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(150, 32);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "+ Add Schedule";
            this.btAdd.UseVisualStyleBackColor = false;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btSave
            // 
            this.btSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSave.ForeColor = System.Drawing.Color.White;
            this.btSave.Location = new System.Drawing.Point(175, 5);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(150, 32);
            this.btSave.TabIndex = 1;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = false;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colPlay,
            this.colSoundFile,
            this.colSelect,
            this.colRemove,
            this.colDelete});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.dgv.Location = new System.Drawing.Point(0, 164);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 32;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(1000, 414);
            this.dgv.TabIndex = 4;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellEndEdit);
            this.dgv.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
            // 
            // colTime
            // 
            this.colTime.HeaderText = "Time (HH:mm)";
            this.colTime.Name = "colTime";
            this.colTime.Width = 130;
            // 
            // colPlay
            // 
            this.colPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colPlay.HeaderText = "Play Sound";
            this.colPlay.Name = "colPlay";
            this.colPlay.Text = "Play";
            this.colPlay.UseColumnTextForButtonValue = true;
            // 
            // colSoundFile
            // 
            this.colSoundFile.HeaderText = "Music File";
            this.colSoundFile.Name = "colSoundFile";
            this.colSoundFile.ReadOnly = true;
            this.colSoundFile.Width = 320;
            // 
            // colSelect
            // 
            this.colSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colSelect.HeaderText = "Select Music File";
            this.colSelect.Name = "colSelect";
            this.colSelect.Text = "Select File";
            this.colSelect.UseColumnTextForButtonValue = true;
            this.colSelect.Width = 140;
            // 
            // colRemove
            // 
            this.colRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colRemove.HeaderText = "Remove Music";
            this.colRemove.Name = "colRemove";
            this.colRemove.Text = "× Remove";
            this.colRemove.UseColumnTextForButtonValue = true;
            this.colRemove.Width = 120;
            // 
            // colDelete
            // 
            this.colDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colDelete.HeaderText = "Delete Schedule";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "× Delete";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 140;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus,
            this.lbFile});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(39, 17);
            this.lbStatus.Text = "Ready";
            // 
            // lbFile
            // 
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(946, 17);
            this.lbFile.Spring = true;
            this.lbFile.Text = "Current Loaded Settings File: (new)";
            this.lbFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMain2
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.pnlAdd);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlDays);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmMain2";
            this.Text = "School Bell System - Weekly Scheduler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlDays.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miLoad;
        private System.Windows.Forms.ToolStripMenuItem miSave;
        private System.Windows.Forms.ToolStripSeparator miSep1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miHelp;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.Panel pnlDays;
        private System.Windows.Forms.Button btMonday;
        private System.Windows.Forms.Button btTuesday;
        private System.Windows.Forms.Button btWednesday;
        private System.Windows.Forms.Button btThursday;
        private System.Windows.Forms.Button btFriday;
        private System.Windows.Forms.Button btSaturday;
        private System.Windows.Forms.Button btSunday;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lbCurrentDay;
        private System.Windows.Forms.Label lbSchedule;
        private System.Windows.Forms.ComboBox cbScheduleType;
        private System.Windows.Forms.Label lbNextBell;
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewButtonColumn colPlay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoundFile;
        private System.Windows.Forms.DataGridViewButtonColumn colSelect;
        private System.Windows.Forms.DataGridViewButtonColumn colRemove;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbFile;
    }
}
