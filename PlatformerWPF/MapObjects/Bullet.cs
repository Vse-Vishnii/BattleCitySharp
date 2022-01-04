using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public class Bullet : MovingObject
    {
        private float speed = 10f;
        private int damage = 2;
        private int lifeTime = 3;
        private float time;

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
                Core.Destroy(this);
            if (Collider.CanMove())
                Move(Transform.MoveDirection, speed, Transform.Size / 2);
            else
                Core.Destroy(this);
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
                Core.Destroy(this);
            }
        }        
    }
}
