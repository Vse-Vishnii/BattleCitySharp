﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public struct Cell
    {
        //Переделать!!!!!!!!!!!
        public int X { get; set; }
        public int Y { get; set; }
        public static int CellSize { get; } = 70;

        public Cell(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
