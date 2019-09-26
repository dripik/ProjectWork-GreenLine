using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader.Sensors;
using CSRedis;

namespace DataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // init sensors
            List<ISensor> sensors = new List<ISensor>
            {
                new VirtualTemperatureSensor()
            };

            // configure Redis
            var redis = new RedisClient("192.168.1.4");

            while (true)
            {
                foreach (ISensor sensor in sensors)
                {
                    // get current sensor value
                    var data = sensor.ToJson();
                    Console.WriteLine(data);

                    // push to redis queue
                    redis.LPush("sensors_data", data);

                    // wait 1 second
                    System.Threading.Thread.Sleep(200);

                }

            }
        }
    }
}
