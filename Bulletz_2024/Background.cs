using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Boidz
{
    class Background : I_Updatable, I_Drawable
    {
        private Sprite head;
        private Texture texture;

        private Vector2 velocity;

        public Background()
        {
            texture = GfxMngr.GetTexture("BG");
            head = new Sprite(texture.Width, texture.Height);

            velocity.X = -800.0f;

            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }

        public void Draw()
        {
            head.DrawTexture(texture);
        }

        public void Update()
        {
            
        }
    }
}
