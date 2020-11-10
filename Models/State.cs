using System;
namespace Pig.Models
{
    public class State
    {
        public int TurnNumber { get; set; }
        public int P1Score { get; set; }
        public int P2Score { get; set; }
        public int CurrentDiceValue { get; set; }

        public State()
        {
            this.TurnNumber = 0;

            this.P1Score = 0;
            this.P2Score = 0;

            this.CurrentDiceValue = 0;
        }
    }
}
