namespace CoveoBlitz
{
    public abstract class ISimpleBot
    {
        public ApiToolkit api { get; set; }

        public abstract void Setup();

        public abstract void Shutdown();

        public abstract string Move(GameState state);
    }
}