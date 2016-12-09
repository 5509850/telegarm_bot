using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace pwserver_bot
{
    //read me https://habrahabr.ru/post/316222/
    class Program
    {
        private static string token = "Your Token";
        private static readonly TelegramBotClient bot = new TelegramBotClient(token);
        static void Main(string[] args)
        {            
            bot.OnMessage += Bot_OnMessage;            
            bot.SetWebhookAsync();

            var me = bot.GetMeAsync().Result;
            Console.Title = me.Username;

            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Message msg = e.Message;

            if (msg == null) return;

            // Если сообщение текстовое
            if (msg.Type == MessageType.TextMessage)
            {
                await bot.SendTextMessageAsync(msg.Chat.Id, "Hello, " + msg.From.FirstName + msg.Text + " msgID" + msg.MessageId);
            }
        }
    }
}
