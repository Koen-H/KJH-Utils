using UnityEngine;

namespace KJH.Utils.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Sets any x y z values of a Vector3
        /// </summary>
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        /// <summary>
        /// Adds to any x y z values of a Vector3
        /// </summary>
        public static Vector3 Add(this Vector3 vector, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }

        /// <summary>
        /// Converts a Vector2 to a Vector3 with a y value of 0.
        /// </summary>
        /// <param name="v2">The Vector2 to convert.</param>
        /// <returns>A Vector3 with the x and z values of the Vector2 and a y value of 0.</returns>
        public static Vector3 ToVector3(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }
    }
}
