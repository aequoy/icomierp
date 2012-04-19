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
using ICOMI_CONTENT;
using ICOMI_SPClient.ViewModels;
using MahApps.Metro.Controls;
using ICOMI_DOMAIN;
using System.Configuration;
using System.IO;
using System.Net;
using ICOMI_SPService;

namespace ICOMI_SPClient.Views
{
    /// <summary>
    /// Logique d'interaction pour BHPDocumentView.xaml
    /// </summary>
    public partial class BHPDocumentView 
    {

        public BHPDocumentView()
        {
            InitializeComponent();
            DataContext = new BHPDocumentViewModel(Dispatcher);
            
        }
        
    }
}
