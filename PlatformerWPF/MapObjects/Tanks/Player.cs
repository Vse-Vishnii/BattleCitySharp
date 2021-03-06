using System.Numerics;
using System.Windows.Input;

namespace BattleCitySharp
{
    public class Player : Tank
    {
        public Health Health { get; }

        private Input input;

        public Player(Input input)
        {
            this.input = input;
            currentCooldown = 0;
            GameObjectType = ObjectType.Player;
            Health = new Health(5, this);
            TeamId = 1;
        }

        public override void Update()
        {
            ProcessMoving();
            ProcessShooting();
        }

        protected override void ProcessMoving()
        {
            var vertical = input.GetAxis("Vertical");
            var horizontal = input.GetAxis("Horizontal");
            moveDir = new Vector2(horizontal, vertical);
            base.ProcessMoving();
        }

        protected override void ProcessShooting()
        {
            currentCooldown -= Time.DeltaTime;
            if (input.GetPressedButton(Key.Space))
            {
                if (currentCooldown <= 0)
                {
                    base.ProcessShooting();
                    currentCooldown = cooldown;
                }
            }
        }
    }
}
