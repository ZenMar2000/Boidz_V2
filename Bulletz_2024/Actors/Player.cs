using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Boidz
{
    class Player : Actor
    {
        private bool isFirePressed;
        protected ProgressBar nrgBar;
        protected TextObject playerName;
        protected int playerId;

        protected TextObject scoreText;
        protected int score;

        protected Controller controller;

        public override int Energy { get => base.Energy; set { base.Energy = value; nrgBar.Scale((float)value / (float)maxEnergy); } }

        public Player(Controller ctrl, int id=0) : base("boid")
        {
            IsActive = true;
            currentSpeed = 350.0f;
            //bulletType = BulletType.PlayerBullet;

            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Enemy);

            shootOffset = new Vector2(sprite.pivot.X + 10.0f, sprite.pivot.Y - 10.0f);

            nrgBar = new ProgressBar("barFrame", "blueBar", new Vector2(4.0f, 4.0f));
            nrgBar.Position = new Vector2(60.0f, 50.0f);

            Vector2 playerNamePos = nrgBar.Position;
            playerNamePos.Y -= 20;
            playerName = new TextObject(playerNamePos, $"Player {playerId + 1}", FontMngr.GetFont(), 5);
            playerName.IsActive = true;

            Vector2 scorePos = nrgBar.Position + new Vector2(0, 24);
            scoreText = new TextObject(scorePos, "", FontMngr.GetFont(), 5);
            scoreText.IsActive = true;
            UpdateScore();

            Reset();


            shootVel = new Vector2(1000.0f, 0.0f);

            controller = ctrl;

            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);

            //fast 2D use radiants and the positive rotation is clockwise
            //sprite.Rotation = -MathHelper.PiOver2; 
        }

        protected void UpdateScore()
        {
            scoreText.Text = score.ToString("00000000");
        }

        public void AddScore(int points)
        {
            score += points;
            UpdateScore();
        }

        public void Input()
        {
            //Vector2 direction = new Vector2(controller.GetHorizontal(), controller.GetVertical());

            //if (direction.Length > 1)
            //{
            //    direction.Normalize();
            //}

            //RigidBody.Velocity = direction * currentSpeed;

            //if (controller.IsFirePressed())
            //{
            //    if (!isFirePressed)
            //    {
            //        isFirePressed = true;
            //        Shoot();
            //    }
            //}
            //else if (isFirePressed)
            //{
            //    isFirePressed = false;
            //}
        }

        public override void OnCollide(GameObject other)
        {
            //if(other is Enemy)
            //{
                //((Enemy)other).OnDie();
                //AddDamage(30);
            //}
        }

        public override void OnDie()
        {
            Console.WriteLine("Player is dead");
        }
    }
}
