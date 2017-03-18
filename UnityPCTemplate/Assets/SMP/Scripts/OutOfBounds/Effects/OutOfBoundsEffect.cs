using UnityEngine;

namespace SMP.OutOfBounds {
    public abstract class OutOfBoundsEffect : MonoBehaviour, IOutOfBoundsEffect {

        public enum EFFECT_TYPE {
            TEXT_MESSAGE,
            CAMERA_BLUR
        }

        protected EFFECT_TYPE effectType;

        public EFFECT_TYPE GetEffectType() {
            return effectType;
        }

        public abstract void EndEffect();
        public abstract void StartEffect();
    }
}
