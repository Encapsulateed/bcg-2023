using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types.Enums.Telegram
{
    internal class Comands
    {
        public enum TextComand
        {
            START,
            INPUT_FIO,
            NULL,
            INPUT_BIRTH
        }

        public static Dictionary<TextComand, string> QueryById = new Dictionary<TextComand, string>()
        {
            {(TextComand)0,"/start" },
            {(TextComand)1,"INPUT_FIO" },
            {(TextComand)2,"" },
            {(TextComand)3,"INPUT_BIRTH" },



        };

        public static Dictionary<string, TextComand> IdByComand = new Dictionary<string, TextComand>()
        {
            {"/start",(TextComand)0 },
            {"INPUT_FIO",(TextComand)1 },
            {"",(TextComand)2},
            {"INPUT_BIRTH",(TextComand)3},
        };
    }
}
