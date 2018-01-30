using TelegramBotWebAPI.Domain.Entities;
using System.Linq;

namespace TelegramBotWebAPI.Domain
{
    public interface IUserList
    {
        IQueryable<RegisteredUser> Users { get; }
    }
}