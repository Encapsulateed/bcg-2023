using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bcg_bot.Types.Enums.Telegram.Comands;
using static bcg_bot.Types.Dictionaries.Messages;
namespace bcg_bot.Bot
{
    internal class ComandExecutor
    {
        public static async Task Execute(string message, long chatId, TelegramBotClient bot)
        {
            await Task.Run(async () =>
            {
                var user = new Types.User();
                user.user = new Models.User() { ChatId = chatId };
                Console.WriteLine(user.user.ChatId);
                await user.Get();

                var comand = IdByComand[message];


                if (comand == TextComands.START)
                {
                    await bot.SendTextMessageAsync(chatId, messages["START"]);

                    await user.Add();
                }
            });
        }
    }
}
