using System;
using System.Collections.Generic;
using System.Text;

namespace KERRY.NMS.CORE.Enums
{
    public sealed class ShipmentStatus
    {

        private ShipmentStatus() { }

        public const string NEW = "NEW";
        public const string PIN = "PIN";
        public const string PUX = "PUX";
        public const string PUP = "PUP";
        public const string SIP = "SIP";
        public const string SIP_L = "SIP-L";
        public const string SOP_D = "SOP-D";
        public const string POD = "POD";
        public const string PODEX = "PODEX";
        public const string SOPR = "SOPR";
        public const string PODR = "PODR";
        public const string CANCEL = "Cancel";
    }
}
