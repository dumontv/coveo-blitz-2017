using System;
using Coveo;
using CoveoBlitz;

namespace Coveo.Bot
{ 
    public class BreathSearch
    {
        private static int xOffSet = 1;
        private static int yOffSet = 0;

        private static int _currentSqrDiam = 0;

        private static Point _currentPos;
        private static Point _startingPos;
        private static string direction = Direction.South;
        private static Point _layerStart;

        public static Point GetNextMovementForTile(Tile[][] board, Point startingPos, Tile type)
        {
            _startingPos = startingPos;
            AddSquareLayer(startingPos);
            bool posFound = false;
            while (!posFound)
            {
                if (CheckBounds(board))
                {
                    break;
                }

                if (_currentPos.Y + 1 == _layerStart.Y)
                {
                    AddSquareLayer(_currentPos);
                }

                if (_currentPos.X >= _startingPos.X + _currentSqrDiam && direction == Direction.East ||
                    _currentPos.Y >= _startingPos.Y + _currentSqrDiam && direction == Direction.South ||
                    _currentPos.X <= _startingPos.X - _currentSqrDiam && direction == Direction.West ||
                    _currentPos.Y <= _startingPos.Y - _currentSqrDiam && direction == Direction.North)
                {
                    CycleDirection();
                }

                posFound = SearchTile(board, type);
            }

            return null;
        }

        private static bool SearchTile(Tile[][] board, Tile type)
        {
            switch (direction)
            {
                case Direction.East:
                    _currentPos = new Point(_currentPos.X + 1, _currentPos.Y);
                    if (board[_currentPos.X][_currentPos.Y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.South:
                    _currentPos = new Point(_currentPos.X, _currentPos.Y + 1);
                    if (board[_currentPos.X][_currentPos.Y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.West:
                    _currentPos = new Point(_currentPos.X - 1, _currentPos.Y);
                    if (board[_currentPos.X][_currentPos.Y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.North:
                    _currentPos = new Point(_currentPos.X, _currentPos.Y - 1);
                    if (board[_currentPos.X][_currentPos.Y] == type)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private static void AddSquareLayer(Point currentPos)
        {
            ++_currentSqrDiam;
            _currentPos = new Point(currentPos.X + xOffSet, currentPos.Y + yOffSet);
            _layerStart = _currentPos;
        }

        private static void CycleDirection()
        {
            switch (direction)
            {
                case Direction.East:
                    direction = Direction.South;
                    break;
                case Direction.South:
                    direction = Direction.West;
                    break;
                case Direction.West:
                    direction = Direction.North;
                    break;
                case Direction.North:
                    direction = Direction.East;
                    break;
            }
        }

        private static bool CheckBounds(Tile[][] board)
        {
            switch (direction)
            {
                case Direction.East:
                    if (_currentPos.X + 1 > board.GetLength(0))
                    return true;
                    break;
                case Direction.South:
                    if (_currentPos.Y + 1 > board.GetLength(1))
                    return true;
                    break;
                case Direction.West:
                    if (_currentPos.X - 1 > board.GetLength(0))
                    return true;
                    break;
                case Direction.North:
                    if (_currentPos.Y - 1 > board.GetLength(1))
                    return true;
                    break;
            }
            return false;
        }
    }
}
