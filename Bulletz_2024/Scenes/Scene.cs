using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }

        public Scene NextScene;

        public Scene()
        {

        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual Scene OnExit()
        {
            IsPlaying = false;
            return NextScene;
        }

        protected abstract void LoadAssets();

        public abstract void Input();
        public virtual void Update()
        {

        }
        public abstract void Draw();
    }
}
