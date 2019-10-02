using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader.Sensors;
using CSRedis;
using System.Net.Http;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace DataReader
{
    class Program
    {
        static async Task Main(string[] args)
        {

 
            // init sensors
            List<ISensor> sensors = new List<ISensor>
            {
                new VirtualTemperatureSensor()
            };

            // configure Redis
            var redis = new RedisClient("192.168.1.5");
            // config ping
            var ping = new Ping();

            while (true)
            {
                foreach (ISensor sensor in sensors)
                {
                    // get current sensor value
                    var data = sensor.ToJson();
                    Console.WriteLine(data);
                    PingReply pingReply = ping.Send("192.168.1.5");

                    if (pingReply.Status == IPStatus.Success)
                    {
                        string x = redis.LPop("sensors_data");
                        while (x != null)
                        {
                            try
                            {
                                await PostextbyPost("http://192.168.1.5:4000/prova", x);
                                Console.WriteLine("dati da coda redis" + x);
                                
                            }
                            catch (Exception e )
                            {
                                Console.WriteLine(e.Message + " invio non riuscito da redispop di :" + x);

                            }
                            x = redis.LPop("sensors_data");
                        }
                        
                      
                        try
                        {
                            bool insert = await PostextbyPost("http://192.168.1.5:4000/", data);
                            Console.WriteLine(insert);
                            // wait 1 second
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

        public static bool ProvaRedis()
        {
            var redis = new RedisClient("192.168.1.5");
            string x = redis.BLPop(30, "sensors_data");
            if (x != null) { return true; }
            else { return false; }
        }

        public static async Task<string> GetTextByGet(string posturi)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(posturi));
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
        public static async Task<bool> PostextbyPost(string posturi, string data)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(new Uri(posturi), new StringContent(data));
            if (response.IsSuccessStatusCode)
            {
                return true; // Handle success
            }
            else
            {
                return false; // Handle failure
            }

        }
    }
}

