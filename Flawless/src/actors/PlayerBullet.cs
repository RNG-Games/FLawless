using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _Flawless.util;

namespace _Flawless.actors
{
    class PlayerBullet : IActable
    {
        private Vector2f speed;
        private Vector2f position;
        private Sprite texture;
        public Circle hitbox { get; protected set; }

        public PlayerBullet(Vector2f position)
        {
            this.position = position;
            this.speed = new Vector2f(0,-0.3f);
            texture = new Sprite(Resources.GetTexture("pbullet.png")) {Position = position};
            hitbox = new Circle(new Vector2f(position.X + texture.Texture.Size.X/2f, position.Y + texture.Texture.Size.Y/2f), texture.Texture.Size.X/2f);
        }
        private bool expired = false;
        /// <summary>
        /// Alles zeichnen, was mit dem Ding zu tun hat
        /// </summary>
        /// <param name="_window">RenderWindow in das gezeichnet wird</param>
        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);


        }

        /// <summary>
        /// Update die Daten
        /// </summary>
        /// <param name="_deltaTime"></param>
        public void Update(float _deltaTime)
        {
            position += speed;
            texture.Position = position;
            hitbox.setPosition(hitbox.middle + speed);
            if (position.X < -5f || position.Y < -5)
                expired = true;
            if (position.X > Program.window.Size.X + 5 || position.Y > Program.window.Size.Y + 5)
                expired = true;
        }

        /// <summary>
        /// Zeit ab der der Actor geupdated/verwaltet wird
        /// </summary>
        /// <returns>Startzeit in Sekunden</returns>
        public float StartTime()
        {
            return 0f;
        }

        /// <summary>
        /// Damit Actors aus der verwalteten Liste genommen werden können
        /// true - wird nicht mehr genutzt
        /// </summary>
        /// <returns>Wahrheitswert ob noch genutzt</returns>
        public bool IsExpired()
        {
            return expired;
        }
    }
}
