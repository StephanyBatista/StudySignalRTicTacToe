using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TicTacToe.Model.Entity;
using TicTacToe.Model.Service;

namespace TicTacToe.Web.SignalR
{
    public class TicTacToeHub : Hub 
    {
        private TicTacToeService _service;

        public TicTacToeHub()
        {
            _service = TicTacToeService.GetInstance();
        }
        
        public IList<Room> GetListRooms()
        {
            return _service.GetListRooms();
        }

        public override Task OnDisconnected()
        {
            var player = _service.GetPlayer(Context.ConnectionId);
            var room = _service.GetRoom(player.RoomName);
            Groups.Remove(Context.ConnectionId, room.Name);
            room.RemovePlayer(player.Id);
            Clients.All.changeRoom(_service.GetListRooms());
            Clients.Group(room.Name).opponentGiveUp();

            return base.OnDisconnected();
        }

        public void EnterRoom(string roomName)
        {
            var room = _service.BindPlayerInRoom(Context.ConnectionId, Clients.Caller.PlayerName, roomName);
            Groups.Add(this.Context.ConnectionId, roomName);
            Clients.All.changeRoom(_service.GetListRooms());
            if (room.IsFull())
                Clients.Group(roomName).startGame(room.PlayerCurrent);
            
        }

        public void MakeMove(string connectionId, string roomName, string cel)
        {
            var room = _service.GetRoom(roomName);
            var status = room.MakeMove(connectionId, cel);
            var player = _service.GetPlayer(connectionId);
            Clients.Group(roomName).cellMarker(cel, player.Symbol);

            if (status == StatusGame.HasWinner)
                ProcessToWinner(roomName, room);
            else if (status == StatusGame.HasTie)
                ProcessToTie(roomName, room);
            else
                ProcessToContinues(roomName, room);
        }

        private void ProcessToContinues(string roomName, Room room)
        {
            Clients.Group(roomName).prepareMove(room.PlayerCurrent);
        }

        private void ProcessToTie(string roomName, Room room)
        {
            Clients.Group(roomName).finishedGame();
            Clients.Group(roomName).startGame(room.PlayerCurrent);
        }

        private void ProcessToWinner(string roomName, Room room)
        {
            Clients.Group(roomName).playerWinner(room.LastPlayerWinner);
            Clients.Group(roomName).startGame(room.PlayerCurrent);
            Clients.All.changeRoom(_service.GetListRooms());
        }
    }
}