using System;

namespace PROGECTWORK
{
    class GPSSensor:Sensor
    {
        public GPSSensor():base(){}

        public GPSSensor(int pin):base(pin){}   //sfrutto il costruttore ereditato

        public override void Read() //sovrascrivo la funzione ereditata
        {
            base.value=SensorLibrary.GetRandom(46,13)+"|"+SensorLibrary.GetRandom(45,12);   //latitudine e longitudine corrispondenti alla provincia di Pordenone
        }
    }
}