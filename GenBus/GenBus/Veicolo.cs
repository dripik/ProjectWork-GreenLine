using System;

namespace PROGECTWORK
{
    class Veicolo   //ogni veicolo ha i suoi dati letti e memorizzati su arduino
    {
        string ip;  //ip dell'ipotetico modulo tcp-ip collegato ad arduino ma assente nella simulazione in quanto viene gestito dai servizi microsoft
        string plate;   //targa univoca del veicolo
        GPSSensor loco; //latitudine e longitudine

        public Veicolo()    //costruttore di supporto fintanto che non si inizializza il veicolo
        {
            ip = "127.0.0.1"; //verrà sostituito con l'ip della macchina
            plate = "";
            loco = new GPSSensor();
        }

        public Veicolo(string id, int GPSPin)   //si assegna l'identità al veicolo e si indica i contatti da usare per ciascun sensore
        {
            ip = "127.0.0.1"; //verrà sostituito con l'ip della macchina
            plate = id;
            loco = new GPSSensor(GPSPin);
        }

        protected string[][] Attributi()
        {
            string[] a = new string[] { "IdVeicolo", plate };   //targa
            string[] b = new string[] { "TimeStamp", System.DateTime.Now.ToString() }; //data
            string[] c = new string[] { "Latitudine", (loco.ToString().Split('|'))[1] };    //lat del gps
            string[] d = new string[] { "Longitudine", (loco.ToString().Split('|'))[0] };   //long del gps

            return new string[][] {a, b, c, d};
        }

        protected void Read(Sensor[] sensori)   
        {
            foreach (Sensor sensore in sensori)
                sensore.Read();
        }

        public virtual void Rileva()
        {
            Sensor[] sens = new Sensor[1];
            sens[0] = loco;
            Read(sens);
        }
    }
}