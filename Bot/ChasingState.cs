using System;
using Coveo.Bot;
using CoveoBlitz;

//When the player is grabbing the goods
public class ChasingState : StateMachine
{
    public ChasingState(Tile target) : base(target)
    {
    }

    public override string Act(GameState gameState)
    {
        throw new NotImplementedException();
    }
}