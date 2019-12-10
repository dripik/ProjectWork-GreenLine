using System;

namespace PROGECTWORK
{
    class Bus:Veicolo   //ogni bus è un veicolo
    {
        string route;   //percorso/itinerario
        DoorSensor doors;   //apertura porte, per convenzione viene rilevato come fermata autobus
        CounterSensor passengers;   //passeggerri

        public Bus():base()    //costruttore di supporto fintanto che non si inizializza il bus
        {
            route = "";
            doors = new DoorSensor();
            passengers = new CounterSensor();
        }

        public Bus(string id, string path, int GPSPin, int doorsPin, int countersPin):base(id, GPSPin)   //si assegna l'identità al bus e si indica i contatti da usare per ciascun sensore
        {
            route = path;
            doors = new DoorSensor(doorsPin);
            passengers = new CounterSensor(countersPin);
        }

        public string ToJson() //restituisce il json del bus veicolo nell'ordine voluto
        {

            string[][] veicolo = base.Attributi();  //ottiene vettore elementi e valori del veicolo da cui eredita

            string[] b = new string[] { "StringaVeicolo", route };  //percorso
            string[] f = new string[] { "Passeggeri", passengers.ToString()};   //passeggeri
            string[] g = new string[] { "PorteAperte", doors.ToString()};   //porte

            return Json.ToJson(new string[][] {veicolo[0], b, veicolo[1], veicolo[2], veicolo[3], f, g});    //formattazione
        }

        public override void Rileva()  //si lascia alla classe da cui deriva a gestire la lettura dei dati della classe derivata per avere una gestione centralizzata ed evitare codice ridondante
        {
            Sensor[] sens = new Sensor[2];
            sens[0]=doors; 
            sens[1]=passengers;
            base.Read(sens);
        }

        
    }
}