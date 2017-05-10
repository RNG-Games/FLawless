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
        public List<PlayerBullet> bullets = new List<PlayerBullet>();
        private float firecounter = 0.2f;
        private float energy = 1f;
        private Text text = new Text("", Resources.getFont("trebuc.ttf"));

        public Player()
        {
            position = new Vector2f(200, 100);
            texture = new Sprite(Resources.GetTexture("player.png")) {Position = position};
            speed = 300f;
            hitbox = new Circle(position, texture.Texture.Size.X);
        }

        public void Draw(RenderWindow _window)
        {
            foreach (var pb in bullets)
            {
               pb.Draw(_window);
            }
            _window.Draw(texture);
            text.DisplayedString = "Energy: " + (int) (energy * 100);
            text.Position = new Vector2f(10,10);
            _window.Draw(text);
        }

        public void Update(float _deltaTime)
        {
            firecounter = (firecounter + _deltaTime)%float.MaxValue;
            var move = new Vector2f(0,0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                move.Y -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                move.Y += speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                move.X -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                move.X += speed * _deltaTime;

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
            hitbox.setPosition(position.X + texture.Texture.Size.X/2f,position.Y + texture.Texture.Size.Y/2f);

            bullets = bullets.Where(n => !n.IsExpired()).ToList();
            foreach (var pb in bullets)
            {
                pb.Update(_deltaTime);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && firecounter >= 0.15f && energy >= 0.01f)
            {
                fire();
                firecounter = 0f;
                energy -= 0.01f;
            }
            else if(firecounter >= 0.2f)
            {
                energy += _deltaTime/33f;
            }
            if (energy > 1f)
                energy = 1f;
        }

        private void fire()
        {
            bullets.Add(new PlayerBullet(position));
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
