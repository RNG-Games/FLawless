﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Flawless.actors.enemies;

namespace _Flawless.actors
{
    static class EnemyFactory
    {
        public static IEnemy GetEnemy(String enemyType)
        {
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
            return null;
        }
    }
}
