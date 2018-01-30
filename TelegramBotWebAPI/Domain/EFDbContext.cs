using TelegramBotWebAPI.Domain.Entities;
using System.Data.Entity;

namespace TelegramBotWebAPI.Domain
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("telegramBotDB")
        {

        }

        public DbSet<RegisteredUser> Users { get; set; }
    }
}