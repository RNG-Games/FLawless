using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace _Flawless.actors
{
    /// <summary>
    /// Benötigt für alle Actors
    /// </summary>
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
        /// Zeit ab der der Actor geupdated/verwaltet wird
        /// </summary>
        /// <returns>Startzeit in Sekunden</returns>
        float StartTime();

        /// <summary>
        /// Damit Actors aus der verwalteten Liste genommen werden können
        /// true - wird nicht mehr genutzt
        /// </summary>
        /// <returns>Wahrheitswert ob noch genutzt</returns>
        bool IsExpired();
    }
}
