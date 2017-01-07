// Copyright (c) 2005-2016, Coveo Solutions Inc.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CoveoBlitz
{
    public class ApiToolkit
    {
        public const string TRAINING_URL = "/api/training";
        public const string ARENA_URL = "/api/arena";

        private readonly string uri;
        private readonly string serverURL;

        public string map { get; private set; }
        public Game Game { get; private set; }

        public string playURL { get; private set; }
        public string viewURL { get; private set; }
        public string botKey { get; private set; }

        public GameState gameState { get; private set; }
        public bool errored { get; set; }

        public ApiToolkit(string serverURL,
            string key,
            bool trainingMode,
            string gameId,
            uint turns = 25,
            string map = null)
        {
            this.map = map;
            this.botKey = key;
            this.uri = serverURL + (trainingMode ? TRAINING_URL : ARENA_URL);
            this.uri += "?key=" + key;
            if (trainingMode) {
                this.uri += "&turns=" + turns;
                if (map != null) {
                    this.uri += "&map=" + map;
                }
            } else {
                this.uri += "&gameId=" + gameId;
            }

            errored = false;
        }

        public string GetDirection(Pos start, Pos target)
        {
            WebRequest client = WebRequest.CreateHttp("http://game.blitz.codes:8081/pathfinding/direction?map=" + this.map + "&size=" + this.Game.board.size + 
                "&start=" + start.ToString() + "&target=" + target.ToString());
            client.Method = "GET";
            client.Timeout = 1000 * 60 * 60; // Because we don't want to timeout

            try
            {
                string result = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();
                return DeserializeDirection(result);
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.Message);
                if (exception.Response != null)
                {
                    using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                    {
                        errored = true;
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }

                Random random = new Random();
                string direction;

                switch (random.Next(0, 5))
                {
                    case 0:
                        direction = Direction.East;
                        break;

                    case 1:
                        direction = Direction.West;
                        break;

                    case 2:
                        direction = Direction.North;
                        break;

                    case 3:
                        direction = Direction.South;
                        break;

                    default:
                        direction = Direction.Stay;
                        break;
                }

                Console.WriteLine("ERROR GOING RANDOM");
                return direction;
            }
        }

        //initializes a new game, its syncronised
        public void CreateGame()
        {
            WebRequest client = WebRequest.CreateHttp(uri);
            client.Method = "POST";
            client.ContentType = "application/x-www-form-urlencoded";
            client.Timeout = 1000 * 60 * 60; // Because we don't want to timeout

            try {
                string result = new StreamReader(client.GetResponse().GetResponseStream()).ReadToEnd();
                this.gameState = Deserialize(result);
            } catch (WebException exception) {
                Console.WriteLine(exception.Message);
                if (exception.Response != null) {
                    using (var reader = new StreamReader(exception.Response.GetResponseStream())) {
                        errored = true;
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
        }

        private GameState Deserialize(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            MemoryStream stream = new MemoryStream(byteArray);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GameResponse));
            GameResponse gameResponse = (GameResponse) ser.ReadObject(stream);
            this.Game = gameResponse.game;

            playURL = gameResponse.playUrl;
            viewURL = gameResponse.viewUrl;

            return new GameState() {
                myHero = gameResponse.hero,
                heroes = gameResponse.game.heroes,
                currentTurn = gameResponse.game.turn,
                maxTurns = gameResponse.game.maxTurns,
                finished = gameResponse.game.finished,
                board = createBoard(gameResponse.game.board.size, gameResponse.game.board.tiles),
                customers = gameResponse.game.customers
            };
        }

        private string DeserializeDirection(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            MemoryStream stream = new MemoryStream(byteArray);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DirectionResponse));
            DirectionResponse directionResponse = (DirectionResponse)ser.ReadObject(stream);
            return directionResponse.direction;
        }

        public void MoveHero(string direction)
        {
            Console.WriteLine("Going {0}", direction);
            string myParameters = "key=" + botKey + "&dir=" + direction;

            using (WebClient client = new WebClient()) {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                try {
                    string result = client.UploadString(playURL, myParameters);
                    this.gameState = Deserialize(result);
                } catch (WebException exception) {
                    using (var reader = new StreamReader(exception.Response.GetResponseStream())) {
                        errored = true;
                        Console.WriteLine(exception.Message);
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
        }

        private Tile[][] createBoard(int size,
            string data)
        {
            Tile[][] board = new Tile[size][];
            for (int i = 0; i < size; i++) {
                board[i] = new Tile[size];
            }

            int x = 0, y = 0;
            char[] charData = data.ToCharArray();

            for (int i = 0; i < charData.Length; i += 2) {
                switch (charData[i]) {
                    case '^':
                        board[x][y] = Tile.SPIKES;
                        break;

                    case '#':
                        board[x][y] = Tile.IMPASSABLE_WOOD;
                        break;

                    case ' ':
                        board[x][y] = Tile.FREE;
                        break;

                    case '@':
                        switch (charData[i + 1]) {
                            case '1':
                                board[x][y] = Tile.HERO_1;
                                break;

                            case '2':
                                board[x][y] = Tile.HERO_2;
                                break;

                            case '3':
                                board[x][y] = Tile.HERO_3;
                                break;

                            case '4':
                                board[x][y] = Tile.HERO_4;
                                break;
                        }
                        break;

                    case '[':
                        board[x][y] = Tile.TAVERN;
                        break;

                    case 'F':
                        switch (charData[i + 1]) {
                            case '-':
                                board[x][y] = Tile.FRIES_NEUTRAL;
                                break;

                            case '1':
                                board[x][y] = Tile.FRIES_1;
                                break;

                            case '2':
                                board[x][y] = Tile.FRIES_2;
                                break;

                            case '3':
                                board[x][y] = Tile.FRIES_3;
                                break;

                            case '4':
                                board[x][y] = Tile.FRIES_4;
                                break;
                        }
                        break;

                    case 'B':
                        switch (charData[i + 1]) {
                            case '-':
                                board[x][y] = Tile.BURGER_NEUTRAL;
                                break;
                            case '1':
                                board[x][y] = Tile.BURGER_1;
                                break;

                            case '2':
                                board[x][y] = Tile.BURGER_2;
                                break;

                            case '3':
                                board[x][y] = Tile.BURGER_3;
                                break;

                            case '4':
                                board[x][y] = Tile.BURGER_4;
                                break;
                        }
                        break;
                    case 'C':
                        switch (charData[i + 1]) {
                            case '1':
                                board[x][y] = Tile.CUSTOMER_1;
                                break;
                            case '2':
                                board[x][y] = Tile.CUSTOMER_2;
                                break;
                            case '3':
                                board[x][y] = Tile.CUSTOMER_3;
                                break;
                            case '4':
                                board[x][y] = Tile.CUSTOMER_4;
                                break;
                        }
                        break;
                }

                x++;
                if (x == size) {
                    x = 0;
                    y++;
                }
            }
            return board;
        }
    }
}