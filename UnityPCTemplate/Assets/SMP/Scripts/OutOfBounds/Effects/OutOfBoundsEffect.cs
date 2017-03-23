using UnityEngine;


namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsEffect
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Base class for Out of Bounds implementation classes.
    /// </summary>
    public abstract class OutOfBoundsEffect : MonoBehaviour, IOutOfBoundsEffect {

        /// <summary>
        /// Effect types are stored as enums for readability purposes.
        /// Each type has its own implementation class.
        /// </summary>
        public enum EFFECT_TYPE {
            TEXT_MESSAGE,
            CAMERA_BLUR,
            FADE_TO_BLACK
        }

        /// <summary>
        /// Each implementation of this class should store its type in this value.
        /// </summary>
        protected EFFECT_TYPE effectType;

        public EFFECT_TYPE GetEffectType() {
            return effectType;
        }

        public abstract void EndEffect();
        public abstract void StartEffect();
    }
}
