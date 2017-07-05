using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.util;
using _Flawless.math;
using _Flawless;

namespace _Flawless.actors
{
    abstract class Bullet : IActable
    {

        public enum BulletType
        {
            A,
            B,
            C,
            count
        }

        protected Sprite texture;
        protected Circle hitbox;
        protected Vector2f spawnPosition;
        public Vector2f position;
        protected float speed;
        protected Angle angle;
        protected float radius;
        protected float damage;
        protected bool expiration;

        public Bullet(Vector2f _position, Angle _angle)
        {
            position = _position;
            spawnPosition = _position;
            angle = _angle;
            radius = 0;
            expiration = false;
        }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public bool IsExpired()
        {
            return expiration;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(float _deltaTime)
        {
            texture.Position = position;
        }

        protected void CheckCollision()
        {
            if (!Resources.GetPlayer().IntersectsWith(this.hitbox) || expiration) return;
            expiration = true;
            Resources.GetPlayer().DoDamage(damage);
        }

        protected virtual void CheckLocation()
        {
            if (hitbox.middle.X + hitbox.radius < -(hitbox.radius * 3) || hitbox.middle.X - hitbox.radius > 
                Program.Window.GetView().Size.X + (hitbox.radius * 3))
                expiration = true;
            if (hitbox.middle.Y + hitbox.radius < -(hitbox.radius * 3) || hitbox.middle.Y - hitbox.radius >
                Program.Window.GetView().Size.X + (hitbox.radius * 3))
                expiration = true;
        }
    }
}
