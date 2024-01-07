using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot.Models.TelegramCommands
{
    public class StepCommand
    {

        [ReplyMenuHandler(true, "Сохранить сообщение")]
        public static async Task StartStop(ITelegramBotClient botClient, Update update)
        {
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(StepOne));
            string msg = "Введите данные: ";
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        public static async Task StepOne(ITelegramBotClient botClient, Update update)
        {
            update.GetCacheData<UserCache>().Data = update.Message.Text;
            string msg = "Данные сохранены, напишите любое сообщение, чтобы их увидеть. ";
            update.RegisterNextStep(new PRTelegramBot.Models.StepTelegram(StepTwo));
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }

        public static async Task StepTwo(ITelegramBotClient botClient, Update update)
        {
            string msg = $"{update.GetCacheData<UserCache>().Data}";
            await PRTelegramBot.Helpers.Message.Send(botClient , update, msg);
            update.ClearStepUser();
        }

        [ReplyMenuHandler(true, "Прогнорировать данные")]
        public static async Task IgnoreStep(ITelegramBotClient botClient, Update update)
        {
            string msg = "";
            if(update.HasStep())
            {
                msg = "Следующий шаг проигнорирован ";
                update.ClearStepUser();
            }
            else
            {
                msg = "Следующий шаг отсутствует ";
            }
            await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
    }
}
