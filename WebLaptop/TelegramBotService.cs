using BusinessLogic;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebLaptop
{
    public class TelegramBotService
    {
        private readonly ITelegramBotClient client;
        private readonly IServiceScopeFactory _scopeFactory;

        public TelegramBotService(string token, IServiceScopeFactory scopeFactory)
        {
            client = new TelegramBotClient(token);
            _scopeFactory = scopeFactory;
        }

        public async Task StartReceiving(CancellationToken cancellationToken)
        {
            client.StartReceiving(Update, Error, cancellationToken: cancellationToken);
        }

        private async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
        }

        private async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message == null)
                return;

            if (!string.IsNullOrEmpty(message.Text))
            {
                Console.WriteLine($"{message.Chat.FirstName}   |    {message.Text}");
            }

            // Використання області (scope) для роботи з контекстом
            using (var scope = _scopeFactory.CreateScope())
            {
                var botUserService = scope.ServiceProvider.GetRequiredService<IBotUserService>();

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
                        

                        // Створення нового запису користувача в базі даних
                        await botUserService.Create(new BotUser()
                        {
                            Id = message.Chat.Id,
                            Name = message.Chat.FirstName,
                            UserName = message.Chat.Username,
                            PhoneNumber = phoneNumber
                        });
                    }
                    else
                    {
                        Console.WriteLine("Contact message received, but no phone number found.");
                    }
                }
            }
        }

        private async Task RequestPhoneNumberAsync(long chatId)
        {
            var replyMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Надіслати номер телефону") { RequestContact = true }})
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
    }

}
