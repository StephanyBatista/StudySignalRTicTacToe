using System;

namespace TicTacToe.Model.Entity
{
    public enum StatusGame
    {
        Continues,
        HasWinner,
        HasTie
    }
    
    public class Room
    {
        public string Name { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player PlayerCurrent { get; private set; }
        public Player LastPlayerWinner { get; private set; }
        
        private Game _game;

        public Room()
        {
            NewGame();
        }

        private void NewGame()
        {
            _game = new Game();
        }

        public void BindPlayer(string id, string playerName)
        {
            if (IsFull())
                throw new Exception("Room full");

            if (PlayerOne == null)
            {
                PlayerOne = new Player(id, playerName, "X", Name);
                PlayerCurrent = PlayerOne;
            }
            else
                PlayerTwo = new Player(id, playerName, "0", Name);
        }

        public bool IsFull()
        {
            return PlayerOne != null && PlayerTwo != null;
        }

        public StatusGame MakeMove(string playerId, string cel)
        {
            if(PlayerCurrent.Id != playerId)
                throw new Exception("It is not the corret player");

            _game.Move(cel, PlayerCurrent.Symbol);
            NextPlayer();

            if (_game.HasWinner())
                return ProcessGameWithWinner();
            else if (_game.HasTie())
                return ProcessGameWithTied();

            return StatusGame.Continues;
        }

        private StatusGame ProcessGameWithTied()
        {
            NewGame();
            return StatusGame.HasTie;
        }

        private StatusGame ProcessGameWithWinner()
        {
            LastPlayerWinner = ConsolidateWinner();
            NewGame();
            return StatusGame.HasWinner;
        }

        private Player ConsolidateWinner()
        {
            var symbolWinner = _game.SymbolWinner;

            if (PlayerOne.Symbol == symbolWinner)
            {
                PlayerOne.AddWins();
                return PlayerOne;
            }
            else
            {
                PlayerTwo.AddWins();
                return PlayerTwo;
            }
        }

        private void NextPlayer()
        {
            if (PlayerCurrent == PlayerOne)
                PlayerCurrent = PlayerTwo;
            else
                PlayerCurrent = PlayerOne;
        }

        public void RemovePlayer(string playerId)
        {
            if (PlayerOne != null && PlayerOne.Id == playerId)
            {
                PlayerOne = null;
                ResetPlayer(PlayerTwo, true);
            }
            else if (PlayerTwo != null && PlayerTwo.Id == playerId)
            {
                PlayerTwo = null;
                ResetPlayer(PlayerOne, false);
            }
            else
                throw new Exception("Player not found");
        }

        private void ResetPlayer(Player player, bool fistPlayer)
        {
            if (player == null) return;
            player = new Player(player.Id, player.Name, fistPlayer ? "X" : "0", Name);
        }
    }
}
