using CoveoBlitz;

namespace Coveo.Bot
{
    public abstract class StateMachine
    {
        public abstract string Act(GameState currentState);
    }
}