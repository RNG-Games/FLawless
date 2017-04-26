using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Flawless.util
{
    class Circle
    {
        public float x { get; protected set; }
        public float y { get; protected set; }
        public float radius { get; protected set; }

        public Circle() { }

        public Circle(float x, float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        public void setPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public bool intersectsWith(Circle c)
        {
            float distancePow2 = (c.x - this.x) * (c.x - this.x) + (c.y - this.y) * (c.y - this.y);
            return distancePow2 < (c.radius + radius) * (c.radius + radius);                        /* since distance ^2 is used, the 
                                                                                                       (radius + radius) is squared too*/
        }
    }



}
