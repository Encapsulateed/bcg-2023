using bcg_bot.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bcg_bot.Types.Enums.Telegram.ModelEnums;
using static bcg_bot.Types.Messages;
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

                if (query == "backText")
                {
                    await ComandExecutor.Execute("BackText", chatId, bot);
                }

                if (query == "BackToUniversity")
                {
                    user.user.ComandLine = "";
                    user.user.PrevComand = "INPUT_BIRTH";
                    await user.Update();

                    await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY"], replyMarkup: KeyBoards.UniversityKeyBoard);
                }


                if (query == "ASK_GROUP")
                {
                    await bot.SendTextMessageAsync(chatId, messages["ASK_GROUP"], replyMarkup: KeyBoards.BackToUniversity);
                    user.user.ComandLine = "ASK_GROUP";
                    await user.Update();

                }
                else if (query == "ASK_UNIVERSITY_TITLE")
                {
                    await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY_TITLE"], replyMarkup: KeyBoards.BackToUniversity);
                    user.user.ComandLine = "ASK_UNIVERSITY_TITLE";
                    await user.Update();
                }
                else if (query == "ASK_PHONE")
                {
                    await bot.SendTextMessageAsync(chatId, messages["ASK_PHONE"], replyMarkup: KeyBoards.BackToUniversity);
                    user.user.ComandLine = "ASK_PHONE";
                    await user.Update();
                }
                else if (query.StartsWith("SET_USER_TYPE"))
                {
                    UserTypes usertype = (UserTypes)int.Parse(query.Split(' ')[1]);
                    user.user.UserType = (int)usertype;
                    await user.Update();

                    if (usertype == UserTypes.PARTICIPANT || usertype == UserTypes.MENTOR)
                    {
                        await bot.SendTextMessageAsync(chatId, messages["SELECT_COMAND_TYPE"], replyMarkup: KeyBoards.SelectComandKeyBoard);
                    }
                    else if (usertype == UserTypes.CAPITAN)
                    {
                        await bot.SendTextMessageAsync(chatId, messages["CREATE_COMAND"], replyMarkup: KeyBoards.CreateComand);

                    }

                }
                else if (query.StartsWith("GET_COMANDS"))
                {
                    int type = int.Parse(query.Split(' ')[1]);
                    int page_num = 0;
                    try
                    {
                        page_num = int.Parse(query.Split(' ')[2]);

                    }
                    catch (Exception)
                    {
                    }
                    bool need_back_bnt = page_num == 0;

                    var comandLst = Comand.GetComandListPaginated(page_num,type);

                    var comandButtons = new List<List<InlineKeyboardButton>>();
                    foreach(var comand in comandLst)
                    {
                        List<InlineKeyboardButton> btn = new List<InlineKeyboardButton>();
                        btn.Add(InlineKeyboardButton.WithCallbackData($"{comand.comand.Title}", ""));
                    }
                    
                }
                else if (query.StartsWith("CREATE_COMAND"))
                {
                    int type = int.Parse(query.Split(' ')[1]);

                    var cmd = new bcg_bot.Models.Comand() {Track = type, Capitan = 477686161 };
                    var com = new bcg_bot.Types.Comand() { comand = cmd };
                    await com.Add();

                    //Спросить название команды
                }

            });
        }
    }
}
