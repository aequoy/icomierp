using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WP8Controls
{
    public class Panorama : ItemsControl
    {
        public static readonly DependencyProperty HeaderOffsetProperty = DependencyProperty.Register(
           "HeaderOffset", typeof(double), typeof(Panorama), new PropertyMetadata(0.0));

        public double HeaderOffset
        {
            get { return (double)this.GetValue(HeaderOffsetProperty); }
            set
            {
                this.SetValue(HeaderOffsetProperty, value);
            }
        }

        #region Override Size
        // Summary:
        //     Identifies the ItemWidth dependency property.
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
           "ItemWidth", typeof(double), typeof(Panorama), new PropertyMetadata(960.0));
        // Summary:
        //     Identifies the ItemHeight dependency property.
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
           "ItemHeight", typeof(double), typeof(Panorama), new PropertyMetadata(480.0));

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

        #region Fields
        private bool MouseCaptured = false;
        private double Increment;
        private Action<String> GoToState;
        private double CurrentWidth = -1;

        public double Traslation
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
                        PanoramaItem currentpanorama = (ob as PanoramaItem);
                        ((TranslateTransform)(currentpanorama.RenderTransform)).X = value;
                        SetOpacity(currentpanorama);
                    }
                    SetItems();
                }
            }
        }

        

        private void SetItems()
        {
            //double t = this.Traslation;
            //double w = this.ItemWidth;

            //Int32 windows =(Int32)(-1*this.Traslation / this.ItemWidth);
            //if (windows > 0)
            //{
            //    PanoramaItem current = (PanoramaItem)this.Items[windows - 1];
            //    this.Items.RemoveAt(windows - 1);
            //    this.Items.Add(current);
            //}
            
        }

        private void SetOpacity(PanoramaItem item)
        {
            double t = Math.Abs(((TranslateTransform)(item.RenderTransform)).X);
            double w = this.ItemWidth;
            double op = t/w + 1;
            double idx = this.Items.IndexOf(item);
            op = op - idx;
            op = op > 1 ? 1 : op;
            op = op < 0.5 ? 0 : op;
            item.HeaderOpacity = op;
        }
        #endregion

        

        public Panorama()
        {
            try
            {
                DefaultStyleKeyProperty.OverrideMetadata(typeof(Panorama), new FrameworkPropertyMetadata(typeof(Panorama)));
            }
            catch { }

            if (!DesignerProperties.GetIsInDesignMode(this)) //Not in Design Mode
            {
                this.Loaded += (s, e) =>
                {
                    foreach (object ob in this.Items)
                    {
                        (ob as PanoramaItem).RenderTransform = new TranslateTransform();
                        SetOpacity(ob as PanoramaItem);
                    }
                };

                GoToState = (s) => VisualStateManager.GoToElementState(this, s, true);
                double w;
                Int32 c = 0;
                double prevtras = 0;
                CompositionTarget.Rendering += (s, e) =>
                {
                    #region Calculate Acceleration and Go to latest position
                    if (MouseCaptured && Mouse.LeftButton == MouseButtonState.Released)
                    {
                        MouseCaptured = false;
                        CurrentWidth = -1;
                        w = -1 * this.ItemWidth;

                        if (Traslation < (this.Items.Count - 1) * w)
                        {
                            c = this.Items.Count - 1;
                            prevtras = c * -1 * this.ItemWidth;
                        }
                        else if (Traslation < 0)
                        {
                            //OLD
                            //c = (Int32)(Traslation / w);
                            //c += Math.Abs(Traslation) > Math.Abs(c * w + w / 2) ? 1 : 0;
                            
                            //if (Math.Abs(Traslation) - Math.Abs(prevtras) > Math.Abs(w / 4))
                            if(Math.Abs(Traslation) + c*w > 20)
                                c += 1;
                            else if (Math.Abs(Traslation) + c * w < - 20)
                                c -= 1;

                            prevtras = c == 0 ? 0 : Traslation;
                            c = c > this.Items.Count - 1 ? this.Items.Count - 1 : c;
                        }
                        else
                        {
                            c = 0;
                            prevtras = 0;
                        }
                        
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
                //this.Loaded += (s, e) =>
                //{
                //    foreach (object ob in this.Items)
                //    {
                //        (ob as PanoramaItem).RenderTransform = new TranslateTransform();
                //        ((ob as PanoramaItem).RenderTransform as TranslateTransform).X = 0;
                //    }

                //    this.Items.CurrentChanged += (s1, e1) =>
                //    {
                //        throw new Exception();
                //        double w = -1 * (960 + 68);
                //        Traslation = this.Items.IndexOf(this.Items.CurrentItem) * w;
                //    };

                //};
            }
        }

        #region Manipulation Helper
        
        private bool MouseOver()
        {
            Point p = Mouse.GetPosition(this);
            return !(p.X < 0 || p.X > this.Width - 120 || p.Y < 0 || p.Y > this.Height);
        }

        private TranslateTransform RenderTranslateTransform(object ob)
        {
            return (ob as PanoramaItem).RenderTransform as TranslateTransform;
        }

        private void Animate(double begin, double end)
        {
            DoubleAnimationUsingKeyFrames da = new DoubleAnimationUsingKeyFrames();
            da.CurrentTimeInvalidated += (s, e) =>
                {
                    foreach (PanoramaItem ob in this.Items)
                    {
                        SetOpacity(ob);
                    }
                    

                };
            da.Completed += (s, e) =>
            {
                
                foreach (object ob in this.Items)
                {
                    (ob as PanoramaItem).RenderTransform = new TranslateTransform(RenderTranslateTransform(ob).X, RenderTranslateTransform(ob).Y);
                }
                SetItems();
                
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
