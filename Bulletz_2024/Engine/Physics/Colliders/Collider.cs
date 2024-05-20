using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Boidz
{
    abstract class Collider
    {
        public Vector2 Offset;
        public RigidBody RigidBody;
        public Vector2 Position { get { return RigidBody.Position + Offset; } }

        public Collider(RigidBody owner)
        {
            RigidBody = owner;
            Offset = Vector2.Zero;
        }

        public abstract bool Collides(Collider collider);
        public abstract bool Collides(CircleCollider collider);
        public abstract bool Collides(BoxCollider collider);
        public abstract bool Collides(CompoundCollider collider);

        public abstract bool Contains(Vector2 point);
    }
}
