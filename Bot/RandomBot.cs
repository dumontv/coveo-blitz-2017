// Copyright (c) 2005-2016, Coveo Solutions Inc.

using Coveo.Bot;
using System;

namespace CoveoBlitz.RandomBot
{
    /// <summary>
    /// RandomBot
    ///
    /// This bot will randomly chose a direction each turns.
    /// </summary>
    public class RandomBot : ISimpleBot
    {
        private readonly Random random = new Random();

        /// <summary>
        /// This will be run before the game starts
        /// </summary>
        public override void Setup()
        {
            Console.WriteLine("Coveo's C# RandomBot");
        }

        /// <summary>
        /// This will be run on each turns. It must return a direction fot the bot to follow
        /// </summary>
        /// <param name="state">The game state</param>
        /// <returns></returns>
        public override string Move(GameState state)
        {
            

            string direction = this.api.GetDirection(state.myHero.pos, new Pos { x = 9, y = 9});

            //switch (random.Next(0, 5))
            //{
            //    case 0:
            //        direction = Direction.East;
            //        break;

            //    case 1:
            //        direction = Direction.West;
            //        break;

            //    case 2:
            //        direction = Direction.North;
            //        break;

            //    case 3:
            //        direction = Direction.South;
            //        break;

            //    default:
            //        direction = Direction.Stay;
            //        break;
            //}

            Console.WriteLine("Completed turn {0}, going {1}", state.currentTurn, direction);
            return direction;
        }

        /// <summary>
        /// This is run after the game.
        /// </summary>
        public override void Shutdown()
        {
            Console.WriteLine("Done");
        }
    }
}