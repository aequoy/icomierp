using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WP8Controls
{
    public partial class PanoramaItem : ContentControl
    {
        // Summary:
        //     Identifies the Header dependency property.
        public static readonly DependencyProperty TextHeaderProperty = DependencyProperty.Register(
           "TextHeader", typeof(string), typeof(PanoramaItem), new UIPropertyMetadata(String.Empty));


        // Summary:
        //     Identifies the Header dependency property.
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
           "Header", typeof(object), typeof(PanoramaItem), new UIPropertyMetadata(null));

        //
        // Summary:
        //     Identifies the HeaderTemplate dependency property.
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
           "HeaderTemplate", typeof(DataTemplate), typeof(PanoramaItem), new UIPropertyMetadata(null));

        //
        // Summary:
        //     Identifies the Orientation dependency property.
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
           "Orientation", typeof(Orientation), typeof(PanoramaItem), new UIPropertyMetadata(Orientation.Horizontal));

        // Summary:
        //     Initializes a new instance of the PanoramaItem class.
        public PanoramaItem()
        {
            InitializeComponent();
        }

        // Summary:
        //     Gets or sets the header for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Object .
        public object Header
        {
            get { return this.GetValue(HeaderProperty); }
            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        //
        // Summary:
        //     Gets or sets the template for the Header property.
        //
        // Returns:
        //     Returns System.Windows.DataTemplate .
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)this.GetValue(HeaderTemplateProperty); }
            set
            {
                this.SetValue(HeaderTemplateProperty, value);
            }
        }

        //
        // Summary:
        //     Gets or sets the primary (scrolling) orientation for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Windows.Controls.Orientation .
        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }
    }
}
