using System;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Oo();
            var a = new BotClient();
            Console.Read();
        }

        public static async Task Oo()
        {
            while (false)
            {
                Thread.Sleep(3000);
                Console.WriteLine();
            }
        }
    }
}
