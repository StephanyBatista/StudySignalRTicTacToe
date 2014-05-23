using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Model.Entity;

namespace TicTacToe.Model.Service
{
    public class TicTacToeService
    {
        private readonly IList<Room> _rooms;
        private static TicTacToeService _instance;

        public static TicTacToeService GetInstance()
        {
            if (_instance == null)
                _instance = new TicTacToeService();
            return _instance;
        }

        private TicTacToeService()
        {
            _rooms = new List<Room>()
            {
                new Room {Name = "Sala 1"},
                new Room {Name = "Sala 2"},
                new Room {Name = "Sala 3"},
                new Room {Name = "Sala 4"},
                new Room {Name = "Sala 5"},
            };
        }

        public IList<Room> GetListRooms()
        {
            return _rooms;
        }

        public Room GetRoom(string roomName)
        {
            var query = _rooms.Where(c => c.Name == roomName);
            if (!query.Any())
                throw new Exception("Room not exist");
            return query.First();
        }

        public Player GetPlayer(string id)
        {
            var query = _rooms.Where(c => (c.PlayerOne != null && c.PlayerOne.Id == id) || (c.PlayerTwo != null && c.PlayerTwo.Id == id));
            if (!query.Any())
                throw new Exception("Room not exist");
            return query.First().PlayerOne.Id == id ? query.First().PlayerOne : query.First().PlayerTwo;
        }

        public Room BindPlayerInRoom(string id, string playerName, string roomName)
        {
            if (_rooms.Any(c =>
                (c.PlayerOne != null && c.PlayerOne.Id == id) ||
                (c.PlayerTwo != null && c.PlayerTwo.Id == id))
                )
                throw new Exception("Player is in a room");

            var room = GetRoom(roomName);
            room.BindPlayer(id, playerName);
            return room;
        }

        public void MakeMove(string id, string roomName, string cel)
        {
            var room = GetRoom(roomName);
            room.MakeMove(id, cel);
        }
    }
}
