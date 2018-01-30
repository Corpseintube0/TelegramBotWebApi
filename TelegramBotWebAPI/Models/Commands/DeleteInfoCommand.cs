using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebAPI.Domain;

namespace TelegramBotWebAPI.Models.Commands
{
    /// <summary>
    /// Команда удаления информации о клиенте.
    /// </summary>
    public class DeleteInfoCommand : Command
    {
        public override string Name
        {
            get { return "delete_info"; }
        }

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var context = new EFDbContext();
            var delUsers = context.Users.Where(r => r.TelegramUserId == message.From.Id);

            if (delUsers.ToArray().Length < 1)
                client.SendTextMessageAsync(chatId, "There is no registered users from this telegram account.");
            else
            {
                context.Users.RemoveRange(delUsers);
                context.SaveChanges();
                client.SendTextMessageAsync(chatId, "Registration was cancelled and user info was deleted.");
            }
        }
    }
}