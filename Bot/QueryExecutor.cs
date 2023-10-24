using bcg_bot.Models;
using bcg_bot.Types;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
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
                    await ComandExecutor.Execute("BackText", chatId, bot, String.Empty);
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
                    int lastId = 0;


                    try
                    {
                        lastId = int.Parse(query.Split(' ')[2]);

                    }
                    catch (Exception)
                    {
                    }

                    bool need_back_bnt = lastId != 0;
                    bool need_next_btn = false;


                    var comandLst = Types.Comand.GetComandListPaginated(lastId, type, out need_next_btn);


                    lastId = comandLst[comandLst.Count - 1].comand.Id;



                    var comandButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var comand in comandLst)
                    {
                        List<InlineKeyboardButton> btn = new List<InlineKeyboardButton>();


                        string univer = "";

                        if (type == 1)
                        {
                            var capitain = new Types.User();
                            capitain.user = new Models.User() { ChatId = comand.comand.Capitan };
                            await capitain.Get();
                            univer = $" - {capitain.user.University}";
                        }


                        btn.Add(InlineKeyboardButton.WithCallbackData($"{comand.comand.Title}{univer}", $"SELECT_COMAND {comand.comand.Id} {type}"));

                        comandButtons.Add(btn);
                    }
                    var controls = new List<InlineKeyboardButton>();
                    if (need_back_bnt)
                    {
                        controls.Add(InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙", $"GET_COMANDS {type} {0}"));

                    }
                    else if (need_back_bnt == false)
                    {
                        controls.Add(InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙", $"SET_USER_TYPE {user.user.UserType}"));

                    }
                    if (need_next_btn)
                    {
                        var next = new List<InlineKeyboardButton>();
                        controls.Add(InlineKeyboardButton.WithCallbackData("🔜 Далее 🔜", $"GET_COMANDS {type} {lastId}"));
                    }
                    comandButtons.Add(controls);


                    var KeyBoard = new InlineKeyboardMarkup(comandButtons);


                    await bot.SendTextMessageAsync(chatId, messages["SELECT_COMAND"], replyMarkup: KeyBoard);

                }
                else if (query.StartsWith("CREATE_COMAND"))
                {
                    int type = int.Parse(query.Split(' ')[1]);

                    var cmd = new Models.Comand() { Track = type, Capitan = chatId, UserCount = 1 };
                    var com = new Types.Comand() { comand = cmd };
                    await com.Add();

                    await com.GetByCapitanId();

                    user.user.ComandLine = "ASK_COMAND_TITLE";
                    user.user.Comand = com.comand.Id;
                    await bot.SendTextMessageAsync(chatId, messages["ASK_COMAND_TITLE"]);
                    await user.Update();

                }
                else if (query.StartsWith("SELECT_COMAND"))
                {
                    int comandId = int.Parse(query.Split(' ')[1]);
                    int comandType = int.Parse(query.Split(' ')[2]);


                    var selected = new Types.Comand() { comand = new Models.Comand() { Id = comandId } };
                    await selected.Get();

                    var capitain = new Types.User();
                    capitain.user = new Models.User() { ChatId = selected.comand.Capitan };
                    await capitain.Get();


                    var contols = new List<List<InlineKeyboardButton>>
                {
                    new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Вступить", $"JOIN {comandId} {capitain.user.ChatId}") },

                    new List<InlineKeyboardButton>()
                    {
                        InlineKeyboardButton.WithCallbackData($"🔙 Назад 🔙", $"GET_COMANDS {comandType} {0}")
                    }
                };


                    var keyBoard = new InlineKeyboardMarkup(contols);


                    string comandData = $"🔹 Название: {selected.comand.Title}\n🔹 Капитан: {capitain.user.Fio}\n🔹 Количество участников:{selected.comand.UserCount}";


                    await bot.SendTextMessageAsync(chatId, comandData, replyMarkup: keyBoard);
                }
                else if (query.StartsWith("JOIN"))
                {
                    int comandId = int.Parse(query.Split(' ')[1]);
                    long capitnChat = long.Parse(query.Split(' ')[2]);

                    var comand = new Types.Comand() { comand = new Models.Comand() { Id = comandId } };
                    await comand.Get();
                    UserTypes usertype = (UserTypes)user.user.UserType;

                    if ((comand.comand.UserCount < 3 && usertype == UserTypes.PARTICIPANT) || usertype == UserTypes.MENTOR)
                    {
                        string user_Type = "участник";


                        if (usertype == UserTypes.MENTOR)
                            user_Type = "тренер";


                        string msgForCapitain = $"В твою команду хочет вступить новый {user_Type} - {user.user.Fio}\nЕго опыт в спортивном программировании:\n{user.user.Expirience}";
                        var KeyBoard = new InlineKeyboardMarkup(new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("✅ Принять ✅",$"ADD_TO_COMAND {comandId} {chatId}")
                            },
                             new[]
                            {
                                InlineKeyboardButton.WithCallbackData("❌ Отказать ❌",$"REJECT_ADD_TO_COMAND {comandId} {chatId}")
                            }
                        });

                        await bot.SendTextMessageAsync(comand.comand.Capitan, msgForCapitain, replyMarkup: KeyBoard);

                        await bot.SendTextMessageAsync(chatId, messages["WAIT_FOR_CAPITAINE_RESPONSE"]);

                    }
                    else
                    {
                        await bot.SendTextMessageAsync(chatId, messages["TO_MANY_PARTICIPANTS"]);

                        if (usertype == UserTypes.PARTICIPANT || usertype == UserTypes.MENTOR)
                        {
                            await bot.SendTextMessageAsync(chatId, messages["SELECT_COMAND_TYPE"], replyMarkup: KeyBoards.SelectComandKeyBoard);
                        }
                    }

                }
                else if (query.StartsWith("ADD_TO_COMAND"))
                {
                    int comandId = int.Parse(query.Split(' ')[1]);
                    long userChat = long.Parse(query.Split(' ')[2]);

                    var comand = new Types.Comand() { comand = new Models.Comand() { Id = comandId } };
                    await comand.Get();

                    comand.comand.UserCount++;


                    string msgForUser = $"Поздравляем! Капитан команды {comand.comand.Title} принял Вас!";

                    await bot.SendTextMessageAsync(userChat, msgForUser);
                    await comand.Update();

                    var userForSend = new Types.User();
                    userForSend.user = new Models.User() { ChatId = userChat };
                    await userForSend.Get();
                    userForSend.user.Comand = comandId;

                    await userForSend.Update();
             

                    await userForSend.EndReg(bot);


                }
                else if (query.StartsWith("REJECT_ADD_TO_COMAND"))
                {
                    int comandId = int.Parse(query.Split(' ')[1]);
                    long userChat = long.Parse(query.Split(' ')[2]);

                    var comand = new Types.Comand() { comand = new Models.Comand() { Id = comandId } };
                    await comand.Get();

                    string msgForUser = $"К сожалению, Капитан команды {comand.comand.Title} отказал Вам!\nВы можете выбрать другую команду.";
                    await bot.SendTextMessageAsync(userChat, msgForUser);

                    await bot.SendTextMessageAsync(userChat, messages["SELECT_COMAND_TYPE"], replyMarkup: KeyBoards.SelectComandKeyBoard);

                }
                else if(query == "CHANGE_FIO")
                {
                    user.user.ComandLine = "CHANGE_FIO";
                    await bot.SendTextMessageAsync(chatId, "Введи своё ФИО");
                    await user.Update();

                }
                else if (query == "CHANGE_PHONE")
                {
                    user.user.ComandLine = "CHANGE_PHONE";
                    await bot.SendTextMessageAsync(chatId, messages["ASK_PHONE"]);
                    await user.Update();
                }
                else if (query == "CHANGE_EXP")
                {
                    user.user.ComandLine = "CHANGE_EXP";
                    await bot.SendTextMessageAsync(chatId, messages["ASK_EXP"]);
                    await user.Update();
                }
                else if (query == "CHANGE_BIRTH")
                {
                    user.user.ComandLine = "CHANGE_BIRTH";
                    await bot.SendTextMessageAsync(chatId, messages["INPUT_BIRTH"]);
                    await user.Update();
                }
                else if (query == "CHANGE_UNIVERSITY_TITLE")
                {
                    user.user.ComandLine = "CHANGE_UNIVERSITY_TITLE"; 
                    await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY_TITLE"]);
                    await user.Update();
                }
                else if (query == "CHANGE_GROUP")
                {
                    user.user.ComandLine = "CHANGE_GROUP";
                    await bot.SendTextMessageAsync(chatId, messages["GROUP_ERROR"]);
                    await user.Update();
                }
                else if(query == "WRITE_USER_TO_GOOGLE")
                {
                    await user.WriteToGoogle();

                    await bot.SendTextMessageAsync(chatId, $"Спасибо за регистрацию, вот твой уникальный код {user.user.Code}");

                }
            });
        }
    }
}
