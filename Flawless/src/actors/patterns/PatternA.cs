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
        protected int bulletNum;
        protected Bullet.BulletType type;
        protected List<Bullet> bulletList;
        protected BulletFactory factory;
        protected Angle angle;
        protected Vector2f position;
        protected Angle angleChange;

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

        protected virtual void updateBullets(float _deltaTime)
        {
            bulletList = bulletList.Where(b => !b.IsExpired()).ToList();
            foreach (var current in bulletList)
            {
                current.Update(_deltaTime);
            }
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

        public override void Draw(RenderWindow _window)
        {
            base.Draw(_window);
            foreach (var bullet in bulletList)
            {
                bullet.Draw(_window);
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

            updateBullets(_deltaTime);
            timeCounter += _deltaTime;
        }

    }
}
