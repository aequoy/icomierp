using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Net;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using ICOMI_SPClient.Utilities;
using System.Windows.Media;

namespace ICOMI_SPClient.Controls
{
    public class ImageLoader : Control, IDisposable
    {
        /// <summary>
        /// The image control which displays the image.
        /// </summary>
        private Image _image;


        /// <summary>
        /// A background thread for loading and decoding images.
        /// </summary>
        private BackgroundWorker _worker;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                _image = GetTemplateChild("PART_Image") as Image;
                UpdateUriSource();
            }

            base.OnApplyTemplate();
        }

        #region UriSource

        /// <summary>
        /// Gets or sets the image UriSource.
        /// </summary>
        /// <value>The image UriSource.</value>
        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        /// <summary>
        /// Backing store for UriSource.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.Register(
            "UriSource",
            typeof(Uri),
            typeof(ImageLoader),
            new PropertyMetadata(null, (sender, e) => (sender as ImageLoader).UpdateUriSource()));

        /// <summary>
        /// Updates the UriSource.
        /// </summary>
        private void UpdateUriSource()
        {
            if (_image == null)
            {
                return;
            }

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                _image.Opacity = 1;
                return;
            }

            IsImageLoading = true;

            if (UriSource == null)
            {
                _image.Source = null;
                return;
            }

            if (_worker != null)
            {
                _worker.DoWork -= Worker_DoWork;
                _worker.RunWorkerCompleted -= Worker_RunWorkerCompleted;
            }

            _worker = new BackgroundWorker();
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.RunWorkerAsync(UriSource);
        }

        /// <summary>
        /// Load and decode the image on a background thread.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Really do want to catch all exceptions")]
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    Stream stream = client.OpenRead(e.Argument as Uri);

                    using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream))
                    {
                        IntPtr hbitmap = bitmap.GetHbitmap();
                        BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        bitmapSource.Freeze();
                        NativeMethods.DeleteObject(hbitmap);
                        e.Result = bitmapSource;
                    }
                }
            }
            catch
            {
                e.Result = null;
            }
        }

        /// <summary>
        /// Display the image.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _image.Source = e.Result as BitmapSource;
            IsImageLoading = false;
        }

        #endregion

        #region Stretch

        /// <summary>
        /// Gets or sets a value that describes how an System.Windows.Controls.Image
        /// should be stretched to fill the destination rectangle. This is a dependency
        /// property.
        /// </summary>
        /// <value>The value that describes how an System.Windows.Controls.Image should be stretched to fill the destination rectangle.</value>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// The identifier for the Stretch dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageLoader), new PropertyMetadata(Stretch.Uniform));

        #endregion

        #region Label

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// The identifier for the Label dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(ImageLoader), new PropertyMetadata(string.Empty));

        #endregion

        #region IsImageLoading

        /// <summary>
        /// Gets or sets a value indicating whether the image is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if the image is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsImageLoading
        {
            get { return (bool)GetValue(IsImageLoadingProperty); }
            set { SetValue(IsImageLoadingProperty, value); }
        }

        /// <summary>
        /// The identifier for the IsImageLoading dependency property.
        /// </summary>
        public static readonly DependencyProperty IsImageLoadingProperty = DependencyProperty.Register("IsImageLoading", typeof(bool), typeof(ImageLoader), new PropertyMetadata(false));

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _worker.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Gets the image displayed in this control.
        /// </summary>
        /// <value>The image displayed in this control.</value>
        internal BitmapImage BitmapImage
        {
            get
            {
                return _image.Source as BitmapImage;
            }
        }
    }

}
