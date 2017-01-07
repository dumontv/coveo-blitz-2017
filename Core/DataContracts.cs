using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoveoBlitz
{
    [DataContract]
    public class GameResponse
    {
        [DataMember]
        public Game game;

        [DataMember]
        public Hero hero;

        [DataMember]
        public string token;

        [DataMember]
        public string viewUrl;

        [DataMember]
        public string playUrl;
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public string id;

        [DataMember]
        public int turn;

        [DataMember]
        public int maxTurns;

        [DataMember]
        public List<Customer> customers;

        [DataMember]
        public List<Hero> heroes;

        [DataMember]
        public Board board;

        [DataMember]
        public bool finished;
    }

    [DataContract]
    public class Hero
    {
        [DataMember]
        public int id;

        [DataMember]
        public string name;

        [DataMember]
        public int elo;

        [DataMember]
        public Pos pos;

        [DataMember]
        public int life;

        [DataMember]
        public int gold;

        [DataMember]
        public int frenchFriesCount;

        [DataMember]
        public int burgerCount;

        [DataMember]
        public Pos spawnPos;

        [DataMember]
        public bool crashed;
    }

    [DataContract]
    public class Pos
    {
        [DataMember]
        public int x;

        [DataMember]
        public int y;
    }

    [DataContract]
    public class Board
    {
        [DataMember]
        public int size;

        [DataMember]
        public string tiles;
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public int id;
        [DataMember]
        public int burger;
        [DataMember]
        public int frenchFries;
        [DataMember]
        public int fulfilledOrders;
    }
}