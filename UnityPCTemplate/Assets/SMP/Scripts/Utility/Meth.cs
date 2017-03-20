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
        /// Normalizes the value passed on a scale from 0 - 1.
        /// </summary>
        /// <param name="value">The value that will be converted</param>
        /// <param name="max">The maximum value of the scale (0)</param>
        /// <param name="min">The minimum value of the scale (1)</param>
        /// <returns>The value passed converted on a scale from 0 - 1</returns>
        public static float Normalize(float value, float max, float min) {
            return ((max - min) * value) + min;
        }
    }
}
