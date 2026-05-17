using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WMPLib;

namespace SchoolBell
{
    public partial class frmSoundList : SchoolBell.baseForm
    {
        WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();

        string folder = "";

        public string SelectedSoundFile = "";

        Timer timer1 = new Timer();

        public frmSoundList()
        {
            InitializeComponent();

            Screen screen = Screen.FromControl(this);

            this.Height = screen.WorkingArea.Height - 100;

            folder = Path.Combine(Application.StartupPath, "sound");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            LoadFiles();

            timer1.Interval = 100;
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            dgv1.ClearSelection();
        }

        void LoadFiles()
        {
            List<string> lst = new List<string>();
            string[] files = Directory.GetFiles(folder);
            foreach (var f in files)
            {
                string g = Path.GetFileName(f);
                lst.Add(g);
            }

            lst.Sort(delegate (string x, string y)
            {
                return x.CompareTo(y);
            });

            dgv1.Rows.Clear();

            if (lst.Count > 0)
                dgv1.Rows.Add(lst.Count);

            for (int i = 0; i < lst.Count; i++)
            {
                dgv1.Rows[i].Cells[colnName.Index].Value = lst[i];
            }

            dgv1.ClearSelection();
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == colnPlay.Index)
            {
                string filePath = Path.Combine(folder, dgv1.Rows[e.RowIndex].Cells[colnName.Index].Value + "");
                if(File.Exists(filePath))
                {
                    player.controls.stop();
                    player.URL = filePath;
                }
            }
            else if (e.ColumnIndex == colnDelete.Index)
            {
                if (MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                int curRow1 = dgv1.FirstDisplayedCell.RowIndex;

                string file = dgv1.Rows[e.RowIndex].Cells[colnName.Index].Value + "";

                string filepath = Path.Combine(folder, file);

                try
                {
                    File.Delete(filepath);
                }
                catch { }

                LoadFiles();

                try
                {
                    dgv1.FirstDisplayedCell = dgv1.Rows[curRow1].Cells[0];
                }
                catch { }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = true;
            f.Filter = "*.wav, *.mp3, *.ogg|*.wav;*.mp3;*.ogg";

            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in f.FileNames)
                {
                    string filename = Path.GetFileName(file);
                    string filepath = Path.Combine(folder, filename);
                    if (File.Exists(filepath))
                    {
                        try
                        {
                            File.Delete(filepath);
                        }
                        catch { }
                    }
                    File.Copy(file, filepath);
                }

                LoadFiles();
            }
        }

        private void frmSoundList_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex != colnName.Index)
                return;

            SelectedSoundFile = dgv1.Rows[e.RowIndex].Cells[colnName.Index].Value + "";
            this.DialogResult = DialogResult.OK;
        }

        private void frmSoundList_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
            player = null;
        }
    }
}
