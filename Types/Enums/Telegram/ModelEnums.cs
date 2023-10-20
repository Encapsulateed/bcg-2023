using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types.Enums.Telegram
{
    internal class ModelEnums
    {
        public enum UserTypes
        {
            PARTICIPANT,
            CAPITAN,
            MENTOR
        }
        public enum ComandTypes
        {
            MIXED,
            TEAMS
        }

        public static Dictionary<int, string> UserTypeById = new Dictionary<int, string>()
        {
            {0,"Участник" },
            {1,"Капитан" },
            {2,"Тренер" },

        };
        public static Dictionary<int, string> ComandTypeById = new Dictionary<int, string>()
        {
            {0,"Микст" },
            {1,"Сборная" },
        };
    }
}
