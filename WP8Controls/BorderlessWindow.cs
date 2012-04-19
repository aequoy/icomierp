using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WP8Controls
{
    public class BorderlessWindow : Window
    {
        public BorderlessWindow()
        {
            this.Margin = new Thickness(0);
            this.WindowStyle = WindowStyle.None;
            this.BorderThickness = new Thickness(0);
            this.ResizeMode = ResizeMode.NoResize;
            this.AllowsTransparency = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
