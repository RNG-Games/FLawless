using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace _Flawless.src.actors
{
    interface Enemy
    {
        Vector2f GetPosition();
        void SetPosition(float x, float y);
        void SetStartTime(float time);
        void KillEnemy();
        void ResetEnemy();
        void TakeControl();        
    }
}
