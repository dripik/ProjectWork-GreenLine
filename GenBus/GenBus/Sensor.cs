using System;

namespace PROGECTWORK
{
    class Sensor
    {
        string _value;  //valore del sensore
        public string value
        {
            get{return _value;}
            set{_value=value;}
        }

        int _pin;   //contatto fisico da usare sul microprocessore arduino
        public int Pin
        {
            get{return _pin;}
            set{_pin=value;}
        }

        public Sensor()
        {
            value="";
            Pin=0;
        }

        public Sensor(int pin)  //inizializzazione con ttribuzione della porta dati relativa al sensore
        {
            value="";
            this.Pin=pin;
        }

        public virtual void Read(){}    //in base al tipo di sensore verra usata la libreria di lettura proprietaria del sensore richiamata dagli oggetti ereditati

        public override string ToString() //legge e restiruisce contemporaneamente dal sensore il valore letto come stringa
        {
            try {   //controllo non necessario in simulazione ma utile per gl'imprevisti tecnici del mondo reale
                Read();
            } catch (Exception) {
                return "error";
            }
            return System.Convert.ToString(this.value); //conversione non necessaria in quanto ho creato le classi ereditate e sono sicuro che Read() restituisca una stringa
        }
    }
}