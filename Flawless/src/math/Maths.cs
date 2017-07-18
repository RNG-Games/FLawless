using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace _Flawless.math
{
    public struct Angle
    {
        public float Value { get; set; }
        public Angle(float value)
        {
            this.Value = value;
        }
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle((a.Value + b.Value) % 360);
        }
        public static Angle operator -(Angle a)
        {
            return new Angle(-a.Value);
        }
        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(((a + -b).Value%360 + 360)%360);
        }

    }

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

        public static float length (Vector2f vector )
        {
            return (float) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

	    public static List<int> factorials = new List<int>() { 1, 1, 2, 6 };

	    public static int factorial(int number)
	    {
		    if (factorials.Count - 1 >= number)
			    return factorials[number];
		    factorials.Insert(number, number * factorial(number - 1));
		    return factorials[number];
	    }

		public static int binomial (int n, int k)
        {
            return factorial(n) / (factorial(k) * factorial(n-k));
        }

        public static Vector2f bezier(float t, List<Vector2f> points)
        {
            if (t < 0 || t > 1) { return new Vector2f(0, 0); }
            int n = points.Count - 1;
            Vector2f result = new Vector2f(0, 0);
            for (int i = 0; i <= n; i++)
            {
                float factor = binomial(n, i) * (float) Math.Pow(t, i) * (float) Math.Pow(1 - t, n - i);
                result.X += factor * points.ElementAt(i).X;
                result.Y += factor * points.ElementAt(i).Y;
            }
            return result;
        }
    }
}

