using OpenTK;
using System.Collections.Generic;
using System.Linq;

namespace Boidz
{
    class PlayScene : Scene
    {
        public List<Boid> boids;
        public PlayScene() : base()
        {

        }


        public override void Start()
        {
            boids = new List<Boid>();
            LoadAssets();

            for (int i = 0; i < 100; i++)
            {
                boids.Add(new Boid());
                float rX = RandomGenerator.GetRandomFloat(1, Game.Window.Width - 1);
                float rY = RandomGenerator.GetRandomFloat(1, Game.Window.Height - 1);
                boids.Last().Position = new Vector2(rX, rY);
            }

            base.Start();
        }

        protected override void LoadAssets()
        {
            //images
            GfxMngr.AddTexture("boid", "Assets/boid.png");
        }

        public override void Input()
        {
            //Player.Input(); //TODO
        }

        public override void Update()
        {
            //PhysicsMngr.Update();
            UpdateMngr.Update();
        }

        public override Scene OnExit()
        {

            //Bg = null;

            //BulletMngr.ClearAll();
            //SpawnMngr.ClearAll();
            UpdateMngr.ClearAll();
            PhysicsMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            FontMngr.ClearAll();
            //PowerUpsMngr.ClearAll();

            //DebugMngr.ClearAll();

            return base.OnExit();
        }

        public override void Draw()
        {
            //Bg.Draw();
            DrawMngr.Draw();

            //DebugMngr.Draw();
        }
    }
}
