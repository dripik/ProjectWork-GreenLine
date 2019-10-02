using System;
using System.Collections.Generic;
using System.Text;

namespace GenBus2.Strumenti
{
    class modeljson
    {
        public int IdVeicolo { get; set; }
        public string StringaVeicolo { get; set; }
        public string TimeStamp { get; set; }
        public decimal Latitudine { get; set; }
        public decimal Longitudine { get; set; }
        public int Passeggeri { get; set; }
        public bool PorteAperte { get; set; }
    }
}
