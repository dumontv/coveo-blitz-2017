using CoveoBlitz;
using System;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private StateMachine _botState;

        public override string Move(GameState state)
		private int _currentCustomerId;        {
            Customer myCustomer = state.customers[_currentCustomerId];
            if (_botState.GetType() == typeof(ChasingState) && 
                state.myHero.life <= 30) 
            {
                _botState = new HealingState();
            }
            
            if (_botState.GetType() == typeof(ChasingState) &&
                state.myHero.burgerCount == myCustomer.burger &&
                state.myHero.frenchFriesCount == myCustomer.frenchFries) 
            {
                _botState = new DeliveringState();
            }

            Pos myPos = state.myHero.pos;
            Tile[] _adjacentTiles = new Tile[4];
            _adjacentTiles[0] = state.board[myPos.x][myPos.y - 1]; //Top
            _adjacentTiles[1] = state.board[myPos.x - 1][myPos.y]; //Left
            _adjacentTiles[2] = state.board[myPos.x][myPos.y + 1]; //Bottom
            _adjacentTiles[3] = state.board[myPos.x + 1][myPos.y]; //Right

            for (int i = 0; i < _adjacentTiles.Length; i++) {
                if (_adjacentTiles[i] == Tile.CUSTOMER_1)
                {
                    
                }
                else if (_adjacentTiles[i] == Tile.CUSTOMER_2) 
                {

                }
                else if (_adjacentTiles[i] == Tile.CUSTOMER_3) 
                {

                }
                else if (_adjacentTiles[i] == Tile.CUSTOMER_4)
                {

                }
            }

            return _botState.Act(state);   
        }

        public override void Setup()
        {
            _botState = new ChasingState();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
