using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _Flawless.actors
{
    abstract class Pattern : IActable
    {
        public virtual void Draw(RenderWindow _window) {}

        public bool IsExpired()
        {
            return false;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(float _deltaTime) {}
    }
}
