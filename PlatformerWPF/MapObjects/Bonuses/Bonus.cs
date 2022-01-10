namespace BattleCitySharp
{
    public abstract class Bonus : GameObject
    {
        public Bonus()
        {
            GameObjectType = ObjectType.Bonus;
        }

        public override void Start()
        {
            Collider.IsTrigger = true;
        }
    }
}
