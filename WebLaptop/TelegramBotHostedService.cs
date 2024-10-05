namespace WebLaptop
{
    public class TelegramBotHostedService : IHostedService
    {
        private readonly TelegramBotService telegramBotService;
        private CancellationTokenSource cancellationTokenSource;

        public TelegramBotHostedService(TelegramBotService telegramBotService)
        {
            this.telegramBotService = telegramBotService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            telegramBotService.StartReceiving(cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
