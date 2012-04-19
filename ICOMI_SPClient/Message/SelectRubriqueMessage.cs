using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_CONTENT;

namespace ICOMI_SPClient.Message
{
    public class SelectRubriqueMessage : MessageBase
    {
       public Rubrique RubriqueSelected { get; set; }
    }
}
