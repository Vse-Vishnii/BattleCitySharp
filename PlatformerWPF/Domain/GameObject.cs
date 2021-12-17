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

        public void CreateGameObjectProperties(Vector2 point, Direction rotation, Image image)
        {
            Transform.Position = point;
            Transform.Direction = rotation;
            Transform.Size = 70;
            ObjectGraphic = image;
            Collider = new Collider(this);
            Start();
        }        
    }
}
