using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;

namespace WP8Controls
{
    // Summary:
    //     Represents an item in a Panorama control.
    public class PanoramaItem : ContentControl
    {

        // Summary:
        //     Identifies the Header dependency property.
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
           "Header", typeof(object), typeof(PanoramaItem), new PropertyMetadata(null));


        // Summary:
        //     Identifies the Header dependency property.
        public static readonly DependencyProperty HeaderOpacityProperty = DependencyProperty.Register(
           "HeaderOpacity", typeof(double), typeof(PanoramaItem), new PropertyMetadata(1.0));
        
        //
        // Summary:
        //     Identifies the HeaderTemplate dependency property.
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
           "HeaderTemplate", typeof(DataTemplate), typeof(PanoramaItem), new PropertyMetadata(null));

        //
        // Summary:
        //     Identifies the Orientation dependency property.
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
           "Orientation", typeof(Orientation), typeof(PanoramaItem), new PropertyMetadata(Orientation.Horizontal));

        static PanoramaItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PanoramaItem),
                new FrameworkPropertyMetadata(typeof(PanoramaItem)));
        }



        // Summary:
        //     Gets or sets the header for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Object .
        public object Header
        {
            get { return (object)this.GetValue(HeaderProperty); }
            set
            {
                this.SetValue(HeaderProperty, value);
            }
        }

        // Summary:
        //     Gets or sets the header for the PanoramaItem.
        //
        // Returns:
        //     Returns System.Object .
        public double HeaderOpacity
        {
            get { return (double)this.GetValue(HeaderOpacityProperty); }
            set
            {
                this.SetValue(HeaderOpacityProperty, value);
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
