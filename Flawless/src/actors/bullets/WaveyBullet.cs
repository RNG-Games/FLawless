using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using _Flawless.math;
using _Flawless.util;

namespace _Flawless.actors.bullets
{
    class WaveyBullet : Bullet
    {
        Vector2f offsetDirection;
        double timeCounter;
        public WaveyBullet(Vector2f _position, Angle _angle) : base(_position, _angle)
        {
            offsetDirection = Maths.toCartesian(spawnPosition, angle.Value, 1f);
            offsetDirection = new Vector2f(offsetDirection.Y, -offsetDirection.X) * 0.25f;
            texture = new Sprite(Resources.GetTexture("pbullet.png")) { Position = position };
            hitbox = new Circle(_position.X + texture.Texture.Size.X / 2f, _position.Y + texture.Texture.Size.Y / 2f, texture.Texture.Size.X / 2f);
            speed = 300f;
            damage = 0.1f;
        }

        public override void Update(float _deltaTime)
        {
            base.Update(_deltaTime);
            timeCounter += 5f * _deltaTime;
            if (timeCounter > 360) timeCounter = 0;
            radius += speed * _deltaTime;
            position = Maths.toCartesian(spawnPosition, angle.Value, radius) + offsetDirection * (float) Math.Sin(timeCounter);
            texture.Position = position;
            hitbox.setPosition(position.X + texture.Texture.Size.X / 2f, position.Y + texture.Texture.Size.Y / 2f);
            CheckCollision();
            CheckLocation();
        }
    }
}
