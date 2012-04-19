using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using ICOMI_SPClient.Utilities;

namespace ICOMI_SPClient.Message
{
    public class LoginMessage:MessageBase
    {
        public SessionContext SecurityContext { get; set; }
    }
}
