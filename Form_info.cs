using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniclock
{
    public partial class form_info : Form
    {
        public form_info()
        {
            InitializeComponent();

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
            string wallpaperPath = key.GetValue("Wallpaper").ToString();

            Bitmap wallpaper = new Bitmap(wallpaperPath);
            panel1.BackgroundImage = wallpaper;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

        }
        MyClockSettings msets = new MyClockSettings();



        protected override void OnClosing(CancelEventArgs e)
        {
            msets.Save();
        }
        private void form_info_Load(object sender, EventArgs e)
        {
            msets.GetSettings();
            try
            {
                trackBar1.Value = msets.width;
                labelDEMO.Width = msets.width;
                trackBar2.Value = msets.height;
                labelDEMO.Height = msets.height;
                labelDEMO.Font = msets.fontStyle;
                fontDialog1.Font = msets.fontStyle;
                labelDEMO.ForeColor = msets.fore_color;
                labelDEMO.BackColor = msets.bg_color;
            }
            catch (Exception)
            {
            }
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int height = trackBar2.Value;
            labelH.Text = height.ToString();
            labelDEMO.Height = height;
            msets.height = height;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           int width = trackBar1.Value ;
            labelW.Text = width.ToString();
            labelDEMO.Width = width;
            msets.width = width;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            var selectedfont = fontDialog1.Font;
            msets.fontStyle = selectedfont;
            labelDEMO.Font = selectedfont;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            var selectedColor = colorDialog1.Color;
            msets.fore_color = selectedColor;
            labelDEMO.ForeColor = selectedColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            var selectedColor = colorDialog2.Color;
            msets.bg_color = selectedColor;
            labelDEMO.BackColor = selectedColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var selectedColor = Color.Navy;
            msets.bg_color = selectedColor;
            labelDEMO.BackColor = selectedColor;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/cornradio/miniclock");
        
        }
    }
}
