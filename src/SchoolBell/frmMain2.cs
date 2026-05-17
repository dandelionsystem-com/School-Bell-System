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

            int todayIdx = DayOfWeekToIndex(DateTime.Now.DayOfWeek);
            SelectDay(todayIdx);

            bellTimer.Tick += BellTimer_Tick;
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            ScheduleNextBell();
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
                dayButtons[i].UseVisualStyleBackColor = true;
                dayButtons[i].BackColor = SystemColors.Control;
                dayButtons[i].Font = new Font(this.Font, FontStyle.Regular);
            }
            dayButtons[dayIdx].BackColor = Color.FromArgb(180, 220, 255);
            dayButtons[dayIdx].Font = new Font(this.Font, FontStyle.Bold);

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
            d.slots.Clear();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string timeStr = (row.Cells[colTime.Index].Value + "").Trim();
                int h, m;
                if (!ParseTime(timeStr, out h, out m)) { h = 0; m = 0; }
                string file = (row.Cells[colSoundFile.Index].Value + "").Trim();
                d.slots.Add(new Slot { hour = h, minute = m, soundfile = file });
            }
            ScheduleNextBell();
        }

        DaySettings ResolveDay(int dayIdx)
        {
            var visited = new HashSet<int>();
            int cur = dayIdx;
            while (true)
            {
                if (!visited.Add(cur)) return new DaySettings();
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
            if (string.IsNullOrWhiteSpace(s)) return false;
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
                for (int i = 0; i < src.slots.Count; i++)
                {
                    var s = src.slots[i];
                    DateTime t = probeDate.AddHours(s.hour).AddMinutes(s.minute);
                    if (t > now)
                    {
                        double ms = (t - now).TotalMilliseconds;
                        if (ms < 100) ms = 100;
                        if (ms > int.MaxValue) ms = int.MaxValue;
                        bellTimer.Interval = (int)ms;
                        targetDay = dIdx;
                        targetSlotIndex = i;
                        bellTimer.Start();
                        lbNextBell.Text = "Next bell: " + dayNames[dIdx] + " " + t.ToString("HH:mm");
                        return;
                    }
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
            ScheduleNextBell();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (days[currentDay].type != "custom") return;
            days[currentDay].slots.Add(new Slot { hour = 0, minute = 0, soundfile = "" });
            RenderGridFromModel();
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
            if (!suppressGridEvents) CommitGridToModel();
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
            try { player.controls.stop(); } catch { }
            base.OnFormClosing(e);
        }
    }
}
