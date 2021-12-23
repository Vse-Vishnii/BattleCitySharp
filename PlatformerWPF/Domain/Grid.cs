namespace BattleCitySharp
{
    public class Grid
    {
        public static Cell[,] Map { get; private set; }

        // public Grid(Cell[,] map)
        // {
        //     Map = map;
        // }

        public static void SetMap(Cell[,] map)
        {
            Map = map;
        }
    }
}