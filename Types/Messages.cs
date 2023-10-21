using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcg_bot.Types
{
    public static class Messages
    {
        public static readonly Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"START", "Привет, пидорас, скажи своё фио"},
            {"FIO_ERROR", "Шлюха, фио научись писать"},
            {"INPUT_BIRTH","Укажи пожалуйста свою дату рождения"},
            {"BIRTH_ERROR","Неверно ввёдена дата"},
            {"ASK_UNIVERSITY","Ты из МГТУ им. Н.Э.Баумана"},
            {"ASK_GROUP","Учебная группа"},
            {"ASK_UNIVERSITY_TITLE","Название университета"},
            {"ASK_PHONE","Твой номер телефона"},
          
            {"GROUP_ERROR","Неверено введена учебная группа"} ,

            {"PHONE_ERROR","Неверно введён номер телефона"},
            {"ASK_EXP","Принимал ли ты раньше участие в чемпионатах по спортивному программированию (например, ICPC)?\r\nЕсли да, то в каких и с каким результатом?"},

            {"ASK_USER_TYPE", "Выбери свою роль в команде ИЗМЕНИТЬ НЕЛЬЗЯ"},
            {"SELECT_COMAND_TYPE", "Выбери трек, в котором участвует твоя команда\n Сборная - это один ВУЗ\n Микст - это разные вузы"},
            {"CREATE_COMAND","Выбери трек, в котором будет участвует твоя команда\n Сборная - это один ВУЗ\n Микст - это разные вузы"}
        };
    }
}