using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Extentions
    {
        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            List<T> ObjList = collection.ToList();
            int rndNum = new Random().Next(0, ObjList.Count - 1);
            return ObjList[rndNum];
        }
    }
}
