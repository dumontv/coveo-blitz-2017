using CoveoBlitz;
using System;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private StateMachine _botState;

        public string Move(GameState state)
        {
            return _botState.Act(state);
            
        }

        public void Setup()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
