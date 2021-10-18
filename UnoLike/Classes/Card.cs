using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoLike.Classes
{
    public class Card
    {
        public Card(int id, Color color, int num)
        {
            this.id = id;
            this.color = color;
            this.num = num;
        }

        public int id { get; set; }
        public Color color { get; set; }
        public int num { get; set; }
        public string description { get; set; }
        public Card possiblePlay { get; set; }

        
    }
}
