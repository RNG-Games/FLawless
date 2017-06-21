using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using _Flawless.actors.patterns;
using _Flawless.src.ai2;

namespace _Flawless.actors.enemies
{
    //ID Bytes: 1010 1010 = AA = 170

    class TestEnemy : Enemy
    {
        public TestEnemy(float _startTime, Vector2f _position) : base(_startTime, _position, new Sprite(Resources.GetTexture("player.png")))
        {
            var stateList = new Queue<Movement.State>();
            stateList.Enqueue(Movement.State.StraightIn);
            stateList.Enqueue(Movement.State.StandStill5s);
            stateList.Enqueue(Movement.State.StraightOut);
            movement = new Movement(stateList, 300f, _position);
            position = movement.SpawnPosition(_position);
        }
    }

    /*
    class EnemyA : Enemy
    {
        PolarPattern test;
        StreamPattern test2;
        public EnemyA(Vector2f position) : base(position) {
            texture = new Sprite(Resources.GetTexture("player.png")) { Position = position };
            //test = new PPBurst(45, Bullet.BulletType.C, position, new math.Angle(0f), new math.Angle(8f));
            test2 = new SingleSP(45, Bullet.BulletType.C, position, new math.Angle(0f), 3);
        }

        public override void Draw(RenderWindow _window)
        {
            base.Draw(_window);
            test2.Draw(_window);
        }

        public override void Update(float _deltaTime)
        {

            base.Update(_deltaTime);
            texture.Position = position;
            test2.Update(_deltaTime);
        }
    }
    */
}
