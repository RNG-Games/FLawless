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
        PolarPattern test;
        public EnemyA(Vector2f position) : base(position) {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            test = new PPBurst(45, Bullet.BulletType.C, position, new math.Angle(0f), new math.Angle(8f));
        }

        public override void Draw(RenderWindow _window)
        {
            base.Draw(_window);
            test.Draw(_window);
        }

        public override void Update(float _deltaTime)
        {

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
            test.Update(_deltaTime);
        }
    }
}
