using UnityEngine;

namespace KJH.Utils.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// Checks if the value is between the specified minimum and maximum values (inclusive).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>True if the value is between the minimum and maximum values, otherwise false.</returns>
        public static bool IsBetween(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Checks if the value is between the specified minimum and maximum values (exclusive).
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>True if the value is between the minimum and maximum values, otherwise false.</returns>
        public static bool IsBetweenExclusive(this float value, float min, float max)
        {
            return value > min && value < max;
        }

        /// <summary>
        /// Checks if the value is within a specified epsilon range of the target value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="target">The target value.</param>
        /// <param name="epsilon">The epsilon value (default is 0.0001).</param>
        /// <returns>True if the value is within the epsilon range of the target value, otherwise false.</returns>
        public static bool IsWithinEpsilon(this float value, float target, float epsilon = 0.0001f)
        {
            return Mathf.Abs(value - target) < epsilon;
        }

        /// <summary>
        /// Checks if the value is within a specified threshold range of the target value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="target">The target value.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>True if the value is within the threshold range of the target value, otherwise false.</returns>
        public static bool IsWithinThreshold(this float value, float target, float threshold)
        {
            return IsWithinEpsilon(value, target, threshold);
        }
    }
}
