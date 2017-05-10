using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace _Flawless.math
{
    class Maths
    {
        /// <summary>
        /// Erzeugt verwendbare Koordinaten für den Positionsvektor
        /// </summary>
        /// <param name="spawnPosition">Rotationszentrum</param>
        /// <param name="angle">Winkel</param>
        /// <param name="radius">Radius</param>
        public static Vector2f toCartesian( Vector2f spawnPosition, double angle, double radius)
        {
            angle = ((angle * Math.PI) / 180);
            var x = Convert.ToSingle(radius * Math.Cos(angle)) + spawnPosition.X;
            var y = Convert.ToSingle(radius * Math.Sin(angle)) + spawnPosition.Y;
            return new Vector2f(x, y);
        }

        public static Vector2f toPolar ( Vector2f spawnPosition, Vector2f position)
        {
            var radius = (float) Math.Sqrt(Math.Pow(position.X - spawnPosition.X, 2) + Math.Pow(position.Y - spawnPosition.Y, 2));
            var angle = (float) Math.Acos(position.X - spawnPosition.X / radius);
            if ( position.Y < spawnPosition.Y) { angle = 360 - angle; }    //if the positon is below the spwan, the angle is "spun" around the x axis
            return new Vector2f(radius, angle);
        }

        public struct Angle
        {
            public float value { get; set; }
            public Angle(float value)
            {
                this.value = value;
            }
            public static Angle operator +(Angle a, Angle b)
            {
                return new Angle((a.value + b.value) % 360);
            }
            public static Angle operator -(Angle a)
            {
                return new Angle(-a.value);
            }
            public static Angle operator -(Angle a, Angle b)
            {
                Angle result = a + (-b);
                result.value += 360;  //erhöhe winkel um 360 , um ggf negativität zu verhindern
                result.value %= 360;
                return result;
            }

        }
        
    }
}

