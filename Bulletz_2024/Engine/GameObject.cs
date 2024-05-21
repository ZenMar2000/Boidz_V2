using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Boidz
{
    class GameObject : I_Updatable, I_Drawable
    {
        protected Sprite sprite;
        protected Texture texture;

        public RigidBody RigidBody;
        public bool IsActive;

        protected float currentSpeed;
        //protected Vector2 velocity;

        public virtual Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }

        public int HalfWidth { get; protected set; }
        public int HalfHeight { get; protected set; }

        protected int textOffsetX, textOffsetY;

        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            }
            set
            {
                sprite.Rotation = (float)Math.Atan2(value.Y, value.X);
            }
        }

        public GameObject(string texturePath, int textOffsetX = 0, int textOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0)
        {
            texture = GfxMngr.GetTexture(texturePath);
            int spriteW = spriteWidth > 0 ? spriteWidth : texture.Width;
            int spriteH = spriteHeight > 0 ? spriteHeight : texture.Height;
            sprite = new Sprite(spriteW, spriteH);
            sprite.scale = new Vector2(0.325f,0.325f);

            this.textOffsetX = textOffsetX;
            this.textOffsetY = textOffsetY;

            HalfWidth = (int)(sprite.Width * 0.5f);
            HalfHeight = (int)(sprite.Height * 0.5f);

            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
        }

        public virtual void Update()
        {
            //sprite.position += velocity * Game.DeltaTime;
        }

        public virtual void OnCollide(GameObject other)
        {

        }

        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, textOffsetX, textOffsetY, (int)sprite.Width, (int)sprite.Height);
            }
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateMngr.RemoveItem(this);
            DrawMngr.RemoveItem(this);

            if (RigidBody != null)
            {
                RigidBody.Destroy();
                RigidBody = null;
            }
        }
    }
}
