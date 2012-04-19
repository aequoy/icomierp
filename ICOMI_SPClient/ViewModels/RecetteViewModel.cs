using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Threading;
using ICOMI_SPService;
using System.Collections.ObjectModel;
using ICOMI_DOMAIN;

namespace ICOMI_SPClient.ViewModels
{
    public class RecetteViewModel : ViewModelBase
    {
        private Dispatcher _dispatcher;
        public ObservableCollection<Recette> LesRecettes { get; set; }
        #region CTOR
        public RecetteViewModel(Dispatcher dispatcher)
         {
             _dispatcher = dispatcher;
             GestionSharepoint gSP = new GestionSharepoint();
             LesRecettes = new ObservableCollection<Recette>(gSP.GetListRecette());

         }
        #endregion
    }
}
