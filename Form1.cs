
using System;
using miniclock.Properties;
using System.Drawing;
using System.IO;

using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace miniclock
{

    public partial class Form_main : Form
    {
        MyClockSettings sets = new MyClockSettings();
        LinkedList<Color> colors = new LinkedList<Color>();
        //Color transparent = Color.Navy;
        int mactype_w = 50; //这个变量是用来治疗mactype导致自己渲染之后显示不全的问题的
        private void Form1_Load(object sender, EventArgs e)
        {
            sets.GetSettings();
            setValues();
        }
        public void setValues()
        {
            label1.Width = sets.width;
            label1.Height = sets.height;
            switch (sets.position)
            {
                case 0:
                    topRightToolStripMenuItem.PerformClick();
                    break;
                case 1:
                    topLeftToolStripMenuItem.PerformClick();
                    break;
                case 2:
                    middleToolStripMenuItem.PerformClick();
                    break;
            }
            label1.BackColor = sets.bg_color; //文本颜色、
            label1.ForeColor = sets.fore_color; //恢复文本颜色
            label1.Font = sets.fontStyle; //恢复字体设置
        }

        public Form_main()
        {
            // 做这些是randomcolor用到的颜色
            colors.AddLast(Color.Aqua);
            colors.AddLast(Color.PowderBlue);
            colors.AddLast(Color.Plum);
            colors.AddLast(Color.Gold);
            colors.AddLast(Color.DarkOrange);
            colors.AddLast(Color.Chartreuse);
            colors.AddLast(Color.Pink);
            colors.AddLast(Color.GreenYellow);

            this.ShowInTaskbar = false;//这个程序将不会显示在任务栏
            InitializeComponent();
            //defbackground = Color.WhiteSmoke; //保存一下初始的那个背景颜色，因为打字打不出来

            timer1.Interval = 10000; //10s one tick timer
            timer1.Tick += new EventHandler(gettime); //ontick event
            timer1.Start();
            label1.Text = DateTime.Now.ToString("HH:mm");


            //this.Width = 99;
            //this.Height = 18;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Right - mactype_w, -1);//显示在右上角
        }

        private void gettime(Object myObject,EventArgs myEventArgs)
        {
            label1.Text = DateTime.Now.ToString("HH:mm");
        }
        //
        //读取新修改的设置
        private void label1_Click(object sender, EventArgs e)
        {
            sets.GetSettings();
            setValues();

            contextMenuStrip1.Show(Cursor.Position);//在鼠标位置显示一个菜单
        }

        //close button
        private void aDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=P432z8q9iVE 关于存储设置的教程
            sets.Save();
            this.Close();
        }

        //next theme button
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //更换文字颜色
            //http://www.flounder.com/csharp_color_table.htm 一些c#颜色
            Color foreColoNext;
            
            foreColoNext = colors.First.Value;
            LinkedListNode<Color> a = colors.First;
            colors.Remove(colors.First);
            colors.AddLast(a);
            label1.ForeColor = foreColoNext;
            //随机的时候使用的范围是0~color的长度

            sets.fore_color = foreColoNext; //存储一下
            sets.Save();
        }
      
        //切换黑色模式/白色模式/透明模式
        private void hacker_styleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (sets.bg_color==Color.White)
            {
                sets.bg_color = Color.Black; 
                sets.fore_color = Color.White;
            }
            else if (sets.bg_color == Color.Black)
            {
                sets.bg_color = Color.Navy;
                sets.fore_color = Color.Cyan;
            }
            else if(sets.bg_color == Color.Navy)
            {
                sets.bg_color = Color.White;
                sets.fore_color = Color.Black;
            }
            else
            {
                sets.bg_color = Color.Black;
                sets.fore_color = Color.White;
            }
            sets.Save();
            setValues();
        }


        //不显示在 alt tab 菜单
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt+Tab
                return cp;
            }
        }


        #region 位置设定
        //position -topleft
        private void topLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Location = new Point(-3, -1);//显示在zuo上角
            sets.position = 1;        //位置 0--top right , 1--top left
            sets.Save();
        }

        //position -topright
        private void topRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Right-sets.width, -1);//显示在右上角
            sets.position = 0;        //位置 0--top right , 1--top left
            sets.Save();
        }
        private void middleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Right-sets.width)/2 , -1);//显示在顶部中心
            sets.position = 2;        //位置 2-middle
            sets.Save();
        }
        #endregion

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form b = new form_info();
            b.Visible = true;
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";  // Edge浏览器的安装路径

            Process.Start(edgePath);  // 启动Edge浏览器
        }

    }
}
