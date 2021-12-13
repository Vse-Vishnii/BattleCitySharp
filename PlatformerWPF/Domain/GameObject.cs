using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleCitySharp
{
    public abstract class GameObject
    {
        public Transform Transform { get; private set; } = new Transform();
        public Collider Collider { get; private set; }
        public Image ObjectGraphic { get; private set; } = new Image();

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void ColliderEnter(Collider collider) { }
        public virtual void ColliderStay(Collider collider) { }
        public virtual void ColliderExit(Collider collider) { }

        public void CreateGameObjectProperties(Cell cell, Direction rotation)
        {
            Transform.Position = new Point(cell.X * 70, cell.Y * 70);
            Transform.Direction = rotation;
            Collider = new Collider(this);
            Start();
        }        
    }
}
