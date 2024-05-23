using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boidz
{
    class PlayScene : Scene
    {
        public PlayScene() : base()
        {

        }

        #region private vars
        private bool clickedL = false;
        private bool clickedSpace = false;
        private bool clickedShiftL = false;
        #endregion

        #region vars
        public List<Boid> boids;

        public bool SpeedMultiplierEnabled = false;
        #endregion

        public override void Start()
        {
            boids = new List<Boid>();
            LoadAssets();

            //for (int i = 0; i < 150; i++)
            //{
            //    boids.Add(new Boid());
            //    float rX = RandomGenerator.GetRandomFloat(1, Game.Window.Width - 1);
            //    float rY = RandomGenerator.GetRandomFloat(1, Game.Window.Height - 1);
            //    boids.Last().Position = new Vector2(rX, rY);
            //}

            base.Start();
        }

        protected override void LoadAssets()
        {
            GfxMngr.AddTexture("boid", "Assets/boid.png");
        }

        public override void Input()
        {
            if (Game.Window.MouseRight)
            {
                boids.Add(new Boid());
                float rX = Game.Window.MouseX;
                float rY = Game.Window.MouseY;
                boids.Last().Position = new Vector2(rX, rY);
            }

            if (Game.Window.GetKey(Aiv.Fast2D.KeyCode.R))
            {
                UpdateMngr.ClearAll();
                DrawMngr.ClearAll();
                boids = new List<Boid>();

                Console.Clear();
                Console.WriteLine("Window cleared");
            }

            if (Game.Window.MouseLeft)
            {
                if (!clickedL)
                {
                    clickedL = true;
                    if (!clickedShiftL)
                    {

                        boids.Add(new Boid());
                        float rX = Game.Window.MouseX;
                        float rY = Game.Window.MouseY;
                        boids.Last().Position = new Vector2(rX, rY);
                    }
                }

            }
            else if (clickedL)
            {
                clickedL = false;
            }

            if (Game.Window.GetKey(Aiv.Fast2D.KeyCode.Space))
            {
                if (!clickedSpace)
                {
                    clickedSpace = true;
                    SpeedMultiplierEnabled = !SpeedMultiplierEnabled;
                    Console.Clear();
                    Console.WriteLine("Flocking currentSpeed multiplier: " + SpeedMultiplierEnabled);
                }
            }
            else if (clickedSpace)
            {
                clickedSpace = false;
            }

            if (Game.Window.GetKey(Aiv.Fast2D.KeyCode.ShiftLeft))
            {
                if (!clickedShiftL)
                {
                    clickedShiftL = true;
                }
            }
            else if (clickedShiftL)
            {
                clickedShiftL = false;
            }
        }

        public override void Update()
        {
            UpdateMngr.Update();
            Game.Window.SetTitle("Boids amount: " + boids.Count());
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            PhysicsMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            FontMngr.ClearAll();
            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
        }
    }
}
