using CoveoBlitz;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private Pos _target;
        
        private bool _needFirstFind;

        private Pos _registeredCustomer;

        private int _lastBurgerCount = 0;
        private int _lastFriesCount = 0;

        private Pos TryCompleteCommand(GameState state)
        {
            Pos customerPosition = new Pos() {x = -1, y = -1};
            for (int i = 0; i < state.customers.Count; i++)
            {
                if (state.customers[i].burger <= state.myHero.burgerCount &&
                    state.customers[i].frenchFries <= state.myHero.frenchFriesCount)
                {
                    switch (state.customers[i].id)
                    {
                        case 1:
                            return GetPositionInMap(state.board, Tile.CUSTOMER_1);
                        case 2:
                            return GetPositionInMap(state.board, Tile.CUSTOMER_2);
                        case 3:
                            return GetPositionInMap(state.board, Tile.CUSTOMER_3);
                        case 4:
                            return GetPositionInMap(state.board, Tile.CUSTOMER_4);
                    }
                }
            }
            return customerPosition;
        }

        public override string Move(GameState state)
        {
            if (!_needFirstFind)
            {
                if (_lastBurgerCount != state.myHero.burgerCount ||
                    _lastFriesCount != state.myHero.frenchFriesCount)
                {
                    Pos position = TryCompleteCommand(state);
                    if (position.x != -1 && position.y != -1)
                    {
                        _target = GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos,
                            new[]
                            {
                                Tile.BURGER_1, Tile.BURGER_2, Tile.BURGER_3, Tile.BURGER_4, Tile.FRIES_1, Tile.FRIES_2,
                                Tile.FRIES_3, Tile.FRIES_4
                            });
                    }
                    else
                    {
                        _target = position;
                    }
                }
            }
            else
            {
                _target = GetTilePosOnMap.GetClosestTile(state.board, state.myHero.pos,
                    new[] { Tile.BURGER_1, Tile.BURGER_2, Tile.BURGER_3, Tile.BURGER_4, Tile.FRIES_1, Tile.FRIES_2, Tile.FRIES_3, Tile.FRIES_4 });
                _needFirstFind = false;
            }
            return api.GetDirection(state.myHero.pos, _target);
        }

        public override void Setup()
        {
            _target = new Pos() {x = 0, y = 0 };
            _registeredCustomer = new Pos() {x = 0, y = 0};
            _needFirstFind = true;
        }

        public override void Shutdown()
        {
            _target = null;
            _registeredCustomer = null;
        }

        private Pos GetPositionInMap(Tile[][] map, Tile tile) 
        {
            Pos pos = new Pos() {x = -1, y = -1};
            for (int i = 0; i < map.Length; i++) 
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[j][i] == tile) {
                        pos.x = j;
                        pos.y = i;
                        return pos;
                    }
                }
            }
            return pos;
        }
    }
}
