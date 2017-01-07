using System;
using Coveo;
using CoveoBlitz;

namespace Coveo.Bot
{ 
    public static class BreathSearch
    {
        private static int xOffSet = 1;
        private static int yOffSet = 0;

        private static int _currentSqrDiam = 0;

        private static Pos _currentPos;
        private static Pos _startingPos;
        private static string direction = Direction.South;
        private static Pos _layerStart;

        public static Pos GetNextMovementForTile(Tile[][] board, Pos startingPos, Tile type)
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

                if (_currentPos.y + 1 == _layerStart.x)
                {
                    AddSquareLayer(_currentPos);
                }

                if (_currentPos.x >= _startingPos.x + _currentSqrDiam && direction == Direction.East ||
                    _currentPos.y >= _startingPos.y + _currentSqrDiam && direction == Direction.South ||
                    _currentPos.x <= _startingPos.x - _currentSqrDiam && direction == Direction.West ||
                    _currentPos.y <= _startingPos.y - _currentSqrDiam && direction == Direction.North)
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
                    _currentPos = new Pos { x = _currentPos.x + 1, y = _currentPos.y };
                    if (board[_currentPos.x][_currentPos.y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.South:
                    _currentPos = new Pos { x = _currentPos.x, y = _currentPos.y + 1 };
                    if (board[_currentPos.x][_currentPos.y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.West:
                    _currentPos = new Pos { x = _currentPos.x - 1, y = _currentPos.y };
                    if (board[_currentPos.x][_currentPos.y] == type)
                    {
                        return true;
                    }
                    break;
                case Direction.North:
                    _currentPos = new Pos { x = _currentPos.x, y = _currentPos.y - 1 };
                    if (board[_currentPos.x][_currentPos.y] == type)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private static void AddSquareLayer(Pos currentPos)
        {
            ++_currentSqrDiam;
            _currentPos = new Pos { x = currentPos.x + xOffSet, y = currentPos.y + yOffSet };
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
                    if (_currentPos.x + 1 > board.GetLength(0))
                    return true;
                    break;
                case Direction.South:
                    if (_currentPos.y + 1 > board.GetLength(1))
                    return true;
                    break;
                case Direction.West:
                    if (_currentPos.x - 1 > board.GetLength(0))
                    return true;
                    break;
                case Direction.North:
                    if (_currentPos.y - 1 > board.GetLength(1))
                    return true;
                    break;
            }
            return false;
        }
    }
}
