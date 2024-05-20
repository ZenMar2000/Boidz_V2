using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2; } }
        public float Height { get { return halfHeight * 2; } }

        public BoxCollider(RigidBody owner, int w, int h) : base(owner)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
            //DebugMngr.AddItem(this);
        }

        public BoxCollider(RigidBody owner, float halfWidth, float halfHeight) : base(owner)
        {
            this.halfWidth = halfWidth;
            this.halfHeight = halfHeight;
            //DebugMngr.AddItem(this);
        }

        public override bool Collides(Collider other)
        {
            return other.Collides(this);
        }

        public override bool Collides(CircleCollider other)
        {
            float deltaX = other.Position.X - Math.Max(Position.X - halfWidth, Math.Min(other.Position.X, Position.X + halfWidth));
            float deltaY = other.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(other.Position.Y, Position.Y + halfHeight));

            return (deltaX * deltaX + deltaY * deltaY) < (other.Radius * other.Radius);
        }

        public override bool Collides(BoxCollider other)
        {
            float deltaX = other.Position.X - Position.X;
            float deltaY = other.Position.Y - Position.Y;

            return
                (Math.Abs(deltaX) <= halfWidth + other.halfWidth) &&
                (Math.Abs(deltaY) <= halfHeight + other.halfHeight);
        }

        public override bool Contains(Vector2 point)
        {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

        public override bool Collides(CompoundCollider other)
        {
            return other.Collides(this);
        }
    }
}
