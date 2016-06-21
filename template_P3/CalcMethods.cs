using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace template_P3
{
    public class CalcMethods
    {

        /// <summary>
        /// Method for normalizing a vector
        /// </summary>
        /// <param name="v">The vector to be normalized</param>
        /// <returns>Returns a normalized vector</returns>
        public static Vector3 Normalize(Vector3 v)
        {
            float length = VectorLength(v);
            return new Vector3(v.X / length, v.Y / length, v.Z / length);
        }

        /// <summary>
        /// Method for calculating a vector's length
        /// </summary>
        /// <param name="v">The vector</param>
        /// <returns>Returns the length of a vector</returns>
        public static float VectorLength(Vector3 v)
        {
            return (float)Math.Sqrt(DotProduct(v, v));
        }

        /// <summary>
        /// The projection of one vector onto another
        /// </summary>
        /// <param name="a">The first vector</param>
        /// <param name="b">The second vector</param>
        /// <returns>Returna a scalar value</returns>
        public static float DotProduct(Vector3 a, Vector3 b)
        {
            return (a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }

        /// <summary>
        /// Calcutate the normal of two vectors
        /// </summary>
        /// <param name="a">The first vector</param>
        /// <param name="b">The second vector</param>
        /// <returns>Returns a normal of two vectors</returns>
        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Y * b.Z - a.Z * b.Y,
                                a.Z * b.X - a.X * b.Z,
                                a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// The infamous ABC Formula
        /// </summary>
        /// <param name="positiveSqrtD">Bool whether you want sqrt(D) to be positive</param>
        /// <returns>Returns a float value for either sqrt(D) or -sqrt(D)</returns>
        public static float ABCFormula(float a, float b, float c, bool positiveSqrtD)
        {
            float D = b * b - 4 * a * c;
            if (positiveSqrtD)
                return (float)((-b + Math.Sqrt(D)) / (2 * a));
            else
                return (float)((-b - Math.Sqrt(D)) / (2 * a));
        }
    }
}
