using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SchoolBell
{
    public partial class frmAbout : SchoolBell.baseForm
    {
        public bool refreshLicenseInfo = false;
        public frmAbout()
        {
            InitializeComponent();
            LoadLicenseInfo();
        }

        void LoadLicenseInfo()
        {
            lbVersion.Text = Program.versiondate;
        }
    }
}