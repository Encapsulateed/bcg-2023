using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using bcg_bot.Types;
using static bcg_bot.Types.Messages;


namespace bcg_bot.Bot
{
    internal class ComandExecutor
    {
        public static async Task Execute(string message, long chatId, TelegramBotClient bot)
        {
            await Task.Run(async () =>
            {
                try
                {
                    var user = new Types.User();
                    user.user = new Models.User() { ChatId = chatId };
                    await user.Get();
                    var com = user.user.ComandLine ?? "";


                    if (message == "BackText")
                    {
                        var prev = user.user.PrevComand;
                        if (prev == "INPUT_FIO")
                        {
                            await bot.SendTextMessageAsync(chatId, messages["START"]);
                            user.user.ComandLine = "INPUT_FIO";
                            await user.Update();
                        }
                        else if (prev == "INPUT_BIRTH")
                        {
                            await bot.SendTextMessageAsync(chatId, messages["INPUT_BIRTH"], replyMarkup: KeyBoards.BackText);

                            user.user.PrevComand = "INPUT_FIO";
                            user.user.ComandLine = "INPUT_BIRTH";
                            await user.Update();
                        }
                        else if (prev == "ASK_GROUP")
                        {
                            user.user.ComandLine = "";
                            user.user.PrevComand = "INPUT_BIRTH";
                            await user.Update();

                            await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY"], replyMarkup: KeyBoards.UniversityKeyBoard);
                        }
                        else if (prev == "ASK_UNIVERSITY_TITLE")
                        {
                            user.user.ComandLine = "";
                            user.user.PrevComand = "INPUT_BIRTH";
                            await user.Update();

                            await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY_TITLE"], replyMarkup: KeyBoards.UniversityKeyBoard);
                        }
                        else if (prev == "ASK_PHONE")
                        {

                            await bot.SendTextMessageAsync(chatId, messages["ASK_PHONE"], replyMarkup: KeyBoards.BackToUniversity);
                            user.user.ComandLine = "ASK_PHONE";
                            await user.Update();

                        }

                    }
                    else
                    {
                        if (message == "/start")
                        {
                            await bot.SendTextMessageAsync(chatId, messages["START"]);

                            await user.Add();

                            user.user.ComandLine = "INPUT_FIO";
                            await user.Update();
                        }
                        else if (com == "INPUT_FIO")
                        {
                            if (Regex.IsMatch(message, @"^(\w+\s+){1,3}\w+$"))
                            {
                                user.user.Fio = message;

                                await bot.SendTextMessageAsync(chatId, messages["INPUT_BIRTH"], replyMarkup: KeyBoards.BackText);

                                user.user.PrevComand = "INPUT_FIO";
                                user.user.ComandLine = "INPUT_BIRTH";
                                await user.Update();

                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, messages["FIO_ERROR"]);
                            }
                        }
                        else if (com == "INPUT_BIRTH")
                        {
                            if (Regex.IsMatch(message, @"^\d{1,2}/\d{1,2}/\d{4}$|^\d{4}-\d{1,2}-\d{1,2}$|^\d{1,2}[/\.]\d{1,2}[/\.]\d{4}$"))
                            {
                                try
                                {
                                    user.user.BirthDate = DateTime.Parse(message);

                                    user.user.ComandLine = "";
                                    user.user.PrevComand = "INPUT_BIRTH";
                                    await user.Update();

                                    await bot.SendTextMessageAsync(chatId, messages["ASK_UNIVERSITY"], replyMarkup: KeyBoards.UniversityKeyBoard);
                                }
                                catch (Exception ex)
                                {

                                    await bot.SendTextMessageAsync(chatId, messages["BIRTH_ERROR"]);
                                }
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, messages["BIRTH_ERROR"], replyMarkup: KeyBoards.BackText);
                            }
                        }
                        else if (com == "ASK_GROUP")
                        {
                            if (Regex.IsMatch(message, @"^[А-ЯЁа-яё]+\d{1,2}-\d{2}[А-ЯЁа-яё]{1,2}"))
                            {
                                user.user.BmstuGroup = message;

                                await bot.SendTextMessageAsync(chatId, messages["ASK_PHONE"], replyMarkup: KeyBoards.BackText);

                                user.user.PrevComand = "ASK_GROUP";
                                user.user.ComandLine = "ASK_PHONE";

                                await user.Update();

                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, messages["GROUP_ERROR"], replyMarkup: KeyBoards.BackToUniversity);
                            }
                        }
                        else if (com == "ASK_UNIVERSITY_TITLE")
                        {
                            user.user.University = message;
                            await bot.SendTextMessageAsync(chatId, messages["ASK_PHONE"], replyMarkup: KeyBoards.BackText);

                            user.user.PrevComand = "ASK_PHONE";
                            user.user.ComandLine = "ASK_UNIVERSITY_TITLE";
                            await user.Update();
                        }
                        else if (com == "ASK_PHONE")
                        {
                            if (Regex.IsMatch(message, @"^(?:\+7|8)(?:\s*-\s*)?(\d{3})(?:\s*-\s*)?(\d{3})(?:\s*-\s*)?(\d{2})(?:\s*-\s*)?(\d{2})$"))
                            {
                                user.user.Phone = message;
                                await bot.SendTextMessageAsync(chatId, messages["ASK_EXP"], replyMarkup: KeyBoards.BackText);
                                user.user.PrevComand = "ASK_PHONE";
                                user.user.ComandLine = "ASK_EXP";
                                await user.Update();
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, messages["PHONE_ERROR"], replyMarkup: KeyBoards.BackText);
                            }
                        }
                        else if (com == "ASK_EXP")
                        {
                            user.user.Expirience = message;
                            user.user.PrevComand = "ASK_EXP";
                            user.user.PrevComand = "ASK_PHONE";
                            await user.Update();

                            await bot.SendTextMessageAsync(chatId, messages["ASK_USER_TYPE"], replyMarkup: KeyBoards.AskUserType);


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }



            });
        }
    }
}
