using System;

namespace PROGECTWORK
{
    class DoorSensor:Sensor
    {
        public DoorSensor():base(){}

        public DoorSensor(int pin):base(pin){}  //sfrutto il costruttore ereditato

        public override void Read() //sovrascrivo la funzione ereditata
        {
            if(SensorLibrary.GetRandom(0,10)<5) //50% di probabilità che almeno una porta è aperta
                base.value="open";
            else
                base.value="close";
        }
    }
}