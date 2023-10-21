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
using Microsoft.EntityFrameworkCore.Infrastructure;
using bcg_bot.Models;

try
{
    for(int i = 0; i < 10; i++)
    {
        var cmd = new bcg_bot.Models.Comand() { Title = $"Команда {i+1}", Track = i%2,Capitan = 477686161 };
        var com = new bcg_bot.Types.Comand() {comand =cmd};
       await com.Add();
        
    }
    for (int i = 10; i < 20; i++)
    {
        var cmd = new bcg_bot.Models.Comand() { Title = $"Команда {i + 1}", Track = i % 2, Capitan = 477686161 };
        var com = new bcg_bot.Types.Comand() { comand = cmd };
        await com.Add();
    }

    await Bot.Start();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption in Program.cs\n Function: Bot.Start()\n\n {ex}\n\n");
}