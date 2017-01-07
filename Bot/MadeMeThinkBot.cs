using CoveoBlitz;
using System;
using System.Collections.Generic;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private Pos _target = null;

        private int _lastBurgerCount = 0;
        private int _lastFriesCount = 0;

        private static List<Tile>[] _allTiles = new List<Tile>[] {
            new List<Tile>() { Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_4, Tile.FRIES_4, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.FRIES_1, Tile.BURGER_2, Tile.FRIES_2, Tile.BURGER_3, Tile.FRIES_3, Tile.BURGER_NEUTRAL, Tile.FRIES_NEUTRAL }
        };

        private static List<Tile>[] _burgerTiles = new List<Tile>[] {
            new List<Tile>() { Tile.BURGER_2, Tile.BURGER_3, Tile.BURGER_4, Tile.BURGER_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.BURGER_3, Tile.BURGER_4, Tile.BURGER_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.BURGER_2, Tile.BURGER_4, Tile.BURGER_NEUTRAL },
            new List<Tile>() { Tile.BURGER_1, Tile.BURGER_2, Tile.BURGER_3, Tile.BURGER_NEUTRAL }
        };

        private static List<Tile>[] _fryTiles = new List<Tile>[] {
            new List<Tile>() { Tile.FRIES_2, Tile.FRIES_3, Tile.FRIES_4, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.FRIES_1, Tile.FRIES_3, Tile.FRIES_4, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.FRIES_1, Tile.FRIES_2, Tile.FRIES_4, Tile.FRIES_NEUTRAL },
            new List<Tile>() { Tile.FRIES_1, Tile.FRIES_2, Tile.FRIES_3, Tile.FRIES_NEUTRAL }
        };

        private static Tile[] _customers = new Tile[] {
            Tile.CUSTOMER_1,
            Tile.CUSTOMER_2,
            Tile.CUSTOMER_3,
            Tile.CUSTOMER_4
        };

        private List<Tile> _tilesToSearch;

        public static string LastDir = Direction.Stay;

        private Pos TryCompleteCommand(GameState state)
        {
            Pos customerPosition = null;
            //Console.WriteLine(state.customers.Count);

            state.customers.Sort((Customer c1, Customer c2) => GetTilePosOnMap.DistanceBetweenPos(state.myHero.pos, GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, new List<Tile>() { _customers[c1.id - 1] })).CompareTo(GetTilePosOnMap.DistanceBetweenPos(state.myHero.pos, GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, new List<Tile>() { _customers[c2.id - 1] }))));

            for (int i = 0; i < state.customers.Count; ++i)
            {
                if (state.customers[i].burger <= state.myHero.burgerCount && state.customers[i].frenchFries <= state.myHero.frenchFriesCount)
                {
                    return GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, new List<Tile>() { _customers[state.customers[i].id - 1] });
                }
            }
            return customerPosition;
        }

        public override string Move(GameState state)
        {
            int burgerRequirements = 0;
            int fryRequirements = 0;
            state.customers.ForEach(c => {
                burgerRequirements += c.burger;
                fryRequirements += c.frenchFries;
            });            

            if (burgerRequirements == 0 && fryRequirements != 0)
            {
                _tilesToSearch = _burgerTiles[state.myHero.id - 1];
            }
            else if (fryRequirements == 0 && burgerRequirements != 0)
            {
                _tilesToSearch = _fryTiles[state.myHero.id - 1];
            }
            else
            {
                _tilesToSearch = _allTiles[state.myHero.id - 1];
            }


            if (_target == null)
            {
                _target = GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, _tilesToSearch);
            }
            else if (_lastBurgerCount != state.myHero.burgerCount || _lastFriesCount != state.myHero.frenchFriesCount)
            {
                Pos position = TryCompleteCommand(state);
                if (position != null)
                {
                    _target = position;
                }
                else
                {
                    _target = GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos, _tilesToSearch);
                }
            }

            _lastBurgerCount = state.myHero.burgerCount;
            _lastFriesCount = state.myHero.frenchFriesCount;

            LastDir = api.GetDirection(state.myHero.pos, _target);
            return LastDir;
        }

        public override void Setup()
        {
            Console.WriteLine("Game Started.");
        }

        public override void Shutdown()
        {
            Console.WriteLine("Game Over.");
        }
    }
}
