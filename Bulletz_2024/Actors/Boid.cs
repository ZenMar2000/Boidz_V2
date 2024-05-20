using OpenTK;
using System.Collections.Generic;

namespace Boidz
{
    class Boid : Actor
    {
        private float speedMultiplier = 1;
        private float ratioMultiplier = 1;
        public Boid() : base("boid")
        {
            IsActive = true;
            speed = ((PlayScene)Game.CurrentScene).Speed;

            SetRandomDirection();

            Reset();

            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }

        private void SetRandomDirection()
        {
            float x;
            float y;
            do
            {
                x = RandomGenerator.GetRandomFloat(-1, 1);
                y = RandomGenerator.GetRandomFloat(-1, 1);
            } while (x == y && x == 0);

            RigidBody.Velocity = new Vector2(x, y);

            RigidBody.Velocity.Normalize();
            RigidBody.Velocity *= speed;
        }

        public override void Update()
        {
            BoidLogic();

            sprite.position += RigidBody.Velocity;
            Forward = RigidBody.Velocity;

            CheckWindowBorders();
        }

        private void CheckWindowBorders()
        {
            //X Border
            if (Position.X - HalfWidth > Game.Window.Width)
            {
                sprite.position.X = 0 - HalfWidth;

            }
            else if (Position.X + sprite.Width * 0.5f <= 0)
            {
                sprite.position.X = Game.Window.Width + HalfWidth;
            }

            //Y border
            if (Position.Y - HalfHeight > Game.Window.Height)
            {
                sprite.position.Y = 0 - HalfHeight;
            }
            else if (Position.Y + HalfHeight <= 0)
            {
                sprite.position.Y = Game.Window.Height + HalfHeight;
            }
        }

        private void BoidLogic()
        {
            Vector2 newDir = RigidBody.Velocity;
            List<Boid> neigbours = new List<Boid>();

            foreach (Boid b in ((PlayScene)Game.CurrentScene).boids)
            {
                if (Vector2.Distance(Position, b.Position) <= ((PlayScene)Game.CurrentScene).CheckRange && b != this)
                {
                    neigbours.Add(b);
                }
            }

            if (neigbours.Count > 0)
            {
                if (((PlayScene)Game.CurrentScene).SpeedMultiplierEnabled && neigbours.Count > 0)
                {
                    speedMultiplier = 1 + ((PlayScene)Game.CurrentScene).SpeedMultiplier * neigbours.Count * 0.25f;
                    ratioMultiplier = 1 + ((PlayScene)Game.CurrentScene).SpeedMultiplier * neigbours.Count * 0.75f;
                }
                else
                {
                    speedMultiplier = 1;
                    ratioMultiplier = 1;
                }

                AlignmentLogic(ref newDir, neigbours);
                CohesionLogic(ref newDir, neigbours);
                SeparationLogic(ref newDir, neigbours);

                RigidBody.Velocity = Vector2.Normalize(Vector2.Lerp(RigidBody.Velocity, newDir, ((PlayScene)Game.CurrentScene).TurnRatio)) * (speed * (speedMultiplier));
            }
        }

        private void AlignmentLogic(ref Vector2 newDir, List<Boid> neigbours)
        {
            Vector2 dir = newDir;
            foreach (Boid b in neigbours)
            {
                if (b != this)
                {
                    dir += b.RigidBody.Velocity;
                }
            }

            newDir = Vector2.Lerp(newDir, dir.Normalized() * speed, ((PlayScene)Game.CurrentScene).AlignmentRatio * ratioMultiplier);

        }

        private void CohesionLogic(ref Vector2 newDir, List<Boid> neigbours)
        {
            Vector2 groupCenter = Vector2.Zero;
            foreach (Boid b in neigbours)
            {
                groupCenter.X += b.Position.X;
                groupCenter.Y += b.Position.Y;
            }

            groupCenter.X /= neigbours.Count;
            groupCenter.Y /= neigbours.Count;

            Vector2 currentPosToCenter = groupCenter - Position;
            newDir = Vector2.Lerp(newDir, currentPosToCenter.Normalized() * speed, ((PlayScene)Game.CurrentScene).CohesionRatio * ratioMultiplier);
        }

        private void SeparationLogic(ref Vector2 newDir, List<Boid> neigbours)
        {
            //TODO Pick only the closest one
            bool found = false;
            Vector2 escapeDirection = newDir;

            Boid closestBoid = null;
            float closestDistance = Game.Window.Width + HalfWidth * 2;

            foreach (Boid b in neigbours)
            {
                float dist = Vector2.Distance(b.Position, Position);
                if (dist <= ((PlayScene)Game.CurrentScene).MinDistance && dist < closestDistance)
                {
                    found = true;
                    closestBoid = b;
                    closestDistance = dist;
                }
            }

            if (found)
            {
                float distPercent = 1 - ((((PlayScene)Game.CurrentScene).MinDistance - Vector2.Distance(closestBoid.Position, Position)) / ((PlayScene)Game.CurrentScene).MinDistance);
                escapeDirection -= (Position - closestBoid.Position) * distPercent * ((PlayScene)Game.CurrentScene).SeparationMultiplier;

                escapeDirection = escapeDirection.Normalized() * speed;
                newDir = Vector2.Lerp(newDir, newDir - (escapeDirection.Normalized() * speed), ((PlayScene)Game.CurrentScene).SeparationRatio * (ratioMultiplier * 0.65f)).Normalized();
            }
        }
    }
}
