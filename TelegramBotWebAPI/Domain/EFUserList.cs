using System.Linq;
using TelegramBotWebAPI.Domain.Entities;

namespace TelegramBotWebAPI.Domain
{
    public class EFUserList : IUserList
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<RegisteredUser> Users
        {
            get { return context.Users; }
        }
    }
}