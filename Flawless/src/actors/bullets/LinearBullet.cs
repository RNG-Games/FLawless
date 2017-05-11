using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.math;

namespace _Flawless.actors.bullets
{
    class LinearBullet : Bullet
    {
        Vector2f movement;

        public LinearBullet(Vector2f _position, Angle _angle) : base(_position, _angle)
        {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            speed = 40f;
        }

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            radius += speed * _deltaTime;
            position = Maths.toCartesian(spawnPosition, angle.Value, radius);
            texture.Position = position;
        }
    }
}
