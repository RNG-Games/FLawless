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
        private Vector2f position;
        private float speed;
        public List<PlayerBullet> bullets = new List<PlayerBullet>();
        private float firecounter = 0.2f;
        private float eRegenCounter = 0.5f;
        private float energy = 1f;
        private Text text = new Text("", Resources.GetFont("trebuc.ttf"));
        private float protection = 0f;
        private bool dying = false;

        public Player()
        {
            position = new Vector2f(Program.window.Size.X / 2.0f, 500);
            texture = new Sprite(Resources.GetTexture("player.png")) {Position = position};
            speed = 300f;
            hitbox = new Circle(position + new Vector2f(18.5f, 22.5f), 8.5f);
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
            text.Color = new Color(0,0,0);
            _window.Draw(text);

            if (dying)
            {
                
            }
        }

        public void Update(float _deltaTime)
        {
            if (dying)
            {
                
                return;
            }
            firecounter += _deltaTime;
            if (firecounter >= float.MaxValue - 15f) firecounter = 2f;
            eRegenCounter += _deltaTime;
            if (eRegenCounter >= float.MaxValue - 15f) eRegenCounter = 2f;

            var move = new Vector2f(0,0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                move.Y -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                move.Y += speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                move.X -= speed * _deltaTime;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                move.X += speed * _deltaTime;

            //TODO: Bewegungseinschränkungen mit Overlay
            if (hitbox.middle.X - hitbox.radius <= 0 && move.X < 0)
                move.X = 0;
            if (hitbox.middle.X + hitbox.radius >= Program.window.Size.X && move.X > 0)
                move.X = 0;
            if (hitbox.middle.Y - hitbox.radius <= 0 && move.Y < 0)
                move.Y = 0;
            if (hitbox.middle.Y + hitbox.radius >= Program.window.Size.Y && move.Y > 0)
                move.Y = 0;

            position += move;

            texture.Position = position;
            hitbox.addToPostion(move);

            bullets = bullets.Where(n => !n.IsExpired()).ToList();
            foreach (var pb in bullets)
            {
                pb.Update(_deltaTime);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && firecounter >= 0.15f && energy >= 0.03f)
            {
                fire();
                firecounter = 0f;
                eRegenCounter = 0f;
                energy -= 0.03f;
            }
            else if(eRegenCounter >= 0.4f)
            {
                energy += _deltaTime/33f;
            }
            if (energy > 1f) energy = 1f;
        }

        private void fire()
        {
            bullets.Add(new PlayerBullet(hitbox.middle));
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

            energy -= damage;
            eRegenCounter = 0f;
            if (energy <= 0)
            {
                energy = 0f;
                dying = true;
            }
        }

    }
}
