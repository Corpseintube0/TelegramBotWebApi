using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading.Tasks;

namespace TelegramBotWebAPI.Models.Commands
{
    public abstract class Command
    {
        /// <summary>
        /// Имя (текстовое обозначение) команды.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Метод, обеспечивающий логику исполнения команды.
        /// </summary>
        /// <param name="message">Сообщение от пользователя.</param>
        /// <param name="client">Экземпляр TelegramBotClient, исполняющий команду.</param>
        public abstract void Execute(Message message, TelegramBotClient client);
    }
}