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

        public Player(string connectionId, string name)
        {
            this.connectionId = connectionId;
            this.name = name;
        }

        public override string ToString()
        {
            return "Joueur : " + this.name + " - " + this.connectionId;
        }
    }
}
