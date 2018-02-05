namespace TelegramBotWebAPI.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; } = ""; //TODO: добавить адрес веб-сервера

        public static string Name { get; set; } = ""; //имя бота

        public static string Key { get; set; } = ""; //токен бота
    }
}
