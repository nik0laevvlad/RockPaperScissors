using System;

namespace RockPaperScissors
{
    class Game
    {
        private LoosersTable loosersTable;
        public Game(LoosersTable table) { loosersTable = table; }
        public string CalculateResult(int computerChoise, int userChoise)
        {
            if (computerChoise == userChoise)
                return "DRAW!";
            if (Array.IndexOf(loosersTable.calculateLoosers(userChoise), computerChoise) != -1)
                return "You are WIN!";
            return "You are LOSE!";
        }
    }
}
