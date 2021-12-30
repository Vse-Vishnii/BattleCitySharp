namespace BattleCitySharp
{
    public class Grid
    {
        public struct Cell
        {
            //Переделать!!!!!!!!!!!
            public int X { get; }
            public int Y { get; }
            public ObjectType ObjectType { get; }

            public Cell(int x, int y, ObjectType objectType = ObjectType.Empty)
            {
                X = x;
                Y = y;
                ObjectType = objectType;
            }
        }

        //Переделать!!!!!!!!!!!
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

        public Cell this[int x, int y] => map[x, y];
    }
}
