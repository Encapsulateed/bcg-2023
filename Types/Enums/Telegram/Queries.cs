using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types.Enums.Telegram
{
    internal class Queries
    {
        public enum InlineQueries
        {
            PARTICIPANT,

        }
        public static Dictionary<int, string> QueryById = new Dictionary<int, string>()
        {
            {0,"Участник" },


        };

    }
}
