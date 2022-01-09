using System.Collections.Generic;
using System.Numerics;

namespace BattleCitySharp
{
    public class Grid
    {
        public class Cell
        {
            public int X { get; }
            public int Y { get; }
            public ObjectType Type { get; private set; }

            public Cell(int x, int y, ObjectType objectType = ObjectType.Empty)
            {
                X = x;
                Y = y;
                Type = objectType;
            }

            public void ChangeObjectType<T>(T original) where T : GameObject
            {
                if (original is MovingObject)
                    return;
                Type = original is Box ? ObjectType.Wall : original is Base ? ObjectType.Base : Type;
            }

            public void ClearCell()
            {
                Type = ObjectType.Empty;
            }
        }

        private Cell[,] map;

        public static int CellSize { get => 70; }
        public static Grid Instance { get; } = new Grid();

        public Cell EmptyCell { get; }
        public int SizeX { get => 13; }
        public int SizeY { get => 13; }

        private Grid()
        {
            map = new Cell[SizeX, SizeY];
            for (var x = 0; x < SizeX; x++)
                for (var y = 0; y < SizeY; y++)
                {
                    map[x, y] = new Cell(x * CellSize, y * CellSize);
                }
            EmptyCell = new Cell(-CellSize, -CellSize);
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0)
                    return Instance.EmptyCell;
                return map[x, y];
            }
        }

        public static void ClearCell(Vector2 position)
        {
            var x = (int)(position.X / CellSize);
            var y = (int)(position.Y / CellSize);            
            Instance[x, y].ClearCell();
        }
    }
}
