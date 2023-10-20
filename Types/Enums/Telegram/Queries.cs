using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types.Enums.Telegram
{
    internal class Queries
    {
        public enum InlineComand
        {
            TEXT_BACK,
            QUERY_BACK

        }
        public static Dictionary<InlineComand, string> QueryById = new Dictionary<InlineComand, string>()
        {
            {(InlineComand)0,"backText" },
            {(InlineComand)1,"backQuery" },

        };
        public static Dictionary<string, InlineComand> IdByQuery = new Dictionary<string, InlineComand>()
        {
            {"backText",(InlineComand)0 },
            {"backQuery",(InlineComand)1 },

        };

    }
}
