using System;
using System.Linq;
using System.Security.Cryptography;

namespace TicTacToe.Model.Entity
{
    public class Game
    {
        readonly string[,] _matriz = new string[3,3];
        public string SymbolWinner { get; set; }

        public void Move(string cel, string symbol)
        {
            ValidateCel(cel);
            
            if (cel == "a1")
                _matriz[0, 0] = symbol;
            else if (cel == "a2")
                _matriz[0, 1] = symbol;
            else if (cel == "a3")
                _matriz[0, 2] = symbol;

            else if (cel == "b1")
                _matriz[1, 0] = symbol;
            else if (cel == "b2")
                _matriz[1, 1] = symbol;
            else if (cel == "b3")
                _matriz[1, 2] = symbol;

            else if (cel == "c1")
                _matriz[2, 0] = symbol;
            else if (cel == "c2")
                _matriz[2, 1] = symbol;
            else if (cel == "c3")
                _matriz[2, 2] = symbol;

            else
                throw new Exception("Key not found");
        }

        public bool HasWinner()
        {
            var symbolWinner = _matriz[0, 0];
            if(!string.IsNullOrEmpty(_matriz[0, 0]) && _matriz[0, 0] == _matriz[0, 1] && _matriz[0, 1] == _matriz[0, 2])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[0, 0];
            if (!string.IsNullOrEmpty(_matriz[0, 0]) && _matriz[0, 0] == _matriz[1, 0] && _matriz[1, 0] == _matriz[2, 0])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[2, 0];
            if (!string.IsNullOrEmpty(_matriz[2, 0]) && _matriz[2, 0] == _matriz[2, 1] && _matriz[2, 1] == _matriz[2, 2])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[0, 2];
            if (!string.IsNullOrEmpty(_matriz[0, 2]) && _matriz[0, 2] == _matriz[1, 2] && _matriz[1, 2] == _matriz[2, 2])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[0, 1];
            if (!string.IsNullOrEmpty(_matriz[0, 1]) && _matriz[0, 1] == _matriz[1, 1] && _matriz[1, 1] == _matriz[2, 1])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[0, 0];
            if (!string.IsNullOrEmpty(_matriz[0, 0]) && _matriz[0, 0] == _matriz[1, 1] && _matriz[1, 1] == _matriz[2, 2])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            symbolWinner = _matriz[2, 0];
            if (!string.IsNullOrEmpty(_matriz[2, 0]) && _matriz[2, 0] == _matriz[1, 1] && _matriz[1, 1] == _matriz[0, 2])
            {
                SymbolWinner = symbolWinner;
                return true;
            }

            return false;
        }

        public bool HasTie()
        {
            for(int i = 0; i < 3; i++)
                for(int j = 0; j < 3; j++)
                    if (string.IsNullOrEmpty(_matriz[i, j]))
                        return false;
            return true;
        }

        public void ValidateCel(string cel)
        {
            if (cel == "a1" && !string.IsNullOrEmpty(_matriz[0, 0]))
                 throw new Exception("Cel already moved");
            else if (cel == "a2" && !string.IsNullOrEmpty(_matriz[0, 1]))
                throw new Exception("Cel already moved");
            else if (cel == "a3" && !string.IsNullOrEmpty(_matriz[0, 2]))
                throw new Exception("Cel already moved");

            if (cel == "b1" && !string.IsNullOrEmpty(_matriz[1, 0]))
                throw new Exception("Cel already moved");
            else if (cel == "b2" && !string.IsNullOrEmpty(_matriz[1, 1]))
                throw new Exception("Cel already moved");
            else if (cel == "b3" && !string.IsNullOrEmpty(_matriz[1, 2]))
                throw new Exception("Cel already moved");

            if (cel == "c1" && !string.IsNullOrEmpty(_matriz[2, 0]))
                throw new Exception("Cel already moved");
            else if (cel == "c2" && !string.IsNullOrEmpty(_matriz[2, 1]))
                throw new Exception("Cel already moved");
            else if (cel == "c3" && !string.IsNullOrEmpty(_matriz[2, 2]))
                throw new Exception("Cel already moved");
        }
    }
}
