namespace BattleCitySharp
{
    public class Bullet : MovingObject
    {
        private float speed = 10f;
        private int damage = 2;
        private int lifeTime = 3;
        private float time;
        private bool destroyed = false;

        public Bullet(int teamId)
        {            
            GameObjectType = ObjectType.Bullet;
            TeamId = teamId;
        }

        public override void Start()
        {
            time = lifeTime;
            Collider.IsTrigger = true;
        }

        public override void Update()
        {
            time -= Time.DeltaTime;
            if (time <= 0)
            {
                DestroyBullet();
                return;
            }                
            if (Collider.CanMove() && !destroyed)
                Move(Transform.MoveDirection, speed, Transform.Size / 2);
            else
                DestroyBullet();
        }

        public override void ColliderEnter(Collider collider)
        {
            if (collider.GameObject is Bullet || collider.IsTrigger)
                return;
            var gameObject = collider.GameObject;
            var health = gameObject.GetComponent<Health>();
            if (health != null && TeamId != gameObject.TeamId)
            {
                health.TakeDamage(damage);
                DestroyBullet();
            }
        }
        
        public void DestroyBullet()
        {
            Core.Destroy(this);
            destroyed = true;
        }
    }
}
