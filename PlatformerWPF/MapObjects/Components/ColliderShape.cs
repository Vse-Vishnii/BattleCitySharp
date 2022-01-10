namespace BattleCitySharp
{
    public class ColliderShape
    {
        public double Left { get => transform.Position.X; }
        public double Top { get => transform.Position.Y; }
        public double Width { get; }
        public double Height { get; }

        private readonly Transform transform;

        public ColliderShape(double width, double height, Transform transform)
        {
            Width = width;
            Height = height;
            this.transform = transform;
        }
    }
}
