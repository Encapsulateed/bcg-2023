using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types
{
    internal class KeyBoards
    {
        public static InlineKeyboardMarkup BackText = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙","backText")
            }
        });
        public static InlineKeyboardMarkup UniversityKeyBoard = new InlineKeyboardMarkup(new[]
         {
            new[]
            {
               InlineKeyboardButton.WithCallbackData("Да","ASK_GROUP")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Нет","ASK_UNIVERSITY_TITLE")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Не студент","ASK_PHONE")
            },
             new[]
            {
            InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙","backText")
            }            

        });
        public static InlineKeyboardMarkup BackToUniversity =  new InlineKeyboardMarkup(new[]
         {
           new[]
            {
                InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙","BackToUniversity")
            }
        });
        public static InlineKeyboardMarkup AskUserType = new InlineKeyboardMarkup(new[]
        {
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Капитан","SET_USER_TYPE 1")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Участник","SET_USER_TYPE 0")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Тренер","SET_USER_TYPE 2")
            },
             new[]
            {
            InlineKeyboardButton.WithCallbackData("🔙 Назад 🔙","backText")
            }
        });

        public static InlineKeyboardMarkup SelectComandKeyBoard = new InlineKeyboardMarkup(new[]
        {
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Сборные","GET_COMANDS 0")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Микст","GET_COMANDS 1")
            },
        });

        public static InlineKeyboardMarkup CreateComand = new InlineKeyboardMarkup(new[]
       {
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Создать новую команду 'Cборая' ","CREATE_COMAND 0")
            },
             new[]
            {
               InlineKeyboardButton.WithCallbackData("Создать новую команду 'Микст' ","CREATE_COMAND 1")
            }
        });
    }
}
