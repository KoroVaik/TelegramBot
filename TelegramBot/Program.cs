using System;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var a = new BotManager();
            a.StartReceivingAsync();
            Console.ReadLine();
            Console.WriteLine("Exit");
        }
    }
}
