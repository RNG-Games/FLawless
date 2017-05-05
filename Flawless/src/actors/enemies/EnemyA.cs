using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace _Flawless.actors.enemies
{
    class EnemyA : Enemy
    {
        public EnemyA(Vector2f position) : base(position) {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
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
        }
    }
}
