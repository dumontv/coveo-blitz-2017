using System;
using CoveoBlitz;
using Coveo.Bot;

//When the player is delivering the goods
public class DeliveringState : StateMachine
{
    public DeliveringState(Tile target) : base(target)
    {
    }

    public override string Act(GameState gameState)
    {
        throw new NotImplementedException();
    }
}