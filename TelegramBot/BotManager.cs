using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class BotManager
    {
        string _token = "1562086051:AAHG9LW7KcyQzZfoMhye-mNUZjRKXNdfbI8";
        TelegramBotClient bot;
        QueuedUpdateReceiver updateReceiver;
        HashSet<long> userIdList;
        AnikdotManager anikdotManager;
        InlineKeyboardMarkup inlineKeyboardMarkup;

        public BotManager()
        {
            anikdotManager = new AnikdotManager();
            bot = new TelegramBotClient(_token);
            updateReceiver = new QueuedUpdateReceiver(bot);
            userIdList = new HashSet<long>();
            inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton> {
                    AnicdotType.Standart.ToString(),
                    AnicdotType.Cenzored.ToString()
                });

        }

        public async void StartReceivingAsync()
        {
            updateReceiver.StartReceiving();
            Console.WriteLine("Start Receiving");
            await foreach (Update update in updateReceiver.YieldUpdatesAsync())
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        ProcessMessageAsync(update.Message);
                        break;
                    case UpdateType.CallbackQuery:
                        ProcessCallbackQueryAsync(update.CallbackQuery);
                        break;
                    default:
                        throw new NotImplementedException();
                };

            }
        }

        private void ProcessMessageAsync(Message message)
        {
            string messageText = "Вибери анегдот:";
            Chat chat = message.Chat;
            if (!IsUserChatIdExist(chat.Id))
            {
                messageText = $"Здаров {chat.Username} \n" + messageText;
            }
            bot.SendTextMessageAsync(chat, messageText, replyMarkup: inlineKeyboardMarkup);
        }

        private async void ProcessCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            long chatId = callbackQuery.Message.Chat.Id;
            AnicdotType anicdotType = (AnicdotType)Enum.Parse(typeof(AnicdotType), callbackQuery.Data);
            var anecdot = anikdotManager.GetAnecdotAsync(anicdotType);
            await bot.EditMessageReplyMarkupAsync(chatId, callbackQuery.Message.MessageId);
            var message = await bot.SendTextMessageAsync(chatId, "Шукаем анегдот...");
            await bot.EditMessageTextAsync(chatId, message.MessageId, await anecdot, replyMarkup: inlineKeyboardMarkup);
        }

        private bool IsUserChatIdExist(long userId)
        {
            return !userIdList.Add(userId);
        }
    }
}

