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
        protected int angle;
        protected Vector2f position;

        public PolarPattern(int bulletNum, Bullet.BulletType type, Vector2f position)
        {

        }
    }

    class PPBurst : PolarPattern
    {
        int angleChange;

        public PPBurst(int bulletNum, Bullet.BulletType type, Vector2f position, ) {
            this.bulletNum = bulletNum;
            this.type = type;
        }

        public override void Update(float _deltaTime)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                bulletList.Add(factory.GetBullet(type, position, angle));
                angle += angleChange;
            }

        }


    }
}
