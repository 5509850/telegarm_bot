using System;

namespace telegram_bot
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly string token = "<YOUR TOKEN>";
        public void GetUpdate(Update update)
        {
            string mess = update.message.text;
            SendMessage(update.message.chat.id, update.message.from.first_name);
        }

        private async void SendMessage(long chatid, string name)
        {
            var Bot = new Telegram.Bot.TelegramBotClient(token);
            await Bot.SendTextMessageAsync(chatid, "Hello, " + name + "!");
        }      
    }
}
