using System;

namespace PROGECTWORK
{
    static class SensorLibrary  //ogni sensore avrebbe la sua libreria proprietaria, questà è destinata alla simulazione pseudocasuale entro limiti dati
    {
        static Random r = new Random();

        public static int GetRandom(int min, int max)
        {
            return r.Next(min, max);
        }
    }
}