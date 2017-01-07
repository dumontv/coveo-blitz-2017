using CoveoBlitz;
using System;
using System.Collections.Generic;

namespace Coveo.Bot
{
    public static class GetTilePosOnMap
    {

        private static List<Pos> _targets = new List<Pos>();

        public static Pos GetClosestTile(Tile[][] board, Pos startingPos, Tile[] type)
        {
            for (int x = 0; x < board.Length; ++x)
            {
                for (int y = 0; y < board[x].Length; ++y)
                {
                    Console.WriteLine("{0}, {1}", x, y);
                    foreach (Tile tile in type)
                    {
                        Console.WriteLine("{0}, {1}", x, y);
                        if (board[x][y] == tile)
                        {
                            _targets.Add(new Pos() { x = y, y = x });
                        }
                    }
                }
            }
            
            return FindClosestTilePos(startingPos);
        }

        private static Pos FindClosestTilePos(Pos startingPos)
        {
            int currDistance = int.MaxValue;
            Pos closestTile = new Pos() { x = 0, y = 0 };

            foreach ( Pos tile in _targets)
            {
                if (((startingPos.x - tile.x) * (startingPos.x - tile.x)) + ((startingPos.y - tile.y) * (startingPos.y - tile.y))  < currDistance)
                {
                    closestTile = tile;
                }
            }

            return closestTile;
        }
    }
}
