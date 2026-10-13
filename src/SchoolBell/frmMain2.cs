using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace SchoolBell
{
    public partial class frmMain2 : SchoolBell.baseForm
    {
        class Slot
        {
            public int hour;
            public int minute;
            public string soundfile = "";
        }

        class DaySettings
        {
            public string type = "custom"; // "custom" or "1".."7"
            public List<Slot> slots = new List<Slot>();
        }

        Dictionary<int, DaySettings> days = new Dictionary<int, DaySettings>();
        int currentDay = 1;

        readonly string[] dayNames = { "", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        string folderMusic;
        string settingsFile;
        string lastFilePointer;
        string currentSettingsPath = "";

        WindowsMediaPlayer player = new WindowsMediaPlayer();
        Timer bellTimer = new Timer();
        Timer clockTimer = new Timer();
        int targetSlotIndex = -1;
        int targetDay = -1;

        Button[] dayButtons;
        bool suppressGridEvents = false;
        bool dirty = false;

        public frmMain2()
        {
            InitializeComponent();

            for (int i = 1; i <= 7; i++) days[i] = new DaySettings();

            dayButtons = new Button[] { null, btMonday, btTuesday, btWednesday, btThursday, btFriday, btSaturday, btSunday };

            folderMusic = Path.Combine(Application.StartupPath, "sound");
            settingsFile = Path.Combine(Application.StartupPath, "settings.txt");
            lastFilePointer = Path.Combine(Application.StartupPath, "lastfile.txt");
            if (!Directory.Exists(folderMusic))
            {
                try { Directory.CreateDirectory(folderMusic); }
                catch (Exception ex) { MessageBox.Show("Cannot create music folder.\r\n" + ex.Message); }
            }

            try { this.Icon = new Icon(Path.Combine(Application.StartupPath, "ico_32x32.ico")); } catch { }

            this.Text = "School Bell System - Weekly Scheduler - V" + Program.version;

            cbScheduleType.Items.Clear();
            cbScheduleType.Items.Add("Custom Schedule");
            for (int i = 1; i <= 7; i++)
                cbScheduleType.Items.Add("Use " + dayNames[i] + " Schedule");

            string startupFile = null;
            try
            {
                if (File.Exists(lastFilePointer))
                {
                    string remembered = File.ReadAllText(lastFilePointer).Trim();
                    if (remembered.Length > 0 && File.Exists(remembered))
                        startupFile = remembered;
                }
            }
            catch { }
            if (startupFile == null && File.Exists(settingsFile))
                startupFile = settingsFile;

            if (startupFile != null)
            {
                LoadFromFile(startupFile, silent: true);
                currentSettingsPath = startupFile;
            }
            UpdateFileStatus();

            InitializeModernUI();

            int todayIdx = DayOfWeekToIndex(DateTime.Now.DayOfWeek);
            SelectDay(todayIdx);

            bellTimer.Tick += BellTimer_Tick;
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            ScheduleNextBell();
        }

        void InitializeModernUI()
        {
            this.BackColor = Color.White;

            // Configure modern DataGridView
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(240, 240, 240);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgv.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 252);
            dgv.RowTemplate.Height = 34;

            // Day buttons flat styling
            for (int i = 1; i <= 7; i++)
            {
                if (dayButtons[i] == null) continue;
                dayButtons[i].FlatStyle = FlatStyle.Flat;
                dayButtons[i].FlatAppearance.BorderSize = 1;
                dayButtons[i].Cursor = Cursors.Hand;
            }

            // Action buttons flat styling
            btAdd.FlatStyle = FlatStyle.Flat;
            btAdd.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btAdd.Cursor = Cursors.Hand;

            btSave.FlatStyle = FlatStyle.Flat;
            btSave.FlatAppearance.BorderColor = Color.FromArgb(29, 78, 216);
            btSave.Cursor = Cursors.Hand;
        }

        int DayOfWeekToIndex(DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                default: return 7;
            }
        }

        void SelectDay(int dayIdx)
        {
            currentDay = dayIdx;
            for (int i = 1; i <= 7; i++)
            {
                if (dayButtons[i] == null) continue;
                if (i == dayIdx)
                {
                    dayButtons[i].BackColor = Color.FromArgb(37, 99, 235);
                    dayButtons[i].ForeColor = Color.White;
                    dayButtons[i].FlatAppearance.BorderColor = Color.FromArgb(29, 78, 216);
                    dayButtons[i].Font = new Font(this.Font, FontStyle.Bold);
                }
                else
                {
                    dayButtons[i].BackColor = Color.White;
                    dayButtons[i].ForeColor = Color.FromArgb(55, 65, 81);
                    dayButtons[i].FlatAppearance.BorderColor = Color.FromArgb(229, 231, 235);
                    dayButtons[i].Font = new Font(this.Font, FontStyle.Regular);
                }
            }

            lbCurrentDay.Text = "Current Day: " + dayNames[dayIdx];

            var d = days[dayIdx];
            suppressGridEvents = true;
            if (d.type == "custom")
                cbScheduleType.SelectedIndex = 0;
            else
            {
                int t;
                if (int.TryParse(d.type, out t) && t >= 1 && t <= 7) cbScheduleType.SelectedIndex = t;
                else cbScheduleType.SelectedIndex = 0;
            }
            RenderGridFromModel();
            suppressGridEvents = false;
        }

        void RenderGridFromModel()
        {
            suppressGridEvents = true;
            dgv.Rows.Clear();

            DaySettings src = ResolveDay(currentDay);
            bool isFollowing = days[currentDay].type != "custom";

            foreach (var s in src.slots)
            {
                int r = dgv.Rows.Add();
                dgv.Rows[r].Cells[colTime.Index].Value = string.Format("{0:00}:{1:00}", s.hour, s.minute);
                dgv.Rows[r].Cells[colSoundFile.Index].Value = s.soundfile;
                if (isFollowing)
                {
                    dgv.Rows[r].Cells[colTime.Index].ReadOnly = true;
                    dgv.Rows[r].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                }
            }
            btAdd.Enabled = !isFollowing;
            dgv.ReadOnly = isFollowing;
            suppressGridEvents = false;
        }

        void CommitGridToModel()
        {
            var d = days[currentDay];
            if (d.type != "custom") return;

            var newSlots = new List<Slot>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string timeStr = (row.Cells[colTime.Index].Value + "").Trim();
                int h, m;
                if (!ParseTime(timeStr, out h, out m)) { h = 0; m = 0; }
                string file = (row.Cells[colSoundFile.Index].Value + "").Trim();
                newSlots.Add(new Slot { hour = h, minute = m, soundfile = file });
            }

            // Check if anything actually changed
            bool changed = d.slots.Count != newSlots.Count;
            if (!changed)
            {
                for (int i = 0; i < d.slots.Count; i++)
                {
                    if (d.slots[i].hour != newSlots[i].hour ||
                        d.slots[i].minute != newSlots[i].minute ||
                        !string.Equals(d.slots[i].soundfile, newSlots[i].soundfile, StringComparison.OrdinalIgnoreCase))
                    {
                        changed = true;
                        break;
                    }
                }
            }

            d.slots = newSlots;
            if (changed)
            {
                SetDirty(true);
            }
            ScheduleNextBell();
        }

        DaySettings ResolveDay(int dayIdx)
        {
            var visited = new Dictionary<int, bool>();
            int cur = dayIdx;
            while (true)
            {
                if (visited.ContainsKey(cur)) return new DaySettings();
                visited[cur] = true;
                var d = days[cur];
                if (d.type == "custom") return d;
                int t;
                if (int.TryParse(d.type, out t) && t >= 1 && t <= 7 && t != cur)
                {
                    cur = t;
                    continue;
                }
                return new DaySettings();
            }
        }

        bool ParseTime(string s, out int hour, out int minute)
        {
            hour = 0; minute = 0;
            if (s == null || s.Trim().Length == 0) return false;
            string[] p = s.Split(':');
            if (p.Length != 2) return false;
            if (!int.TryParse(p[0], out hour)) return false;
            if (!int.TryParse(p[1], out minute)) return false;
            return hour >= 0 && hour <= 23 && minute >= 0 && minute <= 59;
        }

        void PlaySound(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;
            string fp = Path.Combine(folderMusic, filename);
            if (!File.Exists(fp))
            {
                lbStatus.Text = "Missing file: " + filename;
                return;
            }
            try { player.controls.stop(); } catch { }
            player.URL = fp;
            lbStatus.Text = "Playing: " + filename;
        }

        void BellTimer_Tick(object sender, EventArgs e)
        {
            bellTimer.Stop();
            if (targetDay >= 1 && targetDay <= 7 && targetSlotIndex >= 0)
            {
                var src = ResolveDay(targetDay);
                if (targetSlotIndex < src.slots.Count)
                    PlaySound(src.slots[targetSlotIndex].soundfile);
            }
            ScheduleNextBell();
        }

        void ClockTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Second == 0)
                ScheduleNextBell();
        }

        void ScheduleNextBell()
        {
            bellTimer.Stop();
            DateTime now = DateTime.Now;

            for (int offset = 0; offset < 8; offset++)
            {
                DateTime probeDate = now.Date.AddDays(offset);
                int dIdx = DayOfWeekToIndex(probeDate.DayOfWeek);
                var src = ResolveDay(dIdx);

                // rows are kept in the order the user entered them, so look for
                // the earliest upcoming slot rather than the first one in the list
                int best = -1;
                DateTime bestTime = DateTime.MaxValue;
                for (int i = 0; i < src.slots.Count; i++)
                {
                    var s = src.slots[i];
                    if (string.IsNullOrEmpty(s.soundfile)) continue;
                    DateTime t = probeDate.AddHours(s.hour).AddMinutes(s.minute);
                    if (t > now && t < bestTime)
                    {
                        best = i;
                        bestTime = t;
                    }
                }

                if (best >= 0)
                {
                    double ms = (bestTime - now).TotalMilliseconds;
                    if (ms < 100) ms = 100;
                    if (ms > int.MaxValue) ms = int.MaxValue;
                    bellTimer.Interval = (int)ms;
                    targetDay = dIdx;
                    targetSlotIndex = best;
                    bellTimer.Start();
                    lbNextBell.Text = "Next bell: " + dayNames[dIdx] + " " + bestTime.ToString("HH:mm");
                    return;
                }
            }
            lbNextBell.Text = "Next bell: (none scheduled)";
            targetDay = -1;
            targetSlotIndex = -1;
        }

        void UpdateFileStatus()
        {
            lbFile.Text = "Current Loaded Settings File: " +
                (string.IsNullOrEmpty(currentSettingsPath) ? "(new)" : currentSettingsPath);
            try { File.WriteAllText(lastFilePointer, currentSettingsPath ?? ""); } catch { }
        }

        string BuildSettingsText()
        {
            CommitGridToModel();

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 7; i++)
            {
                if (i > 1) sb.AppendLine("----");
                var d = days[i];
                sb.AppendLine("Day=" + i);
                sb.AppendLine("Type=" + d.type);
                foreach (var s in d.slots)
                    sb.AppendLine(string.Format("{0:00}:{1:00}|{2}", s.hour, s.minute, s.soundfile));
            }
            return sb.ToString();
        }

        void LoadFromFile(string path, bool silent)
        {
            try
            {
                var fresh = new Dictionary<int, DaySettings>();
                for (int i = 1; i <= 7; i++) fresh[i] = new DaySettings();

                string[] lines = File.ReadAllLines(path);
                int curDay = -1;
                foreach (var raw in lines)
                {
                    string line = raw.Trim();
                    if (line.Length == 0) continue;
                    if (line == "----") { curDay = -1; continue; }
                    if (line.StartsWith("Day=", StringComparison.OrdinalIgnoreCase))
                    {
                        int.TryParse(line.Substring(4).Trim(), out curDay);
                        continue;
                    }
                    if (line.StartsWith("Type=", StringComparison.OrdinalIgnoreCase))
                    {
                        if (curDay >= 1 && curDay <= 7)
                        {
                            string t = line.Substring(5).Trim().ToLowerInvariant();
                            if (t == "custom") fresh[curDay].type = "custom";
                            else
                            {
                                int n;
                                fresh[curDay].type = (int.TryParse(t, out n) && n >= 1 && n <= 7) ? n.ToString() : "custom";
                            }
                        }
                        continue;
                    }
                    if (curDay >= 1 && curDay <= 7 && line.Contains("|"))
                    {
                        int bar = line.IndexOf('|');
                        string timePart = line.Substring(0, bar).Trim();
                        string filePart = line.Substring(bar + 1).Trim();
                        int h, m;
                        if (ParseTime(timePart, out h, out m))
                            fresh[curDay].slots.Add(new Slot { hour = h, minute = m, soundfile = filePart });
                    }
                }

                days = fresh;
                SelectDay(currentDay);
                ScheduleNextBell();
                SetDirty(false);
                if (!silent) MessageBox.Show("Settings loaded.");
            }
            catch (Exception ex)
            {
                if (!silent) MessageBox.Show("Cannot load:\r\n" + ex.Message);
            }
        }

        // ---------- event handlers ----------

        private void btDay_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b == null || b.Tag == null) return;
            int idx;
            if (int.TryParse(b.Tag.ToString(), out idx)) SelectDay(idx);
        }

        private void cbScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (suppressGridEvents) return;
            int sel = cbScheduleType.SelectedIndex;
            var d = days[currentDay];
            if (sel <= 0) d.type = "custom";
            else d.type = sel.ToString();

            bool custom = d.type == "custom";
            btAdd.Enabled = custom;
            dgv.ReadOnly = !custom;
            RenderGridFromModel();
            SetDirty(true);
            ScheduleNextBell();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (days[currentDay].type != "custom") return;
            days[currentDay].slots.Add(new Slot { hour = 0, minute = 0, soundfile = "" });
            RenderGridFromModel();
            SetDirty(true);
        }

        void SetDirty(bool value)
        {
            dirty = value;
            this.Text = "School Bell System - Weekly Scheduler - V" + Program.version + (dirty ? " *" : "");
            btSave.Text = dirty ? "💾 Save Changes *" : "Save";
            btSave.BackColor = dirty ? Color.FromArgb(220, 38, 38) : Color.FromArgb(37, 99, 235);
            btSave.FlatAppearance.BorderColor = dirty ? Color.FromArgb(185, 28, 28) : Color.FromArgb(29, 78, 216);
        }

        // Saves to the currently loaded file (and settings.txt) without asking.
        bool SaveCurrent()
        {
            dgv.EndEdit();
            string text = BuildSettingsText();
            try
            {
                File.WriteAllText(settingsFile, text);
                if (!string.IsNullOrEmpty(currentSettingsPath) &&
                    !string.Equals(Path.GetFullPath(currentSettingsPath), Path.GetFullPath(settingsFile), StringComparison.OrdinalIgnoreCase))
                    File.WriteAllText(currentSettingsPath, text);
                else
                    currentSettingsPath = settingsFile;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot save settings:\r\n" + ex.Message);
                return false;
            }
            UpdateFileStatus();
            SetDirty(false);
            lbStatus.Text = "Settings saved " + DateTime.Now.ToString("HH:mm:ss");
            return true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveCurrent();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            bool isFollowing = days[currentDay].type != "custom";

            if (e.ColumnIndex == colPlay.Index)
            {
                string file = (dgv.Rows[e.RowIndex].Cells[colSoundFile.Index].Value + "").Trim();
                PlaySound(file);
            }
            else if (e.ColumnIndex == colSelect.Index)
            {
                if (isFollowing) return;
                using (frmSoundList f = new frmSoundList())
                {
                    if (f.ShowDialog(this) != DialogResult.OK) return;
                    string filename = f.SelectedSoundFile;
                    if (string.IsNullOrEmpty(filename)) return;

                    dgv.Rows[e.RowIndex].Cells[colSoundFile.Index].Value = filename;
                    // cascade to following empty rows
                    for (int i = e.RowIndex + 1; i < dgv.Rows.Count; i++)
                    {
                        string cur = (dgv.Rows[i].Cells[colSoundFile.Index].Value + "").Trim();
                        if (cur.Length == 0)
                            dgv.Rows[i].Cells[colSoundFile.Index].Value = filename;
                        else
                            break;
                    }
                    CommitGridToModel();
                }
            }
            else if (e.ColumnIndex == colRemove.Index)
            {
                if (isFollowing) return;
                dgv.Rows[e.RowIndex].Cells[colSoundFile.Index].Value = "";
                CommitGridToModel();
            }
            else if (e.ColumnIndex == colDelete.Index)
            {
                if (isFollowing) return;
                dgv.Rows.RemoveAt(e.RowIndex);
                CommitGridToModel();
            }
        }

        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colTime.Index) return;
            string v = (e.FormattedValue + "").Trim();
            if (v.Length == 0) return;
            int h, m;
            if (!ParseTime(v, out h, out m))
            {
                MessageBox.Show("Invalid time. Use HH:mm (00:00 .. 23:59).");
                e.Cancel = true;
            }
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Only time column is directly editable in the grid cells
            if (!suppressGridEvents && e.ColumnIndex == colTime.Index)
            {
                CommitGridToModel();
            }
        }

        private void miLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Settings (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.InitialDirectory = Application.StartupPath;
                if (ofd.ShowDialog(this) != DialogResult.OK) return;
                LoadFromFile(ofd.FileName, silent: false);
                try { File.Copy(ofd.FileName, settingsFile, true); } catch { }
                currentSettingsPath = ofd.FileName;
                UpdateFileStatus();
            }
        }

        private void miSave_Click(object sender, EventArgs e)
        {
            try { File.WriteAllText(settingsFile, BuildSettingsText()); }
            catch (Exception ex) { MessageBox.Show("Cannot save settings.txt:\r\n" + ex.Message); return; }
            SetDirty(false);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Settings (*.txt)|*.txt";
                sfd.InitialDirectory = string.IsNullOrEmpty(currentSettingsPath)
                    ? Application.StartupPath
                    : (Path.GetDirectoryName(currentSettingsPath) ?? Application.StartupPath);
                sfd.FileName = string.IsNullOrEmpty(currentSettingsPath)
                    ? "schoolbell-settings.txt"
                    : Path.GetFileName(currentSettingsPath);
                if (sfd.ShowDialog(this) != DialogResult.OK)
                {
                    MessageBox.Show("Settings saved to " + settingsFile);
                    return;
                }
                try
                {
                    File.WriteAllText(sfd.FileName, BuildSettingsText());
                    currentSettingsPath = sfd.FileName;
                    UpdateFileStatus();
                    MessageBox.Show("Settings saved.");
                }
                catch (Exception ex) { MessageBox.Show("Cannot save:\r\n" + ex.Message); }
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout f = new frmAbout())
            {
                f.ShowDialog(this);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            dgv.EndEdit();
            if (dirty)
            {
                DialogResult r = MessageBox.Show(
                    "You have unsaved changes. Save them before closing?",
                    "School Bell System", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (r == DialogResult.Cancel || (r == DialogResult.Yes && !SaveCurrent()))
                {
                    e.Cancel = true;
                    return;
                }
            }
            try { player.controls.stop(); } catch { }
            base.OnFormClosing(e);
        }
    }
}
