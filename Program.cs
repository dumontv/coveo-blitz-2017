﻿// Copyright (c) 2005-2016, Coveo Solutions Inc.

using Coveo.Bot;
using CoveoBlitz;
using System;

namespace Coveo
{
    internal class MyBot
    {
        /**
         * @param args args[0] Private key
         * @param args args[1] [training|arena]
         * @param args args[2] Game Id
         */

        private static void Main(string[] args)
        {
            if (args.Length < 2) {
                Console.WriteLine("Usage: myBot.exe key training|arena gameId");
                Console.WriteLine("gameId is optionnal when in training mode");
                Console.ReadKey();
                return;
            }

            string serverURL = "http://game.blitz.codes:8080";
            string gameId = args.Length == 3 ? args[2] : null;

            SimpleBotRunner runner = new SimpleBotRunner(
                new ApiToolkit(serverURL, args[0], args[1] == "training", gameId, 300, "m5"),
                new MadeMeThinkBot()
            );

            runner.Run();

            Console.Read();
        }
    }
}
