using System;

namespace PROGECTWORK
{
    static class Json   //formattatore di json
    {
        public static string ToJson(string[][] tupla)  //parte implementata direttamente in classe in quanto non esistono classi ereditate
        {
            if (tupla.Length == 0) return "";    //no tuple = no json
            string jsn = "{";
            for (int i = 0; i < tupla.Length - 1; i++)  //per ogni tupla meno l'ultimo (evita il ',' prima del '}')
                jsn += "\"" + tupla[i][0] + "\": " + tupla[i][1] + ",";    //nome campo + valore campo
            return jsn+tupla[tupla.Length] + "}";
        }
    }
}