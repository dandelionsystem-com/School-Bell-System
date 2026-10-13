using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SchoolBell
{
    public partial class baseForm : Form
    {
        public baseForm()
        {
            InitializeComponent();
            try
            {
                if (!FontFamily.GenericSansSerif.Name.Equals("Segoe UI", StringComparison.OrdinalIgnoreCase))
                {
                    using (Font test = new Font("Segoe UI", 9F))
                    {
                        if (test.Name == "Segoe UI")
                            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                        else
                            this.Font = new Font("Tahoma", 9F, FontStyle.Regular);
                    }
                }
            }
            catch { }
        }
    }
}
