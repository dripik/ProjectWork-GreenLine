using System;

namespace PROGECTWORK
{
    class CounterSensor:Sensor  //sensore che conta i passeggeri
    {
        public CounterSensor():base(){}

        public CounterSensor(int pin):base(pin){} //sfrutto il costruttore ereditato

        public override void Read() //sovrascrivo la lettura del dato ereditata
        {
            base.value=Convert.ToString(SensorLibrary.GetRandom(0,50)); //0<=persone<50
        }
    }
}