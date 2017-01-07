using System.Diagnostics;

namespace CoveoBlitz
{
    public enum Tile
    {
        IMPASSABLE_WOOD,
        FREE,
        SPIKES,
        HERO_1,
        HERO_2,
        HERO_3,
        HERO_4,
        TAVERN,
        FRIES_NEUTRAL,
        FRIES_1,
        FRIES_2,
        FRIES_3,
        FRIES_4,
        BURGER_NEUTRAL,
        BURGER_1,
        BURGER_2,
        BURGER_3,
        BURGER_4,
        CUSTOMER_1,
        CUSTOMER_2,
        CUSTOMER_3,
        CUSTOMER_4
    }

    public class Direction
    {
        public const string Stay = "Stay";
        public const string North = "North";
        public const string East = "East";
        public const string South = "South";
        public const string West = "West";
    }
}