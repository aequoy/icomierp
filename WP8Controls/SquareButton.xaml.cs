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

namespace WP8Controls
{
    /// <summary>
    /// Interaction logic for SquareButton.xaml
    /// </summary>
    public partial class SquareButton : Button
    {
        // Summary:
        //     Identifies the Image dependency property.
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
           "Image", typeof(ImageSource), typeof(SquareButton), new PropertyMetadata(null));


        // Summary:
        //     Identifies the Text dependency property.
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
           "Text", typeof(string), typeof(SquareButton), new PropertyMetadata(String.Empty));

        
        // Summary:
        //     Gets or sets the header for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Object .
        public ImageSource Image
        {
            get { return (ImageSource)this.GetValue(ImageProperty); }
            set
            {
                this.SetValue(ImageProperty, value);
            }
        }

        // Summary:
        //     Gets or sets the header for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Object .
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public SquareButton()
        {
            InitializeComponent();
        }
    }
}
