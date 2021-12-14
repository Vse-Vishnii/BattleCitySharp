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
        public Transform Transform { get; set; } = new Transform();
        public Collider Collider { get; private set; }
        public Image ObjectGraphic { get; private set; } = new Image();
        
        public ObjectType GameObjectType { get; protected set; }

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void ColliderEnter(Collider collider) { }
        public virtual void ColliderStay(Collider collider) { }
        public virtual void ColliderExit(Collider collider) { }

        public GameObject CreateGameObjectProperties(Cell cell, Direction rotation, Image image)
        {
            Transform.Position = new Vector2(cell.X * 70, cell.Y * 70);
            Transform.Direction = rotation;
            ObjectGraphic = image;
            Collider = new Collider(this);
            Start();
            return this;
        }        
    }
}
