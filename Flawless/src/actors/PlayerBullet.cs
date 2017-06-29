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
            this.speed = new Vector2f(0,-600f);
            texture = new Sprite(Resources.GetTexture("pbullet.png"));
            this.position = position - new Vector2f(texture.Texture.Size.X/2f, 0);
            hitbox = new Circle(position, texture.Texture.Size.X/2f);
            texture.Position = position;
        }
        private bool expired = false;

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public void Update(float _deltaTime)
        {
            position += speed * _deltaTime;
            texture.Position = position;
            hitbox.setPosition(hitbox.middle + speed);
            if (position.X < -10f || position.Y < -10)
                expired = true;
            if (position.X > Program.window.GetView().Size.X + 10f || position.Y > Program.window.GetView().Size.Y + 10f)
                expired = true;
        }

        public float StartTime()
        {
            return 0f;
        }

        public bool IsExpired()
        {
            return expired;
        }
    }
}
