using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    class VirtualTemperatureSensor : ITemperatureSensor, ISensor
    {
        public void SetTemperature(decimal temperature)
        { }

        public decimal GetTemperature()
        {
            Random random = new Random();
            return new decimal((double)random.Next(10, 500) / 10);
        }

        public string ToJson()
        {
            return "{\"temperature\":\"" + GetTemperature() + "\"}";
        }
    }
}
