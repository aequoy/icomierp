using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Interactivity.Core;

namespace WP8Controls
{
    /// <summary>
    /// Interaction logic for ApplicationBar.xaml
    /// </summary>
    public partial class ApplicationBar : UserControl
    {
        private double mousex = -1;
        public ApplicationBar()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Collapsed", true);

            
            this.MouseDown += (sm, em) =>
                {
                    mousex = Mouse.GetPosition(this).X;

                };

            this.MouseLeave += (sm, em) =>
                {
                    if (!IsCollapsed)
                    {
                        CanChange = true;
                    }
                };

            this.MouseMove += (sm, em) =>
                {
                    if (em.LeftButton == MouseButtonState.Released)
                    {
                        CanChange = true;
                    }
                    else if (em.LeftButton == MouseButtonState.Pressed && CanChange)
                    {
                        if (Mouse.GetPosition(this).X - mousex > 20)
                        {
                            VisualStateManager.GoToElementState(this, "Collapsed", true);
                            IsCollapsed = true;
                            CanChange = false;
                        }
                    }
                };

            this.Loaded += (sl, el) =>
                {
                    (this.Parent as FrameworkElement).MouseMove += (sm, em) =>
                    {

                        if (em.LeftButton == MouseButtonState.Pressed && CanChange && IsCollapsed &&
                            Mouse.GetPosition(this.Parent as IInputElement).X > (this.Parent as Grid).ActualWidth - 120)
                        {
                            
                                IsCollapsed = false;
                                VisualStateManager.GoToElementState(this, "Normal", true);
                                CanChange = false;
                            
                        }
                        else if (em.LeftButton == MouseButtonState.Released && IsCollapsed &&
                            Mouse.GetPosition(this.Parent as IInputElement).X > (this.Parent as Grid).ActualWidth - 120)
                        {
                            CanChange = true;
                        }

                    };
                    VisualStateManager.GoToElementState(this, "Collapsed", true);
                };
        }

        

       

        private bool IsCollapsed = true;
        private bool CanChange = true;


        //private void Grid_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Released)
        //    {
        //        CanChange = true;
        //    }
        //    else if (e.LeftButton == MouseButtonState.Pressed && CanChange)
        //    {
        //        if (Mouse.GetPosition(this.LayoutRoot).X - mousex > 20)
        //        {
        //            VisualStateManager.GoToElementState(this.LayoutRoot, "Collapsed", true);
        //            IsCollapsed = true;
        //            CanChange = false;
        //        }
        //    }
        //}

        //private void LayoutRoot_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (!IsCollapsed)
        //    {
        //        CanChange = true;
        //    }
        //}

        //private double mousex = -1;
        //private void LayoutRoot_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    mousex = Mouse.GetPosition(this.LayoutRoot).X;
        //}

       
    }
}
