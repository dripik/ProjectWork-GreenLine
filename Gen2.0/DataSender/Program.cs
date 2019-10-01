using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CSRedis;
using Newtonsoft.Json;


namespace DataSender
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //string c = await GetTextByGet("http://192.168.1.4:4000/get");
            //Console.WriteLine(c);
            // configure Redis
            var redis = new RedisClient("192.168.1.7");
            //var serializer = new JavaScriptSerializer();

      
            while (true)
            {
                // read from Redis queue
                string x = redis.BLPop(30, "sensors_data");
                Console.WriteLine(x);
                try
                {
                   
                   
                    bool insert = await PostextbyPost("http://192.168.1.7:4000/prova", x);
                    Console.WriteLine(insert);
                    // wait 1 second
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    
                }
                


                // send value to remote API
                // TODO...
            }
            
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
