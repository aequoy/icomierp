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
using ICOMI_SPService;

namespace ICOMI_SPClient.Controls
{

    public class WebDocumentLoader : Control, IDisposable
    {
        /// <summary>
        /// The image control which displays the image.
        /// </summary>
        private byte[] _document;
        private WebBrowser _leBrowser;


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
                _leBrowser = GetTemplateChild("PART_Document") as WebBrowser;
                UpdateUriSource();
            }

            base.OnApplyTemplate();
        }

        #region UriSource

        /// <summary>
        /// Gets or sets the image UriSource.
        /// </summary>
        /// <value>The image UriSource.</value>
        public string UriSource
        {
            get { return (string)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        /// <summary>
        /// Backing store for UriSource.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.Register(
            "UriSource",
            typeof(string),
            typeof(WebDocumentLoader),
            new PropertyMetadata(null, (sender, e) => (sender as WebDocumentLoader).UpdateUriSource()));

        /// <summary>
        /// Updates the UriSource.
        /// </summary>
        private void UpdateUriSource()
        {
            if (_leBrowser == null)
            {
                return;
            }

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                _leBrowser.Opacity = 1;
                return;
            }
            if (UriSource == null)
            {
                _leBrowser.Source = null;
                return;
            }
            IsDocumentLoading = true;

            

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
                    var leDocument = e.Argument as string;
                    if (leDocument != null)
                    {
                        string savedFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), leDocument);
                        if (!File.Exists(savedFile))
                        {
                            Dispatcher.Invoke(
                                   System.Windows.Threading.DispatcherPriority.Normal,
                                   new Action(
                                     delegate()
                                     {
                                       GestionSharepoint gSP = new GestionSharepoint();
                                    string extension = string.Empty;
                                    this._document = gSP.GetFile(leDocument, "ICOMI_BPH", out extension);
                                    File.WriteAllBytes(savedFile, this._document);  
                                     }
                                 ));                            
                        }
                        e.Result = savedFile;
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
            this._leBrowser.Navigate(e.Result as string);
            IsDocumentLoading = false;
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
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(WebDocumentLoader), new PropertyMetadata(Stretch.Uniform));

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
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(WebDocumentLoader), new PropertyMetadata(string.Empty));

        #endregion

        #region IsDocumentLoading

        /// <summary>
        /// Gets or sets a value indicating whether the image is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if the image is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsDocumentLoading
        {
            get { return (bool)GetValue(IsDocumentLoadingProperty); }
            set { SetValue(IsDocumentLoadingProperty, value); }
        }

        /// <summary>
        /// The identifier for the IsImageLoading dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDocumentLoadingProperty = DependencyProperty.Register("IsDocumentLoading", typeof(bool), typeof(WebDocumentLoader), new PropertyMetadata(false));

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

    }
}
