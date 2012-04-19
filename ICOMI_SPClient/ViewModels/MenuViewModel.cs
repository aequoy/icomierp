using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using ICOMI_SPClient.Utilities;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Message;

namespace ICOMI_SPClient.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly Dispatcher _dispatcher;
        public ObservableCollection<ICOMI_SPClient.Utilities.Menu> Menus { get; private set; }

        private bool _IsConnected = false;
        public bool IsConnected
        {
            get { return this._IsConnected; }
            set
            {
                this._IsConnected = value;
                if (this._IsConnected)
                {
                    this.Menus.Add(new Utilities.Menu() { Title = "Mon espace", Etat = Message.StateType.Home, SubTitle = "Accéder à votre espace", ImagePath = "/ICOMI_SPClient;component/Resources/monespace.JPG" });
                    Menus.Add(new Utilities.Menu() { Title = "Mes tâches", Etat = Message.StateType.MyTasks, SubTitle = "Accéder à mes tâches", ImagePath = "/ICOMI_SPClient;component/Resources/taches.JPG" });
                }
                else
                {
                    this.Menus.Remove(this.Menus.Where(m => m.Title.Equals("Mon espace")).Select(m => m).FirstOrDefault());
                    this.Menus.Remove(this.Menus.Where(m => m.Title.Equals("Mes tâches")).Select(m => m).FirstOrDefault());                    
                }
                RaisePropertyChanged("IsConnected");
            }
        }
        
        public MenuViewModel(Dispatcher dispatcher)
         {
             _dispatcher = dispatcher;
             Menus = new ObservableCollection<Utilities.Menu>();
             Menus.Add(new Utilities.Menu() { Title = "Bonnes Pratiques d'Hygiène", Etat= Message.StateType.BHP, SubTitle = "BPH", ImagePath = "/ICOMI_SPClient;component/Resources/hygiene.JPG" });
             Menus.Add(new Utilities.Menu() { Title = "HACCP", Etat = Message.StateType.HACCP, SubTitle = "Accéder aux formulaires qualité", ImagePath = "/ICOMI_SPClient;component/Resources/haccp.JPG" });            
             Menus.Add(new Utilities.Menu() { Title = "Recettes", Etat = Message.StateType.Recettes, SubTitle = "Accéder aux recettes", ImagePath = "/ICOMI_SPClient;component/Resources/recette.JPG" });
             Menus.Add(new Utilities.Menu() { Title = "Documents divers", Etat = Message.StateType.DocDivers, SubTitle = "Accéder aux documents divers", ImagePath = "/ICOMI_SPClient;component/Resources/documents divers.JPG" });
             ChangeMenuCommand = new GenericCommand<Menu>(ExecuteChangeMenuCommand, CanExecuteChangeMenuCommand);
             
         }

        public ICommand ChangeMenuCommand
        {
            get;
            set;
        }


        /// <summary>
        /// Valide l'édition en cours et retourne à l'écran principal
        /// </summary>
        /// <param name="notUsed"></param>
        protected virtual void ExecuteChangeMenuCommand(Menu param)
        {
            if (param != null)
            {
                Messenger.Default.Send<ChangeStateMessage>(new ChangeStateMessage() { StateToChange = param.Etat });
            }
        }

        /// <summary>
        /// Indique si l'objet est valide
        /// </summary>
        private bool CanExecuteChangeMenuCommand(Menu param)
        {
            return true;
        }
    }
}
