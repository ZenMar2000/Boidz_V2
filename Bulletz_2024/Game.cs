using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Boidz
{
    static class Game
    {
        // Variables
        public static Window Window;
        //public static Background Bg;
        //public static Player Player;

        private static KeyboardController keyboardCtrl;
        private static List<Controller> controllers;



        // Properties
        public static Scene CurrentScene { get; private set; }
        public static float DeltaTime { get { return Window.DeltaTime; } }
        public static float ScreenCenterX { get { return Window.Width * 0.5f; } }
        public static float ScreenCenterY { get { return Window.Height * 0.5f; } }

        public static void Init()
        {
            Window = new Window(1920, 1080, "Boids amount: ");

            // SCENES
            //TitleScene titleScene = new TitleScene("titleScreen");
            PlayScene playScene = new PlayScene();
            //GameOverScene gameOverScene = new GameOverScene();

            //titleScene.NextScene = playScene;
            playScene.NextScene = null;
            //gameOverScene.NextScene = titleScene;

            CurrentScene = playScene;

            // CONTROLLERS
            // Always create a keyboard controller (init at 0 cause we only have 1 keyboard)
            keyboardCtrl = new KeyboardController(0);

            // Check how many joypads are currently connected
            string[] joysticks = Window.Joysticks;
            controllers = new List<Controller>();
        }

        public static Controller GetController(int index)
        {
            Controller ctrl = keyboardCtrl;

            if (index < controllers.Count)
            {
                ctrl = controllers[index];
            }

            return ctrl;
        }

        public static void Play()
        {
            CurrentScene.Start();

            while (Window.IsOpened)
            {
                //if (!Player.IsAlive)
                //    return;

                //// INPUT
              
                // Exit when ESC is pressed
                if (Window.GetKey(KeyCode.Esc))
                {
                    break;
                }

                if (!CurrentScene.IsPlaying)
                {
                    Scene nextScene = CurrentScene.OnExit();

                    if (nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                // INPUT
                CurrentScene.Input();

                // UPDATE
                CurrentScene.Update();

                // DRAW
                CurrentScene.Draw();

                Window.Update();
            }

        }
    }
}
