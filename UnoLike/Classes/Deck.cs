using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoLike.Classes
{
    public class Deck
    {
        public List<int> cardList { get; set; }

        public Deck()
        {
            this.cardList = new List<int>();
        }

        public void initializeDeck()
        {
            int[] cards = {
            //7
                1,1,1,1,
                2,2,2,2,
                3,3,3,3,
                4,4,4,4,
            //6
                5,5,5,5,
                6,6,6,6,
                7,7,7,7,
                8,8,8,8,
            //5
                9,9,9,9,9,
                10,10,10,10,10,
                11,11,11,11,11,
                12,12,12,12,12,
            //4
                13,13,13,13,13,
                14,14,14,14,14,
                15,15,15,15,15,
                16,16,16,16,16,
            //3
                17,17,17,17,17,17,
                18,18,18,18,18,18,
                19,19,19,19,19,19,
                20,20,20,20,20,20,
            //2
                21,21,21,21,21,21,
                22,22,22,22,22,22,
                23,23,23,23,23,23,
                24,24,24,24,24,24,
            //1
                25,25,
                26,26,
                27,27,
                28,28,
            //Main gagnante
                29,29,
                30,30,
                31,31,
            //Camion
                32,32,32,
            //Dévoiler
                33,33,33,
            //Désigner
                34,34,34,
            //Loto
                35,35,35,35,35
            };

            Random rnd = new Random();
            cards = cards.OrderBy(x => rnd.Next()).ToArray();

            this.cardList = new List<int>(cards);
        }
    }
}
