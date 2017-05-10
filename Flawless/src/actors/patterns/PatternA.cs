using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Flawless.actors.patterns
{
    class PolarPatterns : Pattern
    {
        List<Bullet> bulletList;
        BulletFactory factory = new BulletFactory();

        public override void Update(float _deltaTime)
        {
            
        }
    }

    class PPBurst
    {
        int bulletNum_;
        public PPBurst(int bulletNum_) {
            this.bulletNum_ = bulletNum_;

        }

    }
}
