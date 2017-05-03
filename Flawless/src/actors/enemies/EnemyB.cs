using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace _Flawless.actors.enemies
{
    class EnemyB : IEnemy
    {
        private Vector2f position;
        private Sprite texture;
        public EnemyB() { }

        public void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public Vector2f GetPosition() { return position; }

        public bool IsExpired()
        {
            return false;
        }

        public void KillEnemy()
        {
            throw new NotImplementedException();
        }

        public void ResetEnemy()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(float x, float y)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(int x, int y) { position = new Vector2f(x, y); }

        public void SetStartTime(float time)
        {
            throw new NotImplementedException();
        }

        public float StartTime()
        {
            return 0f;
        }

        public void TakeControl()
        {
            throw new NotImplementedException();
        }

        public void Update(float _deltaTime)
        {

        }
    }
}
