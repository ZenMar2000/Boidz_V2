﻿using OpenTK;
using System.Collections.Generic;

namespace Boidz
{

    class Boid : Actor
    {

        private float smoothness = 0.775f; // 1 = original(no lerp over lerp); 0 = broken (ignore everything)
        private float TurnRatio = 0.1f;

        private int CheckRange = 75;
        private float MinDistance = 27.5f;

        private float AlignmentRatio = 0.03f;
        private float CohesionRatio = 0.023f;
        private float SeparationRatio = 0.065f;
        private float SeparationMultiplier = 1.25f;

        private float baseSpeed = 0.4f;
        private Vector2 targetSpeed;
        private float SpeedMultiplierIncrement = 0.2f;

        private float speedMultiplier = 1;
        private float ratioMultiplier = 1;

        private float alignmentMultiplier = 1;
        private float cohesionMultiplier = 1;
        private float separationMultiplier = 1;



        public Boid() : base("boid")
        {
            IsActive = true;
            currentSpeed = baseSpeed;
            SetStartingDirectionAndSpeed();
            targetSpeed = RigidBody.Velocity;

            Reset();

            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }

        private void SetStartingDirectionAndSpeed()
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
            RigidBody.Velocity *= currentSpeed;
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
                if (Vector2.Distance(Position, b.Position) <= CheckRange && b != this)
                {
                    neigbours.Add(b);
                }
            }

            if (neigbours.Count > 0)
            {
                if (((PlayScene)Game.CurrentScene).SpeedMultiplierEnabled && neigbours.Count > 0)
                {
                    speedMultiplier = 1 + SpeedMultiplierIncrement * neigbours.Count * 0.25f;
                    ratioMultiplier = 1 + SpeedMultiplierIncrement * neigbours.Count;

                    alignmentMultiplier = ratioMultiplier * 0.6f;
                    cohesionMultiplier = ratioMultiplier * 0.35f;
                    separationMultiplier = ratioMultiplier * 0.175f;
                    CheckRange = 90;
                }
                else
                {
                    speedMultiplier = 1;
                    ratioMultiplier = 1;

                    alignmentMultiplier = 1;
                    cohesionMultiplier = 1;
                    separationMultiplier = 1;


                    CheckRange = 75;
                }

                AlignmentLogic(ref newDir, neigbours);
                CohesionLogic(ref newDir, neigbours);
                SeparationLogic(ref newDir, neigbours);

                targetSpeed = Vector2.Normalize(Vector2.Lerp(RigidBody.Velocity, newDir, TurnRatio)) * (currentSpeed * (speedMultiplier));
                RigidBody.Velocity = Vector2.Lerp(RigidBody.Velocity, targetSpeed, smoothness);
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

            newDir = Vector2.Lerp(newDir, dir.Normalized() * currentSpeed, AlignmentRatio * alignmentMultiplier);

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
            newDir = Vector2.Lerp(newDir, currentPosToCenter.Normalized() * currentSpeed, CohesionRatio * cohesionMultiplier);
        }

        private void SeparationLogic(ref Vector2 newDir, List<Boid> neigbours)
        {
            bool found = false;
            Vector2 escapeDirection = newDir;

            Boid closestBoid = null;
            float closestDistance = Game.Window.Width + HalfWidth * 2;

            foreach (Boid b in neigbours)
            {
                float dist = Vector2.Distance(b.Position, Position);
                if (dist <= MinDistance && dist < closestDistance)
                {
                    found = true;
                    closestBoid = b;
                    closestDistance = dist;
                }
            }

            if (found)
            {
                float distPercent = 1 - ((MinDistance - Vector2.Distance(closestBoid.Position, Position)) / MinDistance);
                escapeDirection -= (Position - closestBoid.Position) * distPercent * SeparationMultiplier;

                escapeDirection = escapeDirection.Normalized() * currentSpeed;
                newDir = Vector2.Lerp(newDir, newDir - (escapeDirection), SeparationRatio * (separationMultiplier)).Normalized();
            }
        }
    }
}
