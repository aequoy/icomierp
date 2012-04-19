using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMI_DOMAIN
{
    public class IcomiDocument
    {
        public DocumentMetadataDataContract MetaDatas { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
    }
}
