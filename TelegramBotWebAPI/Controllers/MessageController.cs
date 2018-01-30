using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramBotWebAPI.Models;
using System;
using TelegramBotWebAPI.Domain;

namespace TelegramBotWebAPI.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.GetClient();
            var context = new EFDbContext();

            if (Bot.AwaitingUsers.Contains(update.Message.From.Id))
            {
                var strArray = message.Text.Split('\n');
                if (strArray.Length < 3)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, @"Sorry, wrong syntax. Message must contains at least 3 strings.", replyToMessageId: message.MessageId);
                    Bot.AwaitingUsers.Remove(update.Message.From.Id);
                    return Ok();
                }
                int dateIndex = strArray.Length > 3 ? 3 : 2;
                DateTime birthDate = new DateTime();
                if (!DateTime.TryParse(strArray[dateIndex], out birthDate))
                {
                    await client.SendTextMessageAsync(message.Chat.Id, @"Sorry, wrong date format.", replyToMessageId: message.MessageId);
                    Bot.AwaitingUsers.Remove(update.Message.From.Id);
                    return Ok();
                }
                Domain.Entities.RegisteredUser newUser = new Domain.Entities.RegisteredUser
                {
                    Name = strArray[0],
                    SecondName = strArray[1],
                    MiddleName = strArray.Length > 3 ? strArray[2] : "",
                    BirthDate = birthDate,
                    TelegramUserId = message.From.Id
                };
                context.Users.Add(newUser);
                context.SaveChanges();
                await client.SendTextMessageAsync(message.Chat.Id, @"Information successfully added!");
                Bot.AwaitingUsers.Remove(update.Message.From.Id);
                return Ok();
            }
            
            foreach (var command in commands)
            {
                if (string.Format("/{0}", command.Name) == message.Text)
                {
                    command.Execute(message, client);
                    return Ok();
                }
            }

            await client.SendTextMessageAsync(message.Chat.Id, "Unknown command. Please use these commands:\n/register - Registrates a new client\n/get_info - Shows the information about the client\n/delete_info - Removes the information about the client");
            return Ok();
        }
    }
}
