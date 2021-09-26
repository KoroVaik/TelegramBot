using System;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var a = new BotClient();
            a.Init();
            Console.Read();
        }
    }
}
