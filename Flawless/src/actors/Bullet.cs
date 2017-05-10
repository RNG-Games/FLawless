using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.util;
using _Flawless.math;

namespace _Flawless.actors
{
    abstract class Bullet : IActable
    {

        public enum BulletType
        {
            A,
            B,
            count
        }

        protected Sprite texture;
        protected Circle hitbox;
        protected Vector2f spawnPosition;
        public Vector2f position;
        protected float speed;
        protected Angle angle;

        public Bullet(Vector2f _position, Angle _angle)
        {
            position = _position;
            spawnPosition = _position;
            angle = _angle;
        }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public bool IsExpired()
        {
            throw new NotImplementedException();
        }

        public float StartTime()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(float _deltaTime)
        {
            texture.Position = position;
        }

    }
}
