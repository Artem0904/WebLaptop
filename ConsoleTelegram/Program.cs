// See https://aka.ms/new-console-template for more information

// See https://aka.ms/new-console-template for more information
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

Console.WriteLine("Hello, World!");

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string token = "7724052344:AAGHW_28SNYcFLCXfbwEiUaI5CQ2UagW9t0";

var client = new TelegramBotClient(token);

client.StartReceiving(Update, Error);

Console.ReadLine();
async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
{
    throw new NotImplementedException();
}

async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
{
    var message = update.Message;
    if (message == null || message.Type != MessageType.Text)
        return;
    if(!string.IsNullOrEmpty(message.Text))
    {
        Console.WriteLine($"{message.Chat.FirstName}   |    {message.Text}");
    }

    if (message.Text.ToLower().Contains("/start"))
    {
        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Рівненська ®️"},
            new KeyboardButton[] { "Волинська ®️" },
            new KeyboardButton[] { "Київська ®️" },
        }){ ResizeKeyboard = true };

        Message sentMessage = await client.SendTextMessageAsync(
        chatId: message.Chat.Id,
        text: "Оберіть область",
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: token);
    }



}