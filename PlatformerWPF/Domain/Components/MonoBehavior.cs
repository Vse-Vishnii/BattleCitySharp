using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCitySharp
{
    public abstract class MonoBehavior
    {
        protected Transform Transform { get; set; }

        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void ColliderEnter(Collider collider) { }
        public virtual void ColliderStay(Collider collider) { }
        public virtual void ColliderExit(Collider collider) { }

        protected object Instantiate(object original, Point position, Quaternion rotation)
        {
            Runner.objects.Add((MonoBehavior)original);



            return original;
        }
    }
}
