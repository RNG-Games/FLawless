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
    abstract class PolarPattern : Pattern
    {
        protected Angle angle;
        protected Angle angleChange;

        public Angle GetAngle() { return angle; }
        public Angle GetAngleChange() { return angleChange; }

        public PolarPattern(int bulletNum, Bullet.BulletType type, Vector2f position, Angle angle, Angle angleChange)
        {
            this.bulletNum = bulletNum;
            this.type = type;
            this.position = position;
            this.angle = angle;
            this.position = position;
            this.angleChange = angleChange;
            factory = new BulletFactory();
            bulletList = new List<Bullet>();
        }
    }

    /* Constant Polar Pattern */
    class PPBurst : PolarPattern
    {
        public PPBurst(int bulletNum, Bullet.BulletType type, Vector2f position, Angle angle, Angle angleChange) 
            : base(bulletNum, type, position, angle, angleChange)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                bulletList.Add(factory.GetBullet(type, position, angle));
                angle += angleChange;
            }
        }

        public override Pattern GetCopy(Vector2f position)
        {
            return new PPBurst(this.GetBulletNum(), this.GetBulletType(), position, this.GetAngle(), this.GetAngleChange());
        }
    }

    /* Interval Polar Pattern */
    class PPInterval : PolarPattern
    {
        private bool isSpawned = false;
        private float timeCounter = 0;
        private float interval;

        public PPInterval(int bulletNum, Bullet.BulletType type, Vector2f position, Angle angle, Angle angleChange, float interval)
            : base(bulletNum, type, position, angle, angleChange)
        {
            this.interval = interval;
        }

        public override void Update(float _deltaTime)
        {
            if (!isSpawned && (timeCounter >= interval))
            {
                for (var i = 0; i < bulletNum; i++)
                {
                    bulletList.Add(factory.GetBullet(type, position, angle));
                    angle += angleChange;
                }
            }

            base.Update(_deltaTime);
            timeCounter += _deltaTime;
        }

    }
}
