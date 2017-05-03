using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using _Flawless.util;

namespace _Flawless.actors
{
    class Player : IActable
    {
        private Sprite texture;
        private Circle hitbox;
        public Vector2f position;
        private float speed;

        public Player()
        {
            position = new Vector2f(100, 100);
            texture = new Sprite(Resources.GetTexture("player.png")) {Position = position};
            speed = 0.2f;
        }

        public void Draw(RenderWindow _window)
        {
            _window.Clear(new Color(100,100,100));
            _window.Draw(texture);
        }

        public void Update(float _deltaTime)
        {
            var move = new Vector2f(0,0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                move.Y -= speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                move.Y += speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                move.X -= speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                move.X += speed;

            //TODO: Bewegungseinschränkungen
            if (position.X <= 0 && move.X < 0)
                move.X = 0;
            if (position.X + texture.Texture.Size.X >= Program.window.Size.X && move.X > 0)
                move.X = 0;
            if (position.Y <= 0 && move.Y < 0)
                move.Y = 0;
            if (position.Y + texture.Texture.Size.Y >= Program.window.Size.Y && move.Y > 0)
                move.Y = 0;

            position += move;

            texture.Position = position;
            //hitbox.setPosition(position.X,position.Y);
        }

        public float StartTime()
        {
            return 0f;
        }

        public bool IsExpired()
        {
            return false;
        }
    }
}
