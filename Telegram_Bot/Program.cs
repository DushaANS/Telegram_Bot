using PRTelegramBot.Core;

const string EXIT_COMMAND = "exit";

var telegram = new PRBot(option =>
{
    option.Token = "6831284932:AAFXYDmUDTz5WTJNwYrV_21I5CKEQk4uHD0"; //Токен бота
    option.WhiteListUsers = new List<long>() { };                    //Списки тех, кто может пользоваться                
    option.Admins = new List<long>() { };                            //Назначение даминистраторов
    option.BotId = 0;                                                //Индекс бота
});

telegram.OnLogCommon += Telegram_OnLogCommon;
telegram.OnLogError += Telegram_OnLogError;

await telegram.Start();

void Telegram_OnLogCommon(string msg, PRBot.TelegramEvents typeEvent, ConsoleColor color) //Метод на вывод команды о подключении
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    string message = $"{DateTime.Now}:{msg}";
    Console.WriteLine(message);
    Console.ResetColor();
}

void Telegram_OnLogError(Exception ex, long? id) //Метод о выводе ошибок
{
    Console.ForegroundColor = ConsoleColor.Red;
    string errorMsg = $"{DateTime.Now}:{ex}";
    Console.WriteLine(errorMsg);
    Console.ResetColor();
}

while (true) //Цикл от закрытия бота
{
    var result = Console.ReadLine();
    if(result.ToLower() == EXIT_COMMAND)
    {
        Environment.Exit(0);
    }
}


