using System.Collections.Generic;
using System.Linq;
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
        private Vector2f _position;
        private float speed;
        public List<PlayerBullet> Bullets = new List<PlayerBullet>();
        private float _firecounter = 0.2f;
        private float _eRegenCounter = 0.5f;
        private float _energy = 1f;
        private int _life = 2;
        private Text text = new Text("", Resources.GetFont("rabelo.ttf"));
        private float protection = 0f;
        private bool _dying = false;

        public Player()
        {
            _position = new Vector2f(Program.window.Size.X / 2.0f, 500);
            texture = new Sprite(Resources.GetTexture("player.png")) {Position = _position};
            speed = 300f;
            hitbox = new Circle(_position + new Vector2f(18.5f, 22.5f), 8.5f);
        }

        public void Draw(RenderWindow window)
        {
            foreach (var pb in Bullets)
            {
               pb.Draw(window);
            }
            window.Draw(texture);
            text.DisplayedString = "Energy: " + (int) (_energy * 100);
            text.Position = new Vector2f(10,10);
            text.FillColor = new Color(0,0,0);
            window.Draw(text);

            if (_dying)
            {
                
            }
        }

        public void Update(float deltaTime)
        {
            if (_dying)
            {
                
                return;
            }
            _firecounter += deltaTime;
            if (_firecounter >= float.MaxValue - 15f) _firecounter = 2f;
            _eRegenCounter += deltaTime;
            if (_eRegenCounter >= float.MaxValue - 15f) _eRegenCounter = 2f;

            var move = new Vector2f(0,0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                move.Y -= speed * deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                move.Y += speed * deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                move.X -= speed * deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                move.X += speed * deltaTime;

            move = CheckMove(move);

            _position += move;

            texture.Position = _position;
            hitbox.addToPostion(move);

            Bullets = Bullets.Where(n => !n.IsExpired()).ToList();
            foreach (var pb in Bullets)
            {
                pb.Update(deltaTime);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _firecounter >= 0.15f && _energy >= 0.03f)
            {
                Fire();
                _firecounter = 0f;
                _eRegenCounter = 0f;
                _energy -= 0.03f;
            }
            else if(_eRegenCounter >= 0.4f)
            {
                _energy += deltaTime/33f;
            }
            if (_energy > 1f) _energy = 1f;
        }

        private Vector2f CheckMove(Vector2f move)
        {
            if (Program.debug)
            {
                return move;
            }

            //TODO: Test
            if (hitbox.middle.X - hitbox.radius <= Program.window.GetView().Size.X * (55f / 384) && move.X < 0)
                move.X = 0;
            if (hitbox.middle.X + hitbox.radius >= Program.window.GetView().Size.X * (3f / 5) && move.X > 0)
                move.X = 0;
            if (hitbox.middle.Y - hitbox.radius <= Program.window.GetView().Size.Y * (7f / 216) && move.Y < 0)
                move.Y = 0;
            if (hitbox.middle.Y + hitbox.radius >= Program.window.GetView().Size.Y * (209f / 216) && move.Y > 0)
                move.Y = 0;

            return move;
        }

        private void Fire()
        {
            Bullets.Add(new PlayerBullet(hitbox.middle));
        }
        public float StartTime()
        {
            return 0f;
        }

        public bool IsExpired()
        {
            return false;
        }

        public bool IntersectsWith(Circle eHitbox)
        {
            return eHitbox.intersectsWith(this.hitbox);
        }

        public void DoDamage(float damage)
        {
            damage -= protection;
            if (damage <= 0.01f) damage = 0.01f;

            _energy -= damage;
            _eRegenCounter = 0f;
            if (_energy <= 0)
            {
                _life--;
                if (_life <= 0)
                {
                    _dying = true;
                }
                _energy = 0f;
            }
        }

    }
}
