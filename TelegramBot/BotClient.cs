using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public class BotClient
    {
        string _token = "1562086051:AAFqyQDk5qCDhk7bIm3tuwXa89a8YIXaDJw";
        TelegramBotClient _telegramBotClient;
        
        public BotClient()
        {
            _telegramBotClient = new TelegramBotClient(_token);
            _telegramBotClient.StartReceiving();
            Init();
        }

        private void Init()
        {
            _telegramBotClient.OnMessage += SendMessage;
        }

        public void SendMessage(object o, MessageEventArgs messageEventArgs)
        {
            var chatId = messageEventArgs.Message.Chat.Id;
            _telegramBotClient.SendTextMessageAsync(chatId, $"{DateTime.Now}");
        }
    }
}
