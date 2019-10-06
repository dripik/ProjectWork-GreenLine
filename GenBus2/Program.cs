using System;
using System.Threading.Tasks;
using GenBus2.Strumenti;
using CSRedis;
using System.Net.NetworkInformation;
using static GenBus2.Strumenti.httpS;

namespace GenBus2
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var bus = new GenCord();
            // configure Redis
            RedisClient redis = new RedisClient("192.168.1.5");
            // config ping
            Ping ping = new Ping();

            while (true)
            {

                var data = bus.Generatore();
                Console.WriteLine(data);
                PingReply pingReply = ping.Send("192.168.1.5");

                if (pingReply.Status == IPStatus.Success)
                {
                    string x = redis.LPop("sensors_data");
                    while (x != null)
                    {           //questa parte e relativa perchè nel momento che al ping risponde dovrebbe funzionare anche l'api
                        try
                        {
                            await PostextbyPost("http://192.168.1.5:4000/", x);
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
                        bool insert = await PostextbyPost("http://192.168.1.5:4000/", data);
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
                System.Threading.Thread.Sleep(500);



            }

        }
    }
}
