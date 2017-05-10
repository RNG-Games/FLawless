using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using _Flawless.actors.bullets;
using static _Flawless.math.Maths;

namespace _Flawless.actors
{
    class BulletFactory
    {
        public Bullet GetBullet(Bullet.BulletType type, Vector2f position, Angle angle)
        {
            if(type == Bullet.BulletType.A)
            {
                return new LinearBullet(position, angle);
            }
            return null;
        }
    }
}
