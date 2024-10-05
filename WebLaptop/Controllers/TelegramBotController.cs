using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly TelegramBotService _telegramBotService;

        public TelegramBotController(TelegramBotService telegramBotService)
        {
            _telegramBotService = telegramBotService;
        }

        [HttpPost("start")]
        public IActionResult StartBot()
        {

            var cancellationTokenSource = new CancellationTokenSource();
            _telegramBotService.StartReceiving(cancellationTokenSource.Token);
            return Ok("Telegram Bot started");
        }
    }
}
