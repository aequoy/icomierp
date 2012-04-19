using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace ICOMI_SPClient.Message
{
    public class ChangeStateMessage : MessageBase
    {
        public ChangeStateMessage():base()
        {
            StateToChange = StateType.Accueil;
            StateFrom = StateType.Accueil;
        }
        public StateType StateToChange { get; set; }
        public StateType StateFrom { get; set; }
    }

    public enum StateType
    {
        Accueil,Home, Search,BHP, AddBHPDocument,HACCP,MyTasks,Recettes,DocDivers,Login,Etablissement, Menu, Installation
    };

}
