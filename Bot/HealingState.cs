using System;
using CoveoBlitz;
using Coveo.Bot;

//When the player is going to an healing point
public class HealingState : StateMachine
{
    public HealingState(Tile target) : base(target)
    {
    }

    public override string Act(GameState gameState)
    {
        throw new NotImplementedException();
    }
}