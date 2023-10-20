using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types.Dictionaries
{
    internal class Messages
    {
        public static readonly Dictionary<string,string> messages = new Dictionary<string, string>() 
        {
            {"START", "Привет, пидорас, скажи своё фио"},
            {"FIO_ERROR", "Шлюха, фио научись писать"},
            { "INPUT_BIRTH","Укажи пожалуйста свою дату рождения"},
            {"BIRTH_ERROR","Неверно ввёдена дата"},
            { "ASK_UNIVERSITY","Ты из МГТУ им. Н.Э.Баумана"}
        };
    }
}
