using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Flawless.actors.enemies;
using SFML.System;

namespace _Flawless.actors
{
    static class EnemyFactory
    {
        public static Enemy GetEnemy(String enemyType, float x, float y)
        {
            Vector2f position = new Vector2f(x, y);
            if(enemyType == "A")
            {
                return new TestEnemy(0f, position);
            }
            return null;
        }
    }
}
