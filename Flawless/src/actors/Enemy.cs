using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.src.ai2;

namespace _Flawless.actors
{
    abstract class Enemy : IActable
    {
        protected float startTime;
        protected Vector2f position;
        protected Movement movement;
        protected Sprite texture;

        protected int frameCounter = 0;
        protected Boolean isExpired = false;
        protected Queue<Pattern> patternQueue = new Queue<Pattern>();
        protected Pattern currentPattern = null;

        public Enemy(float _startTime, Vector2f _position, Sprite _texture)
        {
            startTime = _startTime;
            position = _position;
            texture = _texture;
        }

        public virtual void Draw(RenderWindow _window)
        {
            _window.Draw(texture);
        }

        public virtual void Update(float _deltaTime)
        {
            /* movement */
            position = movement.NewPosition(position, _deltaTime);
            texture.Position = position;
            /* patterns */
            if (currentPattern != null) currentPattern.Update(_deltaTime);
            /* miscellaneous */
            ++frameCounter;
        }

        public float StartTime() { return startTime; }
        public Vector2f Position() { return position; }
        public Sprite Texture() { return texture; }
        public int FrameCounter() { return frameCounter; }
        public Boolean IsExpired() { return isExpired; }
        public Pattern CurrentPattern() { return currentPattern; }
    }
}
