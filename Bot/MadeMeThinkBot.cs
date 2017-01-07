using CoveoBlitz;
using System;

namespace Coveo.Bot
{
    public class MadeMeThinkBot : ISimpleBot
    {
        private StateMachine _botState;

        public override string Move(GameState state)
        {
            return _botState.Act(state);
            
        }

        public override void Setup()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
