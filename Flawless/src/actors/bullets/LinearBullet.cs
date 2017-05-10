using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace _Flawless.actors.bullets
{
    class LinearBullet : Bullet
    {
        Vector2f direction;

        public LinearBullet(Vector2f _position, float _angle) : base(_position, _angle)
        {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            speed = 0.1f;
            direction = 
        }

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
        }
    }
}
