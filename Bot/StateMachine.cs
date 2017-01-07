using CoveoBlitz;

namespace Coveo.Bot
{
    public abstract class StateMachine
    {
        protected abstract void Act(GameState currentState);
    }
}