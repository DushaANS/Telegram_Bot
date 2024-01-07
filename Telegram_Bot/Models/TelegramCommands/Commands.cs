using PRTelegramBot.Attributes;
using PRTelegramBot.Helpers.TG;
using PRTelegramBot.Models;
using PRTelegramBot.Models.CallbackCommands;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Models.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram_Bot.Models;

namespace Telegram_Bot.Models.TelegramCommands
{
    public class Commands
    {

        [ReplyMenuHandler(true, "/start")]
        public static async Task Example(ITelegramBotClient botClient, Update update) //Метод для обработки команд
        {
            var message = "Напишите команду меню";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }

        [ReplyMenuHandler(true, "Меню")]
        public static async Task Menu(ITelegramBotClient botClient, Update update) //Меню
        {
            var message = "Вы вызвали меню, умничка!";

            var menuList = new List<KeyboardButton>();
            var menuListString = new List<string>();
            menuList.Add(new KeyboardButton("Календарь"));
            menuList.Add(new KeyboardButton("Список"));
            menuList.Add(new KeyboardButton("Сохранить сообщение"));
            menuList.Add(new KeyboardButton("Проигнорировать сообщение"));
            menuList.Add(new KeyboardButton("Сохранить ID"));
            menuList.Add(new KeyboardButton("Данные ID"));
            menuList.Add(new KeyboardButton("Удалить данные ID"));
            menuList.Add(new KeyboardButton("Вывод страниц"));

            var menu = MenuGenerator.ReplyKeyboard(1, menuList);

            var option = new OptionMessage();
            option.MenuReplyKeyboardMarkup = menu;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, option);
        }

        [ReplyMenuHandler(true, "get")]
        public static async Task Get(ITelegramBotClient botClient, Update update) //Метод для обработки команд
        {
            var message = "Пример /get и /get_1";
            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        }


        [SlashHandler("/get")]
        public static async Task GetSlash(ITelegramBotClient botClient, Update update) //Метод для обработки команд
        {
            if (update.Message.Text.Contains("_"))
            {
                var spl = update.Message.Text.Split('_');
                if (spl.Length > 1)
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Команда /get и параметр {spl[1]}");
                }
                else
                {
                    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда /get");
                }
            }
            else
            {
                var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, "Команда");
            }

        }

        [ReplyMenuHandler(true, "Список")]
        public static async Task Inline(ITelegramBotClient botClient, Update update) //Метод для обработки команд
        {
            var message = "Вы вывели список";

            List<IInlineContent> menu = new List<IInlineContent>();

            //var exampleOne = new InlineCallback("Пример 1", CustomTHeader.ExampleOne);
            var url = new InlineURL("VK", "https://vk.com/dushka_ada");
            var webapp = new InlineWebApp("WebApp", "https://github.com/DushaANS");
            //var exampleTwo = new InlineCallback<EntityTCommand<long>>("Название кнопки 2", CustomTHeader.ExampleTwo, new EntityTCommand<long>(5));
            //var exampleThree = new InlineCallback<EntityTCommand<long>>("Название кнопки 3", CustomTHeader.ExampleThree, new EntityTCommand<long>(3));


            //menu.Add(exampleOne);
            menu.Add(url);
            menu.Add(webapp);
            //menu.Add(exampleTwo);
            //menu.Add(exampleThree);

            var menuItems = MenuGenerator.InlineKeyboard(1, menu);

            var optins = new OptionMessage();
            optins.MenuInlineKeyboardMarkup = menuItems;

            var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message, optins);
        }

        //[InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleOne)]
        //public static async Task InlineExample(ITelegramBotClient botClient, Update update) //Метод для обработки команд 1
        //{
        //    var message = "Пример ExampleOne";

        //    var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        //}

        //[InlineCallbackHandler<CustomTHeader>(CustomTHeader.ExampleTwo, CustomTHeader.ExampleThree)]
        //public static async Task InlineExampleTwoThree(ITelegramBotClient botClient, Update update) //Метод для обработки команд 1 2
        //{
        //    var command = InlineCallback<EntityTCommand<long>>.GetCommandByCallbackOrNull(update.CallbackQuery.Data);
        //    if (command != null)
        //    {
        //        var message = $"Данные {command.Data.EntityId}";
        //        var sendMessage = await PRTelegramBot.Helpers.Message.Send(botClient, update, message);
        //    }

        //}

    }
}
