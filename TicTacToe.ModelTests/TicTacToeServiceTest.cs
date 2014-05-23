using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Model.Service;

namespace TicTacToe.ModelTests
{
    [TestClass]
    public class TicTacToeServiceTest
    {
        [TestMethod]
        public void ShouldGetAllRoomsOfGame()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            Assert.IsTrue(rooms.Count > 0);
        }

        [TestMethod]
        public void ShouldCanBindTwoPlayerInRoom()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();

            try
            {
                service.BindPlayerInRoom("1", "test1", rooms[0].Name);
                service.BindPlayerInRoom("2", "test2", rooms[0].Name);
            }
            catch
            {
                Assert.Fail("Exception in BindPlayerInRoom");
            }
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ShouldNotCanBindARoomIfAlreadyHaveTwoPlayers()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            var room = rooms[0];
            service.BindPlayerInRoom("1", "test1", room.Name);
            service.BindPlayerInRoom("2", "test2", room.Name);
            service.BindPlayerInRoom("3", "test3", rooms[0].Name);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ShouldNotCanBindPlayerMoreThanOneRoom()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            service.BindPlayerInRoom("1", "test1", rooms[0].Name);
            service.BindPlayerInRoom("1", "test1", rooms[1].Name);
        }

        [TestMethod]
        public void ShouldPlayerMakeMove()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            service.BindPlayerInRoom("1", "test1", rooms[0].Name);
            service.BindPlayerInRoom("2", "test2", rooms[0].Name);
            service.MakeMove("1", rooms[0].Name, "a1");
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ShouldNotPlayerMakeMoveWhenItIsNotYourTime()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            service.BindPlayerInRoom("1", "test1", rooms[0].Name);
            service.BindPlayerInRoom("2", "test2", rooms[0].Name);
            service.MakeMove("2", rooms[0].Name, "a1");
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void ShouldNotPlayerMakeMoveWhenCelAlreadyWasUsed()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            service.BindPlayerInRoom("1", "test1", rooms[0].Name);
            service.BindPlayerInRoom("2", "test2", rooms[0].Name);
            service.MakeMove("1", rooms[0].Name, "a1");
            service.MakeMove("2", rooms[0].Name, "a1");
        }

        [TestMethod]
        public void ShouldRemovePlayerRoom()
        {
            var service = TicTacToeService.GetInstance();
            var rooms = service.GetListRooms();
            service.BindPlayerInRoom("1", "test1", rooms[0].Name);
            service.BindPlayerInRoom("2", "test2", rooms[0].Name);
            rooms[0].RemovePlayer("1");
        }
    }
}
