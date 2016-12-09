using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace pwserver_bot
{
    //read me https://habrahabr.ru/post/316222/
    class Program
    {
        private static string token = "<Yout Bot Token>";
        private static readonly TelegramBotClient bot = new TelegramBotClient(token);
        static void Main(string[] args)
        {
            bot.OnMessage += Bot_OnMessage;
            //  bot.SetWebhookAsync();

            var me = bot.GetMeAsync().Result;
            Console.Title = me.Username;

            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Message msg = e.Message;

            if (msg == null || msg.Text == null) return;
            long chatid = msg.Chat.Id;          
            // Если сообщение текстовое
            if (msg.Type == MessageType.TextMessage)
            {
                switch (msg.Text)
                {
                    case "/start":
                        {                          
                           SendMessage(msg, "Welcome to Loan Calculator");                         
                           break;
                        }
                    case "/restart":
                        {
                            SendMessage(msg, "Welcome to Loan Calculator");
                            break;
                        }
                    case "/example":
                        {
                            SendMessage(chatid, @"$1000 3% 5y
$1000 3% 5y");
                            break;
                        }
                    case "/stop":
                        {                          
                            break;
                        }
                        
                    default:
                        {
                            string[] array = msg.Text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            if (array != null && array.Length == 3 || array.Length == 4)
                            {
                                long a = 0;
                                long.TryParse(Regex.Replace(array[0], "[^0-9]", ""), out a);

                                double p = 0;
                                Match m = Regex.Match(array[1], @"[0-9]+(\.[0-9]+)?");
                                p = Convert.ToDouble(m.Value, CultureInfo.InvariantCulture);
                                
                                double y = 0;
                                m = Regex.Match(array[2], @"[0-9]+(\.[0-9]+)?");
                                y = Convert.ToDouble(m.Value, CultureInfo.InvariantCulture);
                                
                                double c = 0;                                                                
                                if (array.Length == 4)
                                {
                                    m = Regex.Match(array[3], @"[0-9]+(\.[0-9]+)?");
                                    c = Convert.ToDouble(m.Value, CultureInfo.InvariantCulture);                                    
                                }
                                if (c != 0)
                                {
                                    SendMessage(chatid, string.Format(@"${0} Loan Amount
{1}% Interest Rate
{2} Loan term in years
${3} Monthly Maintenance Fee", a, p, y, c));
                                }
                                else
                                {
                                    SendMessage(chatid, string.Format(@"${0} Loan Amount
{1}% Interest Rate
{2} Loan term in years", a, p, y));
                                }
                                
                            }
                            else
                            {
                                SendMessage(chatid, @"example of use case: 
1000 3 5
<$ - Loan Amount> 
<% - Interest Rate> 
<years - Loan term> 
<$ Monthly Maintenance Fee> (optional)");
                            }                            
                            break;
                        }
                }

                /*
example - Example of input data for Loan Calculator 
loan - Loan Amount
interest - Interest Rate
term - Number of Years Loan Term
fee - (optional) Monthly Maintenance Fee
calculate - Calculate of Monthly Payments
clear - Reset Input Data
help - Displays a Help



                 */
                //for easy calculate of Monthly Payments
                // await bot.SendTextMessageAsync(msg.Chat.Id, "Hello, " + msg.From.FirstName + msg.Text + " msgID" + msg.MessageId);
            }


        }


        private static async void SendMessage(Message incomingMessage, string messageForSend)
        {
            if (incomingMessage.From.FirstName != null)
            {
                await bot.SendTextMessageAsync(incomingMessage.Chat.Id, string.Format("{0}, {1}", messageForSend, incomingMessage.From.FirstName));
            }
            else
            {
                await bot.SendTextMessageAsync(incomingMessage.Chat.Id, messageForSend);
            }
        }

        private static async void SendMessage(long messId, string messageForSend)
        {            
                await bot.SendTextMessageAsync(messId, messageForSend);            
        }
    }
}
