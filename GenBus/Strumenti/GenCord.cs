using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenBus.Strumenti
{
    class GenCord : modeljson
    {
        Random random = new Random();
        //commentata per aggiunta di lettura da file per le cordinate

        //public decimal Long()
        //{

        //    return new decimal((double)random.NextDouble() * (46 - 45.85) + 45.85);
        //}
        //public decimal Lat()
        //{

        //    return new decimal((double)random.NextDouble() * (12.8 - 12.5) + 12.5);
        //}
        public int Pass()
        {
            return random.Next(0, 70);
        }

        //public string Generatore()
        //{
        //    using (StreamReader r = new StreamReader("C:\\1.txt"))
        //    {
        //        string file = r.ReadToEnd();
        //        dynamic array = JsonConvert.DeserializeObject(file);
        //        foreach (var item in array.coordinates)
        //        {
        //            modeljson json = new modeljson()
        //            {
        //                IdVeicolo = 5,
        //                StringaVeicolo = "Pordenone",
        //                TimeStamp = DateTime.Now.ToString(),
        //                Latitudine = item[1],
        //                Longitudine = item[0],
        //                Passeggeri = Pass(),
        //                PorteAperte = true

        //            };

        //            return JsonConvert.SerializeObject(json);
                    
        //        }
        //        return "done";
        //    }

          
        //}
    }
}
