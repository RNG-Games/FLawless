using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace _Flawless.actors
{
    abstract class Pattern : IActable
    {
        protected int bulletNum;
        protected Bullet.BulletType type;
        protected List<Bullet> bulletList;
        protected BulletFactory factory;
        protected Vector2f position;

        public int GetBulletNum() { return bulletNum; }
        public Bullet.BulletType GetBulletType() { return type; }
        public List<Bullet> GetBulletList() { return bulletList; }
        public Vector2f GetPosition() { return position; }
        public virtual Pattern GetCopy(Vector2f position) { return null; }

        public bool IsExpired()
        {
            if (bulletList.Any<Bullet>()) return false;
            return true;
        }

        public float StartTime()
        {
            return 0f;
        }

        public virtual void Update(float _deltaTime) {
            bulletList = bulletList.Where(b => !b.IsExpired()).ToList();
            foreach (var current in bulletList)
            {
                current.Update(_deltaTime);
            }
        }

        public virtual void Draw(RenderWindow _window)
        {
            foreach (var bullet in bulletList)
            {
                bullet.Draw(_window);
            }
        }
    }
}
