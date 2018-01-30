using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebAPI.Domain;

namespace TelegramBotWebAPI.Models.Commands
{
    /// <summary>
    /// Команда просмотра данных о клиенте.
    /// </summary>
    public class GetInfoCommand : Command
    {
        public override string Name
        {
            get { return "get_info"; }
        }

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var context = new EFDbContext();
            var displayedUsers = context.Users.Where(r => r.TelegramUserId == message.From.Id);

            string textResult = "";
            if (displayedUsers.ToArray().Length < 1)
                textResult = "There is no registered users from this telegram account.";
            else
            {
                foreach (var user in displayedUsers)
                    textResult += String.Format("{0} {1} {2} {3:d}\n", user.Name, user.MiddleName, user.SecondName, user.BirthDate.Date);
            }
            client.SendTextMessageAsync(chatId, textResult);
        }
    }
}