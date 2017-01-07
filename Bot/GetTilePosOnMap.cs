using CoveoBlitz;
using System;
using System.Collections.Generic;

namespace Coveo.Bot
{
    public class GetTilePosOnMap
    {
        private static List<Pos> _targets = new List<Pos>();

        public static Pos GetClosestTile(Tile[][] board, Pos startingPos, List<Tile> types)
        {
            _targets = new List<Pos>();

            for (int i = 0; i < board.Length; ++i)
            {
                for (int j = 0; j < board[i].Length; ++j)
                {
                    types.ForEach(t => {
                        if (board[i][j] == t)
                        {
                            _targets.Add(new Pos() { x = j, y = i });
                        }
                    });
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
                int distance = DistanceBetweenPos(startingPos, tile);
                if (distance < currDistance)
                {
                    currDistance = distance;
                    closestTile = tile;
                }
            }

            return closestTile;
        }

        public static int DistanceBetweenPos(Pos start, Pos destination)
        {
            return Math.Abs((destination.x - start.x) * (destination.x - start.x)) + ((destination.y - start.y) * (destination.y - start.y));
        }
    }
}