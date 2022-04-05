using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoLike.Classes
{
    public class Player
    {
        public string connectionId { get; set; }

        public string name { get; set; }

        public int maxBlue { get; set; }

        public int maxRed { get; set; }

        public int maxGreen { get; set; }

        public int maxYellow { get; set; }

        public Deck hand { get; set; }


        public Player(string connectionId, string name)
        {
            this.connectionId = connectionId;
            this.name = name;
            maxRed = 0;
            maxGreen = 0;
            maxBlue = 0;
            maxYellow = 0;
        }

        public Player()
        {
            maxRed = 0;
            maxGreen = 0;
            maxBlue = 0;
            maxYellow = 0;
        }

        public override string ToString()
        {
            return "Joueur : " + this.name + " - " + this.connectionId + " - b : " + this.maxBlue + " - r : " + this.maxRed + " - v : " + this.maxGreen + " - j : " + this.maxYellow;
        }
    }
}
