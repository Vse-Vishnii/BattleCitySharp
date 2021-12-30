namespace BattleCitySharp
{
    public class Grid
    {
        //Переделать!!!!!!!!!!!
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
