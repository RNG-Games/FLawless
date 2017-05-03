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
        /// <summary>
        /// Alles zeichnen, was mit dem Ding zu tun hat
        /// </summary>
        /// <param name="_window">RenderWindow in das gezeichnet wird</param>
        void Draw(RenderWindow _window);

        /// <summary>
        /// Update die Daten
        /// </summary>
        /// <param name="_deltaTime"></param>
        void Update(float _deltaTime);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        float StartTime();
        bool IsExpired();
    }
}
