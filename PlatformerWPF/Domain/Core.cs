using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public static class Core
    {
        public static object Instantiate(object original, Cell cell, Direction rotation)
        {
            Runner.objects.Add((GameObject)original);
            Runner.objects.Select(o => { o.Collider.Collisions.Add(false); return o; });

            int cellSize = 70;



            return original;
        }
    }
}
