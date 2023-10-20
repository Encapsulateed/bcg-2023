using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bcg_bot.Types.Enums.Telegram.Comands;
using static bcg_bot.Types.Dictionaries.Messages;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using bcg_bot.Types;
using static bcg_bot.Types.Enums.Telegram.Queries;

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

                    TextComand comand = IdByComand[""];
                    TextComand userComand = IdByComand[""];


                    try
                    {
                        comand = IdByComand[message];

                    }
                    catch (Exception)
                    {
                        comand = IdByComand[""];

                    }
                    try
                    {
                        userComand = IdByComand[key: user.user.ComandLine ?? ""];

                    }
                    catch (Exception)
                    {
                        userComand = IdByComand[""];

                    }

                    if (message == "BackText")
                    {
                        var backCom = IdByComand[key: user.user.PrevComand ?? ""];
                        //userComand = IdByComand[key: user.user.PrevComand ?? ""]; 

                        if(backCom == TextComand.INPUT_FIO)
                        {
                            await bot.SendTextMessageAsync(chatId, messages["START"]);
                            user.user.ComandLine = "INPUT_FIO";
                            await user.Update();

                        }
                        else if(backCom == TextComand.INPUT_BIRTH)
                        {
                            await bot.SendTextMessageAsync(chatId, messages["INPUT_BIRTH"], replyMarkup: KeyBoards.BackText);

                            user.user.PrevComand = "INPUT_FIO";
                            user.user.ComandLine = "INPUT_BIRTH";
                            await user.Update();
                        }
                    }
                    else
                    {


                        if (comand == TextComand.START)
                        {
                            await bot.SendTextMessageAsync(chatId, messages["START"]);

                            await user.Add();

                            user.user.ComandLine = "INPUT_FIO";
                            await user.Update();
                        }
                        else if (userComand == TextComand.INPUT_FIO)
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
                        else if (userComand == TextComand.INPUT_BIRTH)
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
                                    Console.WriteLine(ex);
                                    await bot.SendTextMessageAsync(chatId, messages["BIRTH_ERROR"]);
                                }
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(chatId, messages["BIRTH_ERROR"]);
                            }
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
