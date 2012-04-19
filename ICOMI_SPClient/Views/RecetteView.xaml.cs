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
using ICOMI_SPClient.ViewModels;

namespace ICOMI_SPClient.Views
{
    /// <summary>
    /// Logique d'interaction pour RecetteView.xaml
    /// </summary>
    public partial class RecetteView
    {
        public RecetteView()
        {
            InitializeComponent();
            this.DataContext = new RecetteViewModel(Dispatcher);
        }
    }
}
