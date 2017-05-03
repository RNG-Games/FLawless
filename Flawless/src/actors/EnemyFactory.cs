using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Flawless.src.actors
{
    class EnemyFactory
    {
        public Enemy GetEnemy(String enemyType)
        {
            if(enemyType == null)
            {
                return null;
            }
            if(enemyType == "A")
            {
                return new EnemyA();
            }
            if(enemyType == "B")
            {
                return new EnemyB();
            }
            if(enemyType == "C")
            {
                return new EnemyC();
            }
        }
    }
}
