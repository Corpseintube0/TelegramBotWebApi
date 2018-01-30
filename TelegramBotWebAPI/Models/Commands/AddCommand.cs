using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotWebAPI.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotWebAPI.Models.Commands
{
    /// <summary>
    /// Команда регистрации нового клиента.
    /// </summary>
    public class AddCommand : Command
    {
        public override string Name
        {
            get { return "register"; }
        }

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var context = new EFDbContext();
            var rows = context.Users.Where(r => r.TelegramUserId == message.From.Id);

            if (rows.ToArray().Length > 0)
            {
                client.SendTextMessageAsync(chatId, @"Already have a registered user from this telegram account.");
                return;
            }
                
            client.SendTextMessageAsync(chatId, "OK. Send me information about the client. Please use this format:\nFirst name\nSecond name\nMiddle name (if there is one, else skip this)\nBirth date (Format: DD/MM/YYYY)");
            Bot.AwaitingUsers.Add(message.From.Id);
        }
    }
}