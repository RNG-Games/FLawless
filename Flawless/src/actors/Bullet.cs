using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.util;

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

        private Sprite texture;
        private Circle hitbox;
        private Vector2f spawnPosition;
        public Vector2f position;
        private float speed;
        private float angle;

        public Bullet(Vector2f _position, float _angle)
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

        public void Update(float _deltaTime)
        {
            throw new NotImplementedException();
        }

    }
}
