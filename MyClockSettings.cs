using miniclock.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace miniclock
{
    class MyClockSettings
    {
        public int height = 22;
        public int width = 50;
        public Font fontStyle;
        internal int position;
        internal Color bg_color;
        internal Color fore_color;

        internal void Save()
        {
            Settings1.Default.position = position;
            Settings1.Default.bg_color = bg_color;
            Settings1.Default.fore_color = fore_color;
            Settings1.Default.height = height;
            Settings1.Default.width = width;
            Settings1.Default.fontStyle = fontStyle;
            Settings1.Default.Save();
        }

        internal void GetSettings()
        {
            //Settings1.Default.height = 99;
            position = Settings1.Default.position;
            bg_color = Settings1.Default.bg_color;
            fore_color = Settings1.Default.fore_color;
            height = Settings1.Default.height;
            width = Settings1.Default.width;
            fontStyle = Settings1.Default.fontStyle;
        }
    }
}
