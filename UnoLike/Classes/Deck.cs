﻿using System;
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
            int[] cards = {1, 1, 1, 1,
                2, 2, 2, 2,
                3, 3, 3, 3,
                4, 4, 4, 4,
                5, 5, 5, 5,
                6, 6, 6, 6,
                7, 7, 7, 7,
                9, 9, 9, 9, 9,
                10, 10, 10, 10, 10,
                11, 11, 11, 11, 11,
                12, 12, 12, 12, 12,
                13, 13, 13, 13, 13,
                14, 14, 14, 14, 14,
                15, 15, 15, 15, 15,
                16, 16, 16, 16, 16,
                17, 17, 17, 17, 17, 17,
                18, 18, 18, 18, 18, 18,
                19, 19, 19, 19, 19, 19,
                20, 20, 20, 20, 20, 20,
                21, 21, 21, 21, 21, 21,
                22, 22, 22, 22, 22, 22,
                23, 23, 23, 23, 23, 23,
                24, 24, 24, 24, 24, 24,
                25, 25,
                26, 26,
                27, 27,
                28, 28,
                29, 30, 31, 32, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };

            this.cardList = new List<int>(cards);
        }
    }
}
