using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BattleCitySharp
{
    public class Player : MonoBehavior
    {
        //[SerializeField]
        private float Speed;
        //[SerializeField]
        private float RotateSpeed;
        //[SerializeField]
        private float Gravity;

        private Transform tank;
        private Rotation moveDir;
        private Rotation rotateDir;
        private float slowSpeed;
        private int backMultiplier;
        // Start is called before the first frame update
        public Player(Transform transform)
        {
            slowSpeed = Speed / 2;
            tank = transform;
            rotateDir = transform.Rotation;
        }

        // Update is called once per frame
        public override void Update()
        {
            var vertical = Input.GetAxis("Vertical");
            backMultiplier = vertical < 0 ? -1 : 1;
            moveDir = tank.forward * vertical * Speed * Time.DeltaTime;
            characterController.Move(moveDir);
        }

        public override void LateUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal") * backMultiplier;
            tank.Rotation = rotateDir;
        }

        public void SlowDown()
        {
            Speed = slowSpeed;
        }

        public void SpeedUp()
        {
            Speed = slowSpeed * 2;
        }
    }
}
