using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PROGECTWORK
{
    static class Sender //classe tcp-ip
    {
        static Ping ping = new Ping();

        public static bool isOnline(string destination)  //tenta connessione
        {
            if ((ping.Send(destination)).Status == IPStatus.Success) return true;
            return false;
        }

        public static async Task<string> Get(string destination)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(destination));
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static async Task<bool> Post(string destination, string data)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(new Uri(destination), new StringContent(data));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}