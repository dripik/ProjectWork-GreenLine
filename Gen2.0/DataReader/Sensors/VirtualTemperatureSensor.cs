using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace DataReader.Sensors
{
    class VirtualTemperatureSensor : Json, ITemperatureSensor, ISensor
    {
        Random random = new Random();
        
        public void SetTemperature(decimal temperature)
        { }

        public decimal Long()
        {

            return new decimal((double)random.NextDouble() * (12.8 - 12.5) + 12.5);
        }
        public decimal Lat()
        {

            return new decimal((double)random.NextDouble() * (46 - 45.85) + 45.85);
        }

        public string ToJson()
        {
            Json json = new Json()
            {
                IdVeicolo = 1,
                StringaVeicolo = "pordenone",
                TimeStamp = DateTime.Now.ToString(),
                Latitudine = Long(),
                Longitudine = Lat(),
                Passeggeri = 10,
                PorteAperte = true

            };

            return JsonConvert.SerializeObject(json);
        }
    }
   
}
