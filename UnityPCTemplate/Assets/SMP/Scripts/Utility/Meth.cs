namespace SMP.Utility {
    public class Meth {
        public static float Normalize(float value, float max, float min) {
            return ((max - min) * value) + min;
        }
    }
}
