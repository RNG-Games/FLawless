using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using _Flawless.math;
using SFML.Graphics;

namespace _Flawless.actors.patterns
{
    abstract class StreamPattern : Pattern
    {
        protected float interval;
        protected Angle angle;
        protected float time = 5;

        public StreamPattern(int bulletNum, Bullet.BulletType type, Vector2f position, Angle angle, float interval)
        {
            this.bulletNum = bulletNum;
            this.type = type;
            this.position = position;
            this.interval = interval;
            this.angle = angle + new math.Angle(90);
            this.position = position;
            factory = new BulletFactory();
            bulletList = new List<Bullet>();
        }
    }

    class SingleSP : StreamPattern
    {
        public SingleSP(int bulletNum, Bullet.BulletType type, Vector2f position, Angle angle, float interval) 
            : base (bulletNum, type, position, angle, interval) { }

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            if (bulletNum > 0 && time > 0.1)
            {
                bulletList.Add(factory.GetBullet(type, position, angle));
                time = 0;
                bulletNum--;
            }
            time += 0.5f * _deltaTime;
        }
    }

    /*class MultipleSP : StreamPattern
    {
        protected int waveNum;
        protected Angle angleInterval;
    }

    class RotationalSP : StreamPattern
    {
        
    }*/
}
