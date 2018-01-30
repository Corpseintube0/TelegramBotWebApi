using System.Web.Mvc;

namespace TelegramBotWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "it's Flarsen's telegram bot.";
        }
    }
}