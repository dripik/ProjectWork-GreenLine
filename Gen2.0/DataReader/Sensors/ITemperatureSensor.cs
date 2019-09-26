using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    interface ITemperatureSensor
    {
        void SetTemperature(decimal temperature);
        decimal GetTemperature();
    }
}
