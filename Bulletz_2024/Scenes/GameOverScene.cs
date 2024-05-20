using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    class GameOverScene : TitleScene
    {
        public GameOverScene() : base("gameOverScreen", Aiv.Fast2D.KeyCode.Y)
        {

        }

        protected override void LoadAssets()
        {
            GfxMngr.AddTexture("gameOverScreen", "Assets/gameOverBg.png");
        }

        public override void Input()
        {
            base.Input();

            if(IsPlaying && Game.Window.GetKey(Aiv.Fast2D.KeyCode.N))
            {
                IsPlaying = false;
                NextScene = null;
            }
        }
    }
}
