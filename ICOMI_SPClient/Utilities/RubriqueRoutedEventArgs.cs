using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ICOMI_CONTENT;

namespace ICOMI_SPClient.Utilities
{
    public delegate void RubriqueEventHandler(object sender, RubriqueRoutedEventArgs e);

    public class RubriqueRoutedEventArgs : RoutedEventArgs
    {
        public Rubrique LaRubrique { get; set; }

        public RubriqueRoutedEventArgs(RoutedEventArgs e, Rubrique laRubrique)
            : base(e.RoutedEvent)
        {
            LaRubrique = laRubrique;
        }
    }
    
}
