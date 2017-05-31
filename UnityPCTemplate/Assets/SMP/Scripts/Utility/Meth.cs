namespace SMP.Utility {

    /// <summary>
    /// Meth
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Utility class for mathematical functions.
    /// </summary>
    public class Meth {

        /// <summary>
        /// Normalizes the VALUE passed on a scale from MIN to MAX.
        /// </summary>
        /// <param name="value">A value between 0 - 1 that will be converted</param>
        /// <param name="max">The maximum value of the scale</param>
        /// <param name="min">The minimum value of the scale</param>
        /// <returns>The value passed converted on a scale from MIN - MAX</returns>
        public static float NormalizeToScale(float value, float max, float min) {
            if (value <= 0)
                return min;
            else if (value >= 1)
                return max;
            return ((max - min) * value) + min;
        }
    }
}
