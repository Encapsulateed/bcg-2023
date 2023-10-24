global using Microsoft.EntityFrameworkCore;
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Linq;
global using System.Threading.Tasks;
global using Telegram.Bot;
global using Telegram.Bot.Polling;
global using Telegram.Bot.Types;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot.Types.ReplyMarkups;
using bcg_bot.Bot;
using bcg_bot.Types;
using bcg_bot.Models;
using Telegram.Bots.Types;

try
{

    await Bot.Start();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption in Program.cs\n Function: Bot.Start()\n\n {ex}\n\n");
}