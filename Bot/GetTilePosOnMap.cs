using CoveoBlitz;
using System;
using System.Collections.Generic;

namespace Coveo.Bot
{
    public class GetTilePosOnMap
    {
        private static List<Pos> _targets = new List<Pos>();

        public static Pos GetClosestTile(Tile[][] board, Pos startingPos, Tile[] type)
        {
            _targets.Clear();

            for (int x = 0; x < board.Length; ++x)
            {
                for (int y = 0; y < board[x].Length; ++y)
                {
                    foreach (Tile tile in type)
                    {
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


            foreach (Pos tile in _targets)
            {
                int distance = Math.Abs((tile.x - startingPos.x) * (tile.x - startingPos.x)) + ((tile.y - startingPos.y) * (tile.y - startingPos.y));
                if (distance < currDistance)
                {
                    currDistance = distance;
                    closestTile = tile;
                }
            }

            return closestTile;
        }
    }
}
