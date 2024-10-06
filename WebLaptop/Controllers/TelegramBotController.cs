using BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebLaptop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly TelegramBotService _telegramBotService;
        private readonly IBotUserService botUserService;

        public TelegramBotController(TelegramBotService telegramBotService, IBotUserService botUserService)
        {
            _telegramBotService = telegramBotService;
            this.botUserService = botUserService;
        }

        [HttpPost("start")]
        public IActionResult StartBot()
        {

            var cancellationTokenSource = new CancellationTokenSource();
            _telegramBotService.StartReceiving(cancellationTokenSource.Token);
            return Ok("Telegram Bot started");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            botUserService.Delete(id);
            return Ok();
        }
    }
}
