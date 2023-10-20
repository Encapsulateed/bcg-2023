using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bcg_bot.Types.Enums.Telegram.Queries;

namespace bcg_bot.Bot
{
    internal class QueryExecutor
    {
        public static async Task Execute(string query, long chatId, TelegramBotClient bot)
        {
            await Task.Run(async () =>
            {
                var user = new Types.User();
                user.user = new Models.User() { ChatId = chatId };
                await user.Get();
                InlineComand comand = IdByQuery[query];

                if (comand == InlineComand.TEXT_BACK)
                {
                    await ComandExecutor.Execute("BackText", chatId, bot);
                }
                else if(comand == InlineComand.QUERY_BACK)
                {

                }

            });
        }
    }
}
