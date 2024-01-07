using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Bot.Models.TelegramCommands
{
    public class CacheCommand
    {
        [ReplyMenuHandler(true, "Сохранить ID")]
        public static async Task SetCache(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<UserCache>().Id = update.GetChatId();
            string msg = $"Запомнил данные: {update.GetChatId()}";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        [ReplyMenuHandler(true, "Данные ID")]
        public static async Task GetCache(ITelegramBotClient botClient, Update update)
        {
            var cache = update.GetCacheData<UserCache>();
            string msg = "";
            if (cache.Id != null)
            {
                msg = $"Данные из кэша: {cache.Id}";
            }
            else
            {
                msg = "Данных нет";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        [ReplyMenuHandler(true, "Удалить данные ID")]
        public static async Task ClearCache(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<UserCache>().ClearData();
            string msg = "Данные удалены";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

    }
}
