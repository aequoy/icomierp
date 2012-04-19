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
using MahApps.Metro;

namespace ICOMI_SPClient.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class HomeView
    {
        public event RoutedEventHandler AddBHP;


        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel(Dispatcher);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(AddBHP!=null)
                AddBHP(sender, e);
        }       
    }
}
