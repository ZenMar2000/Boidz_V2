using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Boidz
{
    class KeyboardController : Controller
    {
        public KeyboardController(int ctrlIndex) : base(ctrlIndex)
        {

        }

        public override float GetHorizontal()
        {
            float direction = 0.0f;

            if(Game.Window.GetKey(KeyCode.D))
            {
                direction = 1;
            }
            else if(Game.Window.GetKey(KeyCode.A))
            {
                direction = -1;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction = 0.0f;

            if (Game.Window.GetKey(KeyCode.W))
            {
                direction = -1;
            }
            else if (Game.Window.GetKey(KeyCode.S))
            {
                direction = 1;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Window.GetKey(KeyCode.Space);
        }
    }
}
