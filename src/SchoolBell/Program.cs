using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace SchoolBell
{
    static class Program
    {
        public const string version = "2.01";
        public const string versiondate = "2 (15/05/2026)";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmMain2());
        }
    }
}
