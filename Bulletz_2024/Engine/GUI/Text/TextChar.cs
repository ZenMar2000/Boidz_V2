using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    class TextChar : GameObject
    {

        protected Font font;
        protected char character;

        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font f) : base(f.TextureName, spriteWidth: f.CharacterWidth, spriteHeight: f.CharacterHeight)
        {
            sprite.position = spritePosition;
            sprite.pivot = Vector2.Zero;
            font = f;
            Character = character;
            IsActive = true;

            DrawMngr.AddItem(this);
        }

        protected void ComputeOffset()
        {
            Vector2 textureOffset = font.GetOffset(character);

            textOffsetX = (int)textureOffset.X;
            textOffsetY = (int)textureOffset.Y;
        }
    }
}
