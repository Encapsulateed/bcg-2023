using bcg_bot.Types;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bots.Types;
using static bcg_bot.Types.Enums.Telegram.ModelEnums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bcg_bot.Google
{
    internal class GoogleWorker
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly string ApplicationName = "baumanbot";

        private readonly string SpreadSheetId = new Config().GetGoogleToken();


        public SheetsService service;
        public GoogleCredential credential;

        public GoogleWorker()
        {
            using (var stream = new FileStream("secrets.json", FileMode.Open, FileAccess.Read))
            {
                this.credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);

            }

            this.service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public Task AppendUser(Types.User user, string sheet_title)
        {
            return Task.Run(async () =>
            {
                var usr = user.user;

                string range = $"{sheet_title}!A:Z";

                var comand = new Types.Comand();
                comand.comand = new Models.Comand() { Id = usr.Comand ?? 0 };
                await comand.Get();

                var obj_list = new List<object>() { usr.Code, usr.Fio, usr.University, usr.BmstuGroup, usr.Phone, usr.BirthDate, comand.comand.Title, UserTypeById[usr.UserType ?? 0], usr.Link, DateTime.Now.ToShortDateString() };

                var valueRange = new ValueRange();
                valueRange.Values = new List<IList<object>>() { obj_list };

                var add_header_request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId: SpreadSheetId, range);
                add_header_request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

                var response = await add_header_request.ExecuteAsync();

            });
        }

        public Task AppendComand(Types.Comand comand)
        {
            return Task.Run(async () =>
            {
                var cmd = comand.comand;

                await AddComandSheet(comand);

                string range = $"{"Команды"}!A:Z";

                var user = new Types.User();
                user.user = new Models.User() { ChatId = cmd.Capitan };
                await user.Get();

                var obj_list = new List<object>() { cmd.Title, ComandTypeById[cmd.Track ?? 0], user.user.Fio };

                var valueRange = new ValueRange();
                valueRange.Values = new List<IList<object>>() { obj_list };

                var add_header_request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId: SpreadSheetId, range);
                add_header_request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

                var response = await add_header_request.ExecuteAsync();
            });
        }
        private Task AddComandSheet(Types.Comand comand)
        {
            return Task.Run(async () =>
            {
                var cmd = comand.comand;
                // Добавить новый лист
                var addSheetRequest = new AddSheetRequest();
                addSheetRequest.Properties = new SheetProperties();
                addSheetRequest.Properties.Title = cmd.Title;
                BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
                batchUpdateSpreadsheetRequest.Requests = new List<Request>
                {
                    new Request
                    {
                        AddSheet = addSheetRequest
                    }
                };

                // Создать запрос
                var batchUpdateRequest =
                    service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, SpreadSheetId);

                // Выполнить запрос
                var response = await batchUpdateRequest.ExecuteAsync();


                string range = $"{cmd.Title}!A:Z";


                // Добавить хедер
                var obj_list = new List<object>() { "Код", "ФИО", "Университет", "Учебная группа", "Номер телефона", "Дата рожденья", "Команда", "Тип участника", "Линк телеграм", "Дата регистрации" };

                var valueRange = new ValueRange();
                valueRange.Values = new List<IList<object>>() { obj_list };

                var add_header_request = service.Spreadsheets.Values.Append(valueRange, spreadsheetId: SpreadSheetId, range);
                add_header_request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

                var appendResp = await add_header_request.ExecuteAsync();

            });
        }
    }
}
