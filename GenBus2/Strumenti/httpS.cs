using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GenBus2.Strumenti
{
    class httpS
    {
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
