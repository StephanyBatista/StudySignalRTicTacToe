namespace TicTacToe.Model.Entity
{
    public class Player
    {
        public Player(string id, string name, string symbol, string roomName)
        {
            Id = id;
            Name = name;
            Symbol = symbol;
            Wins = 0;
            RoomName = roomName;
        }

        public string Id { get; set; }
        public string Name { get; private set; }
        public string Symbol { get; private set; }
        public int Wins { get; private set; }
        public string RoomName { get; set; }

        public void AddWins()
        {
            Wins++;
        }
    }
}
