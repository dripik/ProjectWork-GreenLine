using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PROGECTWORK
{
    class Program   //viene simulato almeno un mezzo
    {
        const string destinationIP="http://192.168.1.7:4000/";  //url API
        const int gpsDelay=1000;    //pausa di misurazione di 1s

        static async Task Main(string[] args)
        {
            Bus[] bus = new Bus[4];
            bus[0]=new Bus("AA111AA", "linea 1", 1, 2, 3);
            bus[1]=new Bus("BB222BB", "linea 2", 1, 2, 3);
            bus[2]=new Bus("CC333CC", "linea 3", 1, 2, 3);
            bus[3]=new Bus("DD444DD", "linea 4", 1, 2, 3);

            while(true)
            {
                foreach (Bus x in bus)
                {
                    x.Rileva(); //lettura sensori
                    await SendBus(x.ToJson()); //invia la misurazione corrente come json
                }

                await SendCoda(); //cerca di svuotare la coda

                System.Threading.Thread.Sleep(gpsDelay);    //attendi il dovuto
            }
        }

        private static async Task SendBus(string message)  //invio json a API
        {
            Console.WriteLine("Tentativo di invio di: \n" + message);
            if (Sender.isOnline(destinationIP)) //tenta connessione
            {
                try { await Sender.Post(destinationIP, message); Console.WriteLine("Messaggio inviato"); }
                catch (Exception) { Coda.lista.Add(message); Console.WriteLine("Messaggio NON inviato per mancato feedback"); }  //in caso di problemi accoda la misurazione
            } else { Coda.lista.Add(message); Console.WriteLine("Messaggio NON inviato per mancata connessione"); }
        }

        private static async Task SendCoda()   //invio json accodati
        {
            Console.WriteLine("Tentativo di invio della coda di messaggi");
            if (Sender.isOnline(destinationIP)) //tenta connessione
            {
                foreach (string json in Coda.lista)
                {
                    Console.WriteLine("Tentativo di invio di: " + json);
                    try
                    {
                        await Sender.Post(destinationIP, json);
                        Coda.lista.Remove(json);
                        Console.WriteLine("Messaggio inviato e rimosso dalla coda");
                    } //json rimosso se arriva a destinazione
                    catch (Exception) { Console.WriteLine("Messaggio inviato"); }  //in caso di problemi il json non viene rimosso
                }
            }
            else Console.WriteLine("Messaggio NON inviato per mancanza di connessione"); 
        }

    }
}
