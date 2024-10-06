namespace WebLaptop
{
    public class TelegramBotHostedService : IHostedService
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TelegramBotHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var telegramBotService = scope.ServiceProvider.GetRequiredService<TelegramBotService>();
                await telegramBotService.StartReceiving(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
