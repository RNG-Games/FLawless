using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _Flawless.actors
{
    interface IActable
    {
        void Draw(RenderWindow _window);
        void Update(float _deltaTime);
        float StartTime();
        bool IsExpired();
    }
}
