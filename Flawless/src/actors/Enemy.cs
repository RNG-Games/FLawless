using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace _Flawless.actors
{
    abstract class Enemy : IActable
    {
        protected Vector2f position;
        protected Sprite texture;
        protected Queue<Pattern> patternQueue = new Queue<Pattern>();
        protected int frameCounter;

        public Enemy(Vector2f position)
        {
            this.position = position;
        }

        public virtual void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public Vector2f GetPosition()
        {
            return position;
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2f(x, y);
        }

        public Boolean IsExpired()
        { return false; }

        public virtual void Update(float _deltaTime)
        {
            frameCounter++;
        }

        public float StartTime()
        {
            return 0f;
        }

        //void KillEnemy
        //void SetStartTime(float time)
        //void ResetEnemy()
        //void TakeControl()
    }
}
