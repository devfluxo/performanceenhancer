using Booster.Misc;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Booster
{
    public partial class Main : Form
    {
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        int a;
        int b;

        public Main()
        {
            InitializeComponent();
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {

        }

        private void ClearStandbyList()
        {
            new Win32_NtSetSystemInformation().ClearStandbyCache();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            this.ClearStandbyList();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            Update();
            if (rkApp.GetValue("Booster") == null)
            {
                chkRun.Checked = false;
            }
            else
            {
                chkRun.Checked = true;
            }
        }

        private void OnFormClosing(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution(0, false);
        }

        private void siticoneRoundedButton1_Click_1(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update();
            if (chkRun.Checked)
            {
                rkApp.SetValue("Booster", Application.ExecutablePath);
            }
            else
            {
                rkApp.DeleteValue("Booster", false);
            }
        }

        private void Update()
        {
            var caps = WinApiCalls.QueryTimerResolution();
            var ram = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            currentms.Text = "Current timer: " + (caps.PeriodCurrent / 10000.0).ToString() + "MS";
            freememory.Text = "Available memory: " + ram.NextValue().ToString() + "MB";
        }

        private void siticoneRoundedButton2_Click_1(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution(0, false);
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WinApiCalls.SetTimerResolution(0, false);
        }

        private void purgeRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Win32_NtSetSystemInformation().ClearStandbyCache();
        }

        private void asdToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
