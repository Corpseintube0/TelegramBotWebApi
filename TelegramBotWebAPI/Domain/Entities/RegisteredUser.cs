using System;
using System.ComponentModel.DataAnnotations;

namespace TelegramBotWebAPI.Domain.Entities
{
    public class RegisteredUser
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public int TelegramUserId { get; set; }
    }
}