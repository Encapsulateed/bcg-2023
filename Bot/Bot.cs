﻿using bcg_bot.Types;
using Newtonsoft.Json.Linq;
using System;

namespace bcg_bot.Bot
{
    internal class Bot
    {
        private static TelegramBotClient bot = new TelegramBotClient(new Config().GetTgToken());
        public static async Task Start()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            while (true)
            {
                try
                {
                    var cancellationToken = CancellationToken.None;
                    var receiverOptions = new ReceiverOptions
                    {
                        AllowedUpdates = { }, // receive all update types
                    };

                    var updateReceiver = new QueuedUpdateReceiver(bot, receiverOptions);

                    try
                    {
                        await foreach (Update update in updateReceiver.WithCancellation(cancellationToken))
                        {

                            _ = Task.Run(() =>
                            {
                                _ = UpdatesHandler.HandleUpdateAsync(update, bot);
                                return Task.CompletedTask;
                            });

                        }
                    }
                    catch (OperationCanceledException exception)
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }
            }
        }
    }
}
