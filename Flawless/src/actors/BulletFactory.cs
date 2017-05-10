using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using _Flawless.actors.bullets;

namespace _Flawless.actors
{
    class BulletFactory
    {
        public Bullet GetBullet(Bullet.BulletType type, Vector2f position, float angle)
        {
            if(type == Bullet.BulletType.A)
            {
                return new BulletA(position, angle);
            }
            return null;
        }
    }
}
