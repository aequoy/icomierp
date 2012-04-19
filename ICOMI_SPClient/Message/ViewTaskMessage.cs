using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_DOMAIN;

namespace ICOMI_SPClient.Message
{
    public class ViewTaskMessage : MessageBase
    {
        public IcomiTask TaskToShow { get; set; }
    }
}
