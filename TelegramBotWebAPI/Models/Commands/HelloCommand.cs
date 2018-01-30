using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotWebAPI.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name
        {
            get { return "hello"; }
        }

        public override void Execute(Message message, TelegramBotClient client)
        {
            if (message == null)
                return;
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            client.SendTextMessageAsync(chatId, string.Format("Hello, {0}!", message.From.FirstName), replyToMessageId: messageId);
        }
    }
}