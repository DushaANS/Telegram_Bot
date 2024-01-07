using CalendarPicker.CalendarControl;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Theader = PRTelegramBot.Models.Enums.THeader;

namespace Telegram_Bot.Models.TelegramCommands
{
    public class Calendar
    {
        public static DateTimeFormatInfo dfti = CultureInfo.GetCultureInfo("ru-Ru", false).DateTimeFormat;

        [ReplyMenuHandler(true, "Календарь")]

        public static async Task CalendarPick(ITelegramBotClient botClient, Update update)
        {
            var calendar = Markup.Calendar(DateTime.Now, dfti);
            var option = new OptionMessage();
            option.MenuInlineKeyboardMarkup = calendar;
            await PRTelegramBot.Helpers.Message.Send(botClient, update.GetChatId(), "Укажите дату: ", option);
        }

        [InlineCallbackHandler<Theader>(Theader.YearMonthPicker)]
        public static async Task PickYearMonth(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = Markup.PickMonthYear(command.Data.Date, dfti);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = date;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        [InlineCallbackHandler<Theader>(Theader.PickMonth)] // выбор месяца
        public static async Task PickMonth(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = Markup.PickMonth(command.Data.Date, dfti);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = date;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        [InlineCallbackHandler<Theader>(Theader.PickYear)] // выбор конкретного года
        public static async Task PickYear(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = Markup.PickYear(command.Data.Date, dfti);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = date;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        [InlineCallbackHandler<Theader>(Theader.ChangeTo)] // возварщение главного меню календаря
        public static async Task ChangeHandler(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = Markup.Calendar(command.Data.Date, dfti);
                var option = new OptionMessage();
                option.MenuInlineKeyboardMarkup = date;
                await PRTelegramBot.Helpers.Message.EditInline(botClient, update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, option);
            }
        }

        [InlineCallbackHandler<Theader>(Theader.PickDate)]
        public static async Task PickDate(ITelegramBotClient botClient, Update update)
        {
            var command = InlineCallback<CallendarTCommand>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
            if (command != null)
            {
                var date = command.Data.Date;
                await PRTelegramBot.Helpers.Message.Edit(botClient, update, $"Дата:{date}");
            }

        }






    }
}
