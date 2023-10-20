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
        public enum TextComands
        {
            START,

        }

        public static Dictionary<TextComands, string> QueryById = new Dictionary<TextComands, string>()
        {
            {0,"/start" },
        };

        public static Dictionary<string, TextComands> IdByComand = new Dictionary<string, TextComands>()
        {
            {"/start",0 }

        };
    }
}
