using System.Threading.Tasks;
using Telegram.Bot;
using System.Collections.Generic;
using TelegramBotWebAPI.Models.Commands;
using TelegramBotWebAPI.Domain;

namespace TelegramBotWebAPI.Models
{
    public static class Bot
    {
        private static TelegramBotClient _botClient;
        private static List<Command> _commandList;
        private static EFDbContext _dbContext = new EFDbContext();

        /// <summary>
        /// Список, содержащий Id пользователей, находящихся в режиме диалога с ботом.
        /// </summary>
        public static List<int> AwaitingUsers = new List<int>();

        /// <summary>
        /// Возвращает список команд бота.
        /// </summary>
        public static IReadOnlyCollection<Command> Commands
        {
            get
            {
                return _commandList.AsReadOnly();
            }
        }

        /// <summary>
        /// Возвращает экземпляр клиента для отправки ответа.
        /// </summary>
        public static async Task<TelegramBotClient> GetClient()
        {
            if (_botClient != null)
                return _botClient;

            //инициализация команд
            _commandList = new List<Command>();
            _commandList.Add(new HelloCommand());
            _commandList.Add(new AddCommand());
            _commandList.Add(new GetInfoCommand());
            _commandList.Add(new DeleteInfoCommand());

            _botClient = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, "api/message/update");
            //await _botClient.SetWebhookAsync(hook); //делать активным только когда в AppSettings есть url

            return _botClient;
        }
    }
}