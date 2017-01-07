namespace Coveo.Bot.MadeMeThinkBot
{
    public abstract class StateMachine
    {
        //Possible Chase States
        // 1- Chasing
        // 2- Delivering
        // 3- Healing
        protected abstract void Act();
    }
}