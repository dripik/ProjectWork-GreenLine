using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenBus.Strumenti
{
    class GenCord : modeljson
    {
        Random random = new Random();


        public decimal Long()
        {

            return new decimal((double)random.NextDouble() * (46 - 45.85) + 45.85);
        }
        public decimal Lat()
        {

            return new decimal((double)random.NextDouble() * (12.8 - 12.5) + 12.5);
        }
        public int Pass()
        {
            return random.Next(0, 50);
        }

        public string Generatore()
        {
            modeljson json = new modeljson()
            {
                IdVeicolo = 5,
                StringaVeicolo = "Pordenone",
                TimeStamp = DateTime.Now.ToString(),
                Latitudine = Long(),
                Longitudine = Lat(),
                Passeggeri = Pass(),
                PorteAperte = true

            };

            return JsonConvert.SerializeObject(json);
        }
    }
}
