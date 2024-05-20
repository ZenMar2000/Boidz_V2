using Aiv.Fast2D;
using OpenTK;
using Boidz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz.Managers
{
    static class CameraMgr
    {
        private static Camera mainCamera;

        public static GameObject Target;
        public static float CameraSpeed = 5;
        public static Vector2 TargetOffset;

        public static void Init(GameObject target)
        {
            mainCamera = new Camera(target.Position.X, target.Position.Y);
            mainCamera.pivot = new Vector2(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f);
            TargetOffset = new Vector2(0, -200f);
            Target = target;
        }

        public static void Update()
        {
            //mainCamera.position = Target.Position; //position locking

            //smooth lerp
            //mainCamera.position = Vector2.Lerp(mainCamera.position, Target.Position + TargetOffset, Game.Window.DeltaTime * CameraSpeed);

        }

    }
}
