namespace BattleCitySharp
{
    public class AidKit : Bonus
    {
        private int heal = 5;
        public override void ColliderEnter(Collider collider)
        {
            var obj = collider.GameObject;
            if(obj is Player)
            {
                obj.GetComponent<Health>().TakeDamage(-heal);
                Core.Destroy(this);
            }
        }
    }
}
