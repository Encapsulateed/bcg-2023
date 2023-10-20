global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot;
global using Telegram.Bot.Types;
global using bcg_bot;
global using Telegram.Bot.Types;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot.Polling;
global using Google.Apis.Auth.OAuth2;
global using Google.Apis.Services;
global using Google.Apis.Sheets.v4;
global using Google.Apis.Sheets.v4.Data;
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Linq;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;
global using Telegram.Bot;
global using Telegram.Bot.Types.ReplyMarkups;
using bcg_bot.Bot;
using bcg_bot.Types;
using System.Text.RegularExpressions;
using Telegram.Bots.Types;

try
{
    if (Regex.IsMatch("09.09.2003", @"^\d{1,2}/\d{1,2}/\d{4}$|^\d{4}-\d{1,2}-\d{1,2}$|^\d{1,2}[/\.]\d{1,2}[/\.]\d{4}$"))
    {
    }
    else
    {
        Console.WriteLine("0");

    }


    await Bot.Start();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption in Program.cs\n Function: Bot.Start()\n\n {ex}\n\n");
}