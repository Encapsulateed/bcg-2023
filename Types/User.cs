using bcg_bot.Google;
using bcg_bot.Models;
using Npgsql.Internal.TypeMapping;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bcg_bot.Types.Enums.Telegram.ModelEnums;

namespace bcg_bot.Types
{
    internal class User
    {
        public Models.User? user { get; set; }



        public Task Add()
        {

            return Task.Run(async () =>
            {

                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        await db.Users.AddAsync(this.user);
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in User.cs\nFunction: User.Add()\n\n");
                    }
                }
            });
        }

        public Task Update()
        {
            return Task.Run(async () =>
            {
                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        var user = db.Users.Where(usr => usr.ChatId == this.user.ChatId).FirstOrDefault();
                        db.Update(user).CurrentValues.SetValues(this.user);

                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Exeption in User.cs\nFunction: User.Update()\\n\n");
                    }
                }
            });
        }

        public Task Get()
        {
            return Task.Run(() =>
            {
                using (BcgContext db = new BcgContext())
                {
                    try
                    {
                        var usr = db.Users.Where(usr => usr.ChatId == this.user.ChatId).FirstOrDefault();

                        user = usr;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No such user exeption in User.cs\nFunction: User.Get()\n\n {ex}\n\n");
                    }

                }
            });

        }

        public Task EndReg(TelegramBotClient bot)
        {
            return Task.Run(async () =>
            {
                try
                {


                    string answers = $"Ваши ответы, проверьте, что всё ОКЕЙ:\n🔹 ФИО: {user.Fio}\n🔹 Дата рожденья:{user.BirthDate.Value.ToShortDateString()}\n🔹 Номер телефона: {user.Phone}\n🔹 Опыт: {user.Expirience}\n";
                    var controls = new List<List<InlineKeyboardButton>>()
                {      new List<InlineKeyboardButton>(){InlineKeyboardButton.WithCallbackData("✅ Потдвердить регистрацию ✅", "WRITE_USER_TO_GOOGLE")},
                       new List<InlineKeyboardButton>(){InlineKeyboardButton.WithCallbackData("Изменить ФИО","CHANGE_FIO")},
                        new List<InlineKeyboardButton>(){InlineKeyboardButton.WithCallbackData("Изменить Дату рожденья","CHANGE_BIRTH")},
                       new List<InlineKeyboardButton>(){InlineKeyboardButton.WithCallbackData("Изменить Телефон","CHANGE_PHONE")},
                       new List<InlineKeyboardButton>(){InlineKeyboardButton.WithCallbackData("Изменить Опыт","CHANGE_EXP")},
                };
                    // Не бауманец
                    if (user.University == string.Empty || user.University == null)
                    {

                    }
                    else if (user.University != "МГТУ им. Н.Э.Баумана")
                    {
                        answers += $"🔹 ВУЗ: {user.University}\n";
                        controls.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Изменить ВУЗ", "CHANGE_UNIVERSITY_TITLE") });

                    }
                    else
                    {
                        answers += $"🔹 Учебная группа: {user.BmstuGroup}\n";
                        controls.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Изменить Учебную группа", "CHANGE_GROUP") });

                    }
                    await bot.SendTextMessageAsync(user.ChatId, answers, replyMarkup: new InlineKeyboardMarkup(controls));
                    user.ComandLine = "";
                    Update();
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX);

                }
            });


        }

        public Task WriteToGoogle()
        {
            return Task.Run(async () =>
            {
                try
                {


                    UserTypes userType = (UserTypes)user.UserType;
                    var cmd = new Comand() { comand = new Models.Comand() { Id = user.Comand ?? 0 } };
                    await cmd.Get();

                    var gw = new GoogleWorker();

                    await gw.AppendUser(this, "Участники");
                    await gw.AppendUser(this, cmd.comand.Title);

                    if (userType == UserTypes.MENTOR)
                    {
                        await gw.AppendUser(this, "Тренеры");
                    }
                    if (userType == UserTypes.CAPITAN)
                    {
                        await gw.AppendUser(this, "Капитаны");
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
