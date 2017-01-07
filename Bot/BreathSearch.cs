using System;
using Coveo;
using CoveoBlitz;

namespace Coveo.Bot
{
    public class BreathSearch
    {
        private static int xOffSet = 1;
        private static int yOffSet = 0;

        private static int currentSqrLength = 1;

        static public string GetNextMovementForTile(Tile[][] board, Point currentPos, Tile type)
        {
            InitialiseSearch(currentPos);
            return Direction.Stay;
        }

        private static void InitialiseSearch(Point currentPos)
        {
            //currenPos = new Point(currentPos.X + xOff)
        }
    }
}
