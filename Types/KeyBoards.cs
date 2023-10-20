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
               InlineKeyboardButton.WithCallbackData("Нет","ASK_UNIVERSITY")
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
    }
}
