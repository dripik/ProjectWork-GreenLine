using System;
using System.Threading.Tasks;
using GenBus.Strumenti;
using CSRedis;
using System.Net.NetworkInformation;
using static GenBus.Strumenti.httpS;
using System.IO;
using Newtonsoft.Json;

namespace GenBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string IpRedis = System.Configuration.ConfigurationManager.AppSettings["IpNode"];
            string IpNode = System.Configuration.ConfigurationManager.AppSettings["IpRedis"];
            string Filename = System.Configuration.ConfigurationManager.AppSettings["Bus"];
            int IdVeicolo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IdBus"]);
            // configure Redis
            RedisClient redis = new RedisClient(IpRedis);
            // config ping
            Ping ping = new Ping();

            while (true)
            {
                using (StreamReader r = new StreamReader("../../Strumenti/"+Filename))
                {
                    string file = r.ReadToEnd();
                    dynamic array = JsonConvert.DeserializeObject(file);
                    foreach (var item in array.geometry.coordinates)
                    {
                        modeljson json = new modeljson()
                        {
                            IdVeicolo = IdVeicolo,
                            StringaVeicolo = Filename.Replace(".txt", ""),
                            TimeStamp = DateTime.Now.ToString(),
                            Latitudine = item[1],
                            Longitudine = item[0],
                            Passeggeri = new GenCord().Pass(),
                            PorteAperte = true

                        };



                        var data = JsonConvert.SerializeObject(json);
                        Console.WriteLine(data);
                        PingReply pingReply = ping.Send(IpNode);

                        if (pingReply.Status == IPStatus.Success)
                        {
                            string x = redis.LPop("sensors_data");
                            while (x != null)
                            {           //questa parte e relativa perchè nel momento che al ping risponde dovrebbe funzionare anche l'api
                                try
                                {
                                    await PostextbyPost("http://" + IpNode + ":4000/", x);
                                    Console.WriteLine("dati da coda redis" + x);

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message + " invio non riuscito da redispop di :" + x);

                                }
                                x = redis.LPop("sensors_data");
                            }


                            try
                            {
                                bool insert = await PostextbyPost("http://" + IpNode + ":4000/", data);
                                Console.WriteLine(insert);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                redis.LPush("sensors_data", data);
                                Console.WriteLine("Scritto in redis");
                            }


                        }
                        else
                        {

                            Console.WriteLine("host non raggiungibile");
                            // push to redis queue
                            redis.RPush("sensors_data", data);
                            Console.WriteLine("pusho su redis dentro catch");

                        }


                        // wait 1 second
                        System.Threading.Thread.Sleep(1000);


                       
                    }

                }

            }
        }
    }
}
