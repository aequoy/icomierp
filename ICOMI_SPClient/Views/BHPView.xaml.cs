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
using MahApps.Metro.Controls;
using ICOMI_CONTENT;
using ICOMI_SPClient.Utilities;

namespace ICOMI_SPClient.Views
{
    /// <summary>
    /// Logique d'interaction pour BHPView.xaml
    /// </summary>
    public partial class BHPView
    {
        public event RubriqueEventHandler ViewDocumentForRubrique;
        public BHPView()
        {
            InitializeComponent();
            DataContext = new BHPViewModel(Dispatcher);
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            var tileSender = sender as Tile;            
            var laRubrique = tileSender.DataContext as Rubrique;            
            if (laRubrique != null)
            {
                RubriqueRoutedEventArgs rrea = new RubriqueRoutedEventArgs(e, laRubrique);
                if(ViewDocumentForRubrique!=null)
                    ViewDocumentForRubrique(sender, rrea);
            }
        }
    }
}
