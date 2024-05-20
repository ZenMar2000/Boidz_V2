using OpenTK;
using System.Collections.Generic;

namespace Boidz
{
    class Boid : Actor
    {
        #region variables
        private float turnRatio = 0.1f;

        private int checkRange = 75;
        private float minDistance = 40;

        private float alignmentRatio = 0.0275f;
        private float cohesionRatio = 0.012f;
        private float separationRatio = 0.0475f;
        private float separationMultiplier = 2;
        #endregion

        #region properties

        #endregion

        public Boid() : base("boid")
        {
            IsActive = true;
            speed = 0.35f;

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
                if (Vector2.Distance(Position, b.Position) <= checkRange && b != this)
                {
                    neigbours.Add(b);
                }
            }

            if (neigbours.Count > 0)
            {
                AlignmentLogic(ref newDir, neigbours);
                CohesionLogic(ref newDir, neigbours);
                SeparationLogic(ref newDir, neigbours);

                RigidBody.Velocity = Vector2.Normalize(Vector2.Lerp(RigidBody.Velocity, newDir, turnRatio)) * speed;
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

            newDir = Vector2.Lerp(newDir, dir.Normalized() * speed, alignmentRatio);

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
            newDir = Vector2.Lerp(newDir, currentPosToCenter.Normalized() * speed, cohesionRatio);
        }

        private void SeparationLogic(ref Vector2 newDir, List<Boid> neigbours)
        {
            bool found = false;
            Vector2 escapeDirection = newDir;
            foreach (Boid b in neigbours)
            {
                if (Vector2.Distance(b.Position, Position) <= minDistance)
                {
                    found = true;
                    float distPercent = 1 - ((minDistance - Vector2.Distance(b.Position, Position)) / minDistance);

                    escapeDirection -= (Position - b.Position) * distPercent * separationMultiplier;
                }
            }

            if (found)
            {
                escapeDirection = escapeDirection.Normalized() * speed;
                newDir = Vector2.Lerp(newDir, newDir - (escapeDirection.Normalized() * speed), separationRatio).Normalized();
            }
        }

    }

    //struct FuzzyLogic
    //{
    //    private double distanceFL;
    //    private double infectionTimerFL;
    //    private double directionAffinityFL;

    //    public int index;
    //    public double result;
    //    public FuzzyLogic(Ball currentBall, Ball targetBall, int index)
    //    {
    //        Vector2 dist = targetBall.Position - currentBall.Position;

    //        distanceFL = 1 - (dist.LengthSquared / currentBall.InfectionRadius);
    //        infectionTimerFL = 1 - targetBall.GetInfectionTimer();
    //        directionAffinityFL = (Math.Cos(currentBall.RigidBody.Velocity.X) - Math.Cos(targetBall.RigidBody.Velocity.X)) - 1;

    //        result = distanceFL + infectionTimerFL + directionAffinityFL;
    //        this.index = index;
    //    }
    //}
}
