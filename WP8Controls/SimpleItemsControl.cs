using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace WP8Controls
{
    public class SimpleItemsControl : ItemsControl
    {
        #region Override Size
        // Summary:
        //     Identifies the ItemWidth dependency property.
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
           "ItemWidth", typeof(double), typeof(SimpleItemsControl), new PropertyMetadata(320.0));
        // Summary:
        //     Identifies the ItemHeight dependency property.
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
           "ItemHeight", typeof(double), typeof(SimpleItemsControl), new PropertyMetadata(240.0));

        //
        // Summary:
        //     Gets or sets the width of the Panorama childrens.
        //
        // Returns:
        //     Returns System.Double
        public double ItemWidth
        {
            get { return (double)this.GetValue(ItemWidthProperty); }
            set
            {
                this.SetValue(ItemWidthProperty, value); 
            }
        }

        //
        // Summary:
        //     Gets or sets the width of the Panorama childrens.
        //
        // Returns:
        //     Returns System.Double
        public double ItemHeight
        {
            get { return (double)this.GetValue(ItemHeightProperty); }
            set
            {
                this.SetValue(ItemHeightProperty, value);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            (element as PanoramaItem).Width = ItemWidth;
            (element as PanoramaItem).Height = ItemHeight;
        }
        #endregion

        private Action<String> GoToState;
        private bool MouseCaptured = false;
        private double CurrentWidth = -1;
        private double Increment;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (!DesignerProperties.GetIsInDesignMode(this)) //Not in Design Mode
            {
                this.Loaded += (s, e) =>
                    {
                        foreach (object ob in this.Items)
                        {
                            (ob as PanoramaItem).RenderTransform = new TranslateTransform();
                        }
                    };


                GoToState = (s) => VisualStateManager.GoToElementState(this, s, true);
                double w = -1 * this.ItemWidth;
                Int32 c;
                CompositionTarget.Rendering += (s, e) =>
                {
                    #region Calculate Acceleration and Go to latest position
                    if (MouseCaptured && Mouse.LeftButton == MouseButtonState.Released)
                    {
                        MouseCaptured = false;
                        CurrentWidth = -1;
                        if (Traslation < (this.Items.Count - 1) * w)
                            c = this.Items.Count - 1;
                        else if (Traslation < 0)
                        {
                            c = (Int32)(Traslation / w);
                            c += Math.Abs(Traslation) > Math.Abs(c * w + w / 2) ? 1 : 0;
                        }
                        else
                            c = 0;

                        Animate(Traslation, Math.Min(0, c * w));
                        return;
                    }
                    #endregion

                    #region Current Translation
                    if (!MouseCaptured && Mouse.LeftButton == MouseButtonState.Pressed && MouseOver())
                    {
                        MouseCaptured = true;
                        CurrentWidth = Mouse.GetPosition(this.Items[0] as PanoramaItem).X;
                        return;
                    }
                    #endregion

                    #region New Translation
                    if (MouseCaptured && Mouse.LeftButton == MouseButtonState.Pressed && MouseOver())
                    {
                        Increment = Mouse.GetPosition(this.Items[0] as PanoramaItem).X;
                        this.Traslation += Increment - CurrentWidth;
                    }
                    #endregion
                };
            }
            else
            {
                
            }
        }
        public SimpleItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleItemsControl),
               new FrameworkPropertyMetadata(typeof(SimpleItemsControl)));
        }

        #region Translation
        private double Traslation
        {
            get
            {
                if (this.Items != null || this.Items.Count > 0)
                {
                    if ((this.Items[0] as PanoramaItem).RenderTransform == null)
                    {
                        foreach (object ob in this.Items)
                        {
                            (ob as PanoramaItem).RenderTransform = new TranslateTransform();
                        }
                    }

                    return ((TranslateTransform)((this.Items[0] as PanoramaItem).RenderTransform)).X;
                }
                else
                    return 0;
            }
            set
            {
                if (this.Items != null || this.Items.Count > 0)
                {
                    foreach (object ob in this.Items)
                    {
                        ((TranslateTransform)((ob as PanoramaItem).RenderTransform)).X = value;
                    }
                }
            }
        }
        #endregion

        #region Touch Manipulation
        private bool MouseOver()
        {
            Point p = Mouse.GetPosition(this);
            return !(p.X < 0 || p.X > this.Width || p.Y < 0 || p.Y > this.Height);
        }
        #endregion

        #region Animation
        private TranslateTransform RenderTranslateTransform(object ob)
        {
            return (ob as PanoramaItem).RenderTransform as TranslateTransform;
        }

        private void Animate(double begin, double end)
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.Completed += (s, e) =>
            {
                foreach (object ob in this.Items)
                {
                    (ob as PanoramaItem).RenderTransform = new TranslateTransform(RenderTranslateTransform(ob).X, RenderTranslateTransform(ob).Y);
                }
            };

            EasingDoubleKeyFrame e0 = new EasingDoubleKeyFrame(begin, KeyTime.FromTimeSpan(TimeSpan.Zero));
            e0.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.5 };
            da.KeyFrames.Add(e0);

            EasingDoubleKeyFrame e1 = new EasingDoubleKeyFrame(end, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6)));
            e1.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseOut, Amplitude = 0.5 };
            da.KeyFrames.Add(e1);

            foreach (object ob in this.Items)
            {
                RenderTranslateTransform(ob).BeginAnimation(TranslateTransform.XProperty, da, HandoffBehavior.SnapshotAndReplace);
            }
        }
        #endregion
    }
}
