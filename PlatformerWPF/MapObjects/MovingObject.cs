using System.Numerics;

namespace BattleCitySharp
{
    public class MovingObject : GameObject
    {
        protected void Move(Vector2 moveDir, float speed, int size = 35)
        {
            if (moveDir.X != 0 || moveDir.Y != 0)
            {
                Drawer.RotateObject(this, size, size);
                if (Collider.CanMove())
                {
                    Drawer.Move(this, moveDir, speed);
                }
            }
        }
    }
}
