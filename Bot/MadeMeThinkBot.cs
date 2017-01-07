using CoveoBlitz;
using System;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private Tile _target;

        private bool _targetingCustomer;
        private bool _targetingFood; 

        private StateMachine _botState;

        private int _currentCustomerId;

        public string Move(GameState state)
        {
            if (_botState.GetType() == typeof(ChasingState)) 
            {
                Pos myPos = state.myHero.pos;
                Tile[] _adjacentTiles = new Tile[4];
                _adjacentTiles[0] = state.board[myPos.x][myPos.y - 1]; //Top
                _adjacentTiles[1] = state.board[myPos.x - 1][myPos.y]; //Left
                _adjacentTiles[2] = state.board[myPos.x][myPos.y + 1]; //Bottom
                _adjacentTiles[3] = state.board[myPos.x + 1][myPos.y]; //Right

                if (_targetingCustomer) 
                {
                    for (int i = 0; i < _adjacentTiles.Length; i++) 
                    {
                        if (_adjacentTiles[i] == Tile.CUSTOMER_1)
                        {
                            _target = Tile.CUSTOMER_1;
                        }
                        else if (_adjacentTiles[i] == Tile.CUSTOMER_2) 
                        {
                            _target = Tile.CUSTOMER_2;
                        }
                        else if (_adjacentTiles[i] == Tile.CUSTOMER_3) 
                        {
                            _target = Tile.CUSTOMER_3;
                        }
                        else if (_adjacentTiles[i] == Tile.CUSTOMER_4)
                        {
                            _target = Tile.CUSTOMER_4;
                        }
                        _botState = new ChasingState(_target);
                    }
                } 
                else if (_targetingFood)
                {

                }
            }
            else if (_botState.GetType() == typeof(HealingState)) 
            {

            }
            else if (_botState.GetType() == typeof(DeliveringState)) 
            {

            }

            Customer myCustomer = state.customers[_currentCustomerId];
            if (_botState.GetType() == typeof(ChasingState) && 
                state.myHero.life <= 30) 
            {
                //SET THE TARGET TO A COKE
                _botState = new HealingState(_target);
            }

            if (_botState.GetType() == typeof(ChasingState) &&
                state.myHero.burgerCount == myCustomer.burger &&
                state.myHero.frenchFriesCount == myCustomer.frenchFries) 
            {
                switch (_currentCustomerId) 
                {
                    case 1:
                        _target = Tile.CUSTOMER_1;
                        break;
                    case 2:
                        _target = Tile.CUSTOMER_2;
                        break;
                    case 3:
                        _target = Tile.CUSTOMER_3;
                        break;
                    case 4:
                        _target = Tile.CUSTOMER_4;
                        break;
                }
                _botState = new DeliveringState(_target);
            }

            

            return _botState.Act(state);   
        }

        public void Setup()
        {
            //RETOURNER LE PLUS PROCHE CUSTOMER
            _botState = new ChasingState(Tile.CUSTOMER_1);
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
