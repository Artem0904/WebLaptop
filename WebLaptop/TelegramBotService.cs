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
        //private readonly IRepository<BotUser> botUserRepo;
        private readonly IBotUserService botUserService;
        public TelegramBotService(string token, IBotUserService botUserService/*, IRepository<BotUser> botUserRepo*/)
        {
            client = new TelegramBotClient(token);
            this.botUserService = botUserService;
            //this.botUserRepo = botUserRepo;
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
                    //await botUserRepo.Insert(new BotUser() { Id = message.Chat.Id, Name = message.Chat.Username, UserName= message.Chat.FirstName, PhoneNumber=message.Contact.PhoneNumber});

                    botUserService.Create(new BotUser()
                    {
                        Id = message.Chat.Id,
                        Name = message.Chat.Username,
                        UserName = message.Chat.FirstName,
                        PhoneNumber = phoneNumber
                    });
                }
                else
                {
                    Console.WriteLine("Contact message received, but no phone number found.");
                }
            }

        }

        private async Task RequestPhoneNumberAsync(long chatId)
        {
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
    }
}
