// See https://aka.ms/new-console-template for more information

// See https://aka.ms/new-console-template for more information
using DataAccess.Repositories;
using DataAccess.Data.Entities;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.DependencyInjection;
using WebLaptop;
using DataAccess.Data;

Console.WriteLine("Hello, World!");

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string token = "7724052344:AAGHW_28SNYcFLCXfbwEiUaI5CQ2UagW9t0";
//TelegramBotDbContext context;
//IRepository<BotUser> usersRepo = new Repository<BotUser>(context: context);


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
    //await client.SendTextMessageAsync(
    //            chatId: message.Chat.Id,
    //            text: $"Млн",
    //            replyMarkup: new ReplyKeyboardRemove(),
    //            cancellationToken: token
    //        );
    if (message == null)
        return;

    if (!string.IsNullOrEmpty(message.Text))
    {
        Console.WriteLine($"{message.Chat.FirstName}   |    {message.Text}");
    }

    if (message.Type == MessageType.Text && message.Text.ToLower().Contains("/start"))
    {
            await RequestPhoneNumberAsync(message.Chat.Id);
    }
    else if (message.Type == MessageType.Contact)
    {
        var phoneNumber = message.Contact?.PhoneNumber;

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            Console.WriteLine($"Received phone number: {phoneNumber}");

            await client.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"Ваш номер телефону: {phoneNumber}",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: token
            );
            //await usersRepo.Insert(new BotUser() { Id = message.Chat.Id, Name = message.Chat.Username, UserName= message.Chat.FirstName, PhoneNumber=message.Contact.PhoneNumber});
            /*ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Рівненська ®️"},
            new KeyboardButton[] { "Волинська ®️" },
            new KeyboardButton[] { "Київська ®️" },
        })
        { ResizeKeyboard = true };

        Message sentMessage = await client.SendTextMessageAsync(
        chatId: message.Chat.Id,
        text: "Оберіть область",
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: token);*/
        }
        else
        {
            Console.WriteLine("Contact message received, but no phone number found.");
        }
    }
}

async Task RequestPhoneNumberAsync(long chatId)
{
    // Створюємо кнопку для надсилання контакту
    var replyMarkup = new ReplyKeyboardMarkup(new[]
    {
        new KeyboardButton("Надіслати номер телефону") { RequestContact = true }
    })
    {
        ResizeKeyboard = true,
        OneTimeKeyboard = true
    };

    await client.SendTextMessageAsync(
        chatId: chatId,
        text: "Будь ласка, надішліть свій номер телефону",
        replyMarkup: replyMarkup
    );
}