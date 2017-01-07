using CoveoBlitz;

namespace Coveo.Bot
{
    public abstract class StateMachine
    {
        protected Tile _target;
        
        protected StateMachine(Tile target) {
            this._target = target;
        }

        public abstract string Act(GameState currentState);
    }
}