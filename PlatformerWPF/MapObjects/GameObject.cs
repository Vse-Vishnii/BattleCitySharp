using System.Linq;
using System.Numerics;

namespace BattleCitySharp
{
    public abstract class GameObject
    {
        public Transform Transform { get; set; } = new Transform();
        public Collider Collider { get; private set; }    
        public ObjectType GameObjectType { get; protected set; }
        public int TeamId { get; set; } = 0;

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void ColliderEnter(Collider collider) { }
        public virtual void ColliderStay(Collider collider) { }
        public virtual void ColliderExit(Collider collider) { }

        public void CreateGameObjectProperties(Vector2 point, Direction rotation, int size, ColliderShape shape)
        {
            Transform.Position = point;
            Transform.Direction = rotation;
            Transform.Size = size;
            Collider = new Collider(this, shape);
        }

        public T GetComponent<T>()
        {
            var property = GetType().GetProperties()
                            .Where(o => o.PropertyType == typeof(T))
                            .FirstOrDefault();
            return property != null ? (T)property.GetValue(this) : default(T);
        }
    }
}
