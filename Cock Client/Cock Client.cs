﻿using Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cock_Client
{
    public partial class Cock_Client : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        public Mem m = new Mem();
        public Cock_Client()
        {
            InitializeComponent();
        }

        bool ProcOpen = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcOpen = m.OpenProcess("Minecraft.Windows.exe");
            if (!ProcOpen)
            {
                Thread.Sleep(1000);
                return;
            }

            Thread.Sleep(75);
            BGWorker.ReportProgress(0);
        }

        private void Cock_Client_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void BGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProcOpen)
                ProcOpenLabel.Text = "Open";

            short ckey = GetAsyncKeyState(Keys.C);


            if ((ckey & 0x8000) > 0)
            {
                if (Zoom.Checked)
                {
                    Zoom.Checked = false;
                }
                else if (!Zoom.Checked)
                {
                    Zoom.Checked = true;
                }
            }

            if (TriggerBotEnabled)
            {
                if (m.ReadInt("Minecraft.Windows.exe+0369A300,0x2E0,0x6C0,0x900", "") != 0)
                {
                    uint X = (uint)Cursor.Position.X;
                    uint Y = (uint)Cursor.Position.Y;
                    Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN | Win32.MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                }
            }
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void Suicide_Click(object sender, EventArgs e)
        {
            m.WriteMemory("base+036A3EE8,0x8,0x18,0xA8,0x19C", "float", "1000");
        }

        private void Fly_CheckedChanged(object sender, EventArgs e)
        {
            if (Fly.Checked)
            {
                m.FreezeValue("base+36A3EE8,0x0,0x20,0x0xB8,0x8B8", "int", "1");
            }

            else if (!Fly.Checked)
            {
                m.UnfreezeValue("base+36A3EE8,0x0,0x20,0x0xB8,0x8B8");
            }
        }

        private void AirJump_CheckedChanged(object sender, EventArgs e)
        {
            if (AirJump.Checked)
            {
                m.FreezeValue("base+036A3EE8,0x0,0x20,0xB8,0x1A0", "int", "16777473");
            }

            else if (!Fly.Checked)
            {
                m.UnfreezeValue("base+036A3EE8,0x0,0x20,0xB8,0x1A0");
            }
        }

        private void Fullbright_CheckedChanged(object sender, EventArgs e)
        {
            if (Fullbright.Checked)
            {
                m.WriteMemory("Minecraft.Windows.exe+03657E90,0x38,0x148,0xF0","float","1000");
            }

            else if (!Fullbright.Checked)
            {
                m.WriteMemory("Minecraft.Windows.exe+03657E90,0x38,0x148,0xF0", "float", "1");
            }
        }

        private void Fly_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Zoom_CheckedChanged(object sender, EventArgs e)
        {
            if (Zoom.Checked)
            {
                m.WriteMemory("Minecraft.Windows.exe+03657E90,0x38,0x130,0xF0", "float", "30");
            }
            else if (!Zoom.Checked)
            {
                m.WriteMemory("Minecraft.Windows.exe+03657E90,0x38,0x130,0xF0", "float", "90");
            }
        }

        private void UserFOV_Click(object sender, EventArgs e)
        {

        }

        private void Speed_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Speed.Checked)
            {
                m.FreezeValue("Minecraft.Windows.exe+0369CC08,0xE8,0x478,0x18,0x1F8,0x9C", "float", SpeedValue.Text);
            }
            else if (!Speed.Checked)
            {
                m.UnfreezeValue("Minecraft.Windows.exe+0369CC08,0xE8,0x478,0x18,0x1F8,0x9C");
                m.WriteMemory("Minecraft.Windows.exe+0369CC08,0xE8,0x478,0x18,0x1F8,0x9C", "float", "0.1000000100");
            }
        }

        private void autoctrl_CheckedChanged(object sender, EventArgs e)
        {
            if (autoctrl.Checked)
            {
                m.FreezeValue("Minecraft.Windows.exe+0369AF60,0xF0,0x18,0x0,0x110,0x2E0,0x8,0x14E", "int", "0");
            }
            else if (!autoctrl.Checked)
            {
                m.UnfreezeValue("Minecraft.Windows.exe+0369AF60,F0,18,0,110,2E0,8,14E");
            }
        }

        private void autoshift_CheckedChanged(object sender, EventArgs e)
        {
            if (autoshift.Checked)
            {
                m.FreezeValue("Minecraft.Windows.exe+0369AF60,F0,18,0,110,2E0,8,14D", "float", "0");
            }
            else if (!autoshift.Checked)
            {
                m.UnfreezeValue("Minecraft.Windows.exe+0369AF60,F0,18,0,110,2E0,8,14D");
            }
        }

        private void autospace_CheckedChanged(object sender, EventArgs e)
        {
            if (autospace.Checked)
            {
                m.FreezeValue("Minecraft.Windows.exe+0369AF60,F0,18,0,110,2E0,8,15D", "byte", "0");
            }
            else if (!autospace.Checked)
            {
                m.UnfreezeValue("Minecraft.Windows.exe+0369AF60,F0,18,0,110,2E0,8,15D");
            }
        }

        bool TriggerBotEnabled = false;
        private void TriggerBot_CheckedChanged(object sender, EventArgs e)
        {
            if (TriggerBot.Checked)
            {
                TriggerBotEnabled = true;
            }

            else if (!TriggerBot.Checked)
            {
                TriggerBotEnabled = false;
            }
        }

        public class Win32
        {
            [DllImport("User32.Dll")]
            public static extern long SetCursorPos(int x, int y);

            [DllImport("User32.Dll")]
            public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
            //Mouse actions
            public const int MOUSEEVENTF_LEFTDOWN = 0x02;
            public const int MOUSEEVENTF_LEFTUP = 0x04;
            public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
            public const int MOUSEEVENTF_RIGHTUP = 0x10;

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;
            }
        }

        private void ProcOpenLabel_TextChanged(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            UserFOV.Text = m.ReadFloat("Minecraft.Windows.exe+03657E90,0x38,0x130,0xF0", "").ToString();
        }

        private void NoFall_CheckedChanged(object sender, EventArgs e)
        {
            if (NoFall.Checked)
            {
                m.FreezeValue("base+036A3EE8,0x8,0x18,0xA8,0x19C", "float", "0");
            }

            else if (!NoFall.Checked)
            {
                m.UnfreezeValue("base+036A3EE8,0x8,0x18,0xA8,0x19C");
            }
        }

        private void keybinds_Click(object sender, EventArgs e)
        {
            MessageBox.Show("C - Zoom");
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (InstaBreak.Checked)
            {
                m.FreezeValue("Minecraft.Windows.exe+036A3EF8,0x8,0x18,0xA8,0x19C", "float", "1");
            }

            else if (!InstaBreak.Checked)
            {
                m.UnfreezeValue("Minecraft.Windows.exe+036A3EF8,0x8,0x18,0xA8,0x19C");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}