using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace _Flawless.actors.patterns
{
    abstract class PolarPattern : Pattern
    {
        protected int bulletNum;
        protected Bullet.BulletType type;
        protected List<Bullet> bulletList;
        protected BulletFactory factory = new BulletFactory();
        protected float angle;
        protected Vector2f position;
        protected float angleChange;

        public PolarPattern(int bulletNum, Bullet.BulletType type, Vector2f position, float angle, float angleChange)
        {
            this.bulletNum = bulletNum;
            this.type = type;
            this.position = position;
            this.angle = angle;
            this.position = position;
            this.angleChange = angleChange;
        }

        protected virtual void updateBullets(float _deltaTime)
        {
            foreach (var current in bulletList)
            {
                current.Update(_deltaTime);
            }
        }
    }

    /* Constant Polar Pattern */

    class PPBurst : PolarPattern
    {
        public PPBurst(int bulletNum, Bullet.BulletType type, Vector2f position, float angle, float angleChange) 
            : base(bulletNum, type, position, angle, angleChange)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                bulletList.Add(factory.GetBullet(type, position, angle));
            }
        }

        public override void Update(float _deltaTime)
        {
            updateBullets(_deltaTime);
        }
    }

    /* Interval Polar Pattern */
    class PPInterval : PolarPattern
    {
        private bool isSpawned = false;
        private float timeCounter = 0;
        private float interval;

        public PPInterval(int bulletNum, Bullet.BulletType type, Vector2f position, float angle, float angleChange, float interval)
            : base(bulletNum, type, position, angle, angleChange)
        {
            this.interval = interval;
        }

        public override void Update(float _deltaTime)
        {
            if (!isSpawned && (timeCounter >= interval))
            {
                for (int i = 0; i < bulletNum; i++)
                {
                    bulletList.Add(factory.GetBullet(type, position, angle));
                }
            }

            updateBullets(_deltaTime);
            timeCounter += _deltaTime;
        }

    }
}
