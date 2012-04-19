using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICOMI_SPClient.Message;

namespace ICOMI_SPClient.Utilities
{
    public class Menu
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImagePath { get; set; }
        public StateType Etat { get; set; }
    }
}
