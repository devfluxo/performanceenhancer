﻿using Booster.Misc;
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Booster
{
    public partial class Main : Form
    {
        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        RegistryKey key =  Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoModify");

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
                this.Hide();
                chkRun.Checked = true;
            }

            if (key.GetValue("AutoTimer") == null)
            {
                automodify.Checked = false;
            }
            else
            {
                automodify.Checked = true;
                WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
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

            if (automodify.Checked)
            {
                key.SetValue("AutoTimer", "true");
                WinApiCalls.SetTimerResolution((uint)(0.5 * 10000));
            }
            else
            {
                key.DeleteValue("AutoTimer", false);
                WinApiCalls.SetTimerResolution(0, false);
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

        private void contextMenuStrip1_Click(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void openGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
