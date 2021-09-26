using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class BotClient
    {
        string _token = "1562086051:AAFqyQDk5qCDhk7bIm3tuwXa89a8YIXaDJw";
        TelegramBotClient bot;
        QueuedUpdateReceiver updateReceiver;
        HashSet<long> userIdList;
        AnikdotManager anikdotManager;

        public BotClient()
        {
            anikdotManager = new AnikdotManager();
        }

        public async void StartReceiving()
        {
            updateReceiver.StartReceiving();
            await foreach (Update update in updateReceiver.YieldUpdatesAsync())
            {
                if (update.Message is Message message)
                {
                    Chat chat = message.Chat;
                    if (!IsUserChatIdExist(chat.Id))
                    {
                        await bot.SendTextMessageAsync(chat, $"Salam {chat.Username}");
                    }
                    await bot.SendTextMessageAsync(chat, anikdotManager.GetAnecdot(AnicdotResources.Standart));
                }
            }
        }

        private bool IsUserChatIdExist(long userId)
        {
            return !userIdList.Add(userId);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is Message message)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Hello");
            }
        }

        public void Init()
        {
            bot = new TelegramBotClient(_token);
            updateReceiver = new QueuedUpdateReceiver(bot);
            userIdList = new HashSet<long>();
            StartReceiving();
        }
    }
}

