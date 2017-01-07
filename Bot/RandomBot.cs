// Copyright (c) 2005-2016, Coveo Solutions Inc.

using Coveo.Bot;
using CoveoBlitz;
using System;
using System.Collections.Generic;

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

        private GetTilePosOnMap _getTilePos = new GetTilePosOnMap();

        Pos nextPos = null;

        private static Tile[][] _tiles = new Tile[][] {
            new Tile[] { Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new Tile[] { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new Tile[] { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new Tile[] { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL }
        };

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
            Console.WriteLine(state.board[nextPos.y][nextPos.x]);
            List<Tile> tilesToSearch = new List<Tile>();
            tilesToSearch.AddRange(_tiles[state.myHero.id - 1]);
            string direction = this.api.GetDirection(state.myHero.pos, GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, tilesToSearch));

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