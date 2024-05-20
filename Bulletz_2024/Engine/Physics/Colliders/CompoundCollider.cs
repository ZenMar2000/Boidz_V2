using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    class CompoundCollider : Collider
    {
        public Collider BoundingCollider;
        protected List<Collider> colliders;

        public CompoundCollider(RigidBody owner, Collider boundingCollider) : base(owner)
        {
            BoundingCollider = boundingCollider;
            colliders = new List<Collider>();
            Offset = Vector2.Zero;
        }

        public virtual bool InnerCollidersCollide(Collider collider)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (collider.Collides(colliders[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void AddCollider(Collider collider)
        {
            colliders.Add(collider);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleCollider other)
        {
            return (other.Collides(BoundingCollider) && InnerCollidersCollide(other));
        }

        public override bool Collides(BoxCollider other)
        {
            return (other.Collides(BoundingCollider) && InnerCollidersCollide(other));
        }

        public override bool Collides(CompoundCollider other)
        {
            if (BoundingCollider.Collides(other.BoundingCollider))
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    for (int j = 0; j < other.colliders.Count; j++)
                    {
                        if (colliders[i].Collides(other.colliders[j]))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override bool Contains(Vector2 point)
        {
            if (BoundingCollider.Contains(point))
            {
                for (int i = 0; i < colliders.Count; i++)
                {
                    if (colliders[i].Contains(point))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
