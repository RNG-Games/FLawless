using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _Flawless.actors.patterns;

namespace _Flawless.actors.enemies
{
    class EnemyA : Enemy
    {
        bool isSpawned = false;
        public EnemyA(Vector2f position) : base(position) {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
        }

        public override void Update(float _deltaTime)
        {
            if (isSpawned == false) {
                PolarPattern test = new PPBurst(10, Bullet.BulletType.A, position, new math.Angle(0f), new math.Angle(1f));
                isSpawned = true;
            }

            base.Update(_deltaTime);
            if (frameCounter % 10000 < 5000)
            {
                position.X += 0.1f;
            }
            else
            {
                position.X -= 0.1f;
            }
            texture.Position = position;
        }
    }
}
