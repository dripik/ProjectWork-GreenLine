using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataReader.Sensors
{
    class VirtualTemperatureSensor : ITemperatureSensor, ISensor
    {
        Random random = new Random();
        
        public void SetTemperature(decimal temperature)
        { }

        public decimal GetTemperature()
        {
            
            return new decimal((double)random.NextDouble() * (12.8 - 12.5) + 12.5);
        }
        public decimal GetTemperature2()
        {
            
            return new decimal((double)random.NextDouble() * (46 - 45.85) + 45.85);
        }

        public string ToJson()
        {
            return "{\"altitudine\":\"24\"" + ",\"description\":\"Pordenone\"" + ",\"longitude\":\"" + GetTemperature() + "\"" + ",\"latitudine\":\"" + GetTemperature2() + "\"}";
        }
    }
}
