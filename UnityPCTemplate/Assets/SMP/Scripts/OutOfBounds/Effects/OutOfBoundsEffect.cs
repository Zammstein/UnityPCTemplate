using UnityEngine;

namespace SMP.OutOfBounds {
    public abstract class OutOfBoundsEffect : MonoBehaviour, IOutOfBoundsEffect {

        public enum EFFECT_TYPE {
            TEXT_MESSAGE,
            CAMERA_BLUR
        }

        public EFFECT_TYPE effectType;
        
        private bool effectActive;

        public bool EffectIsActive() {
            return effectActive;
        }

        public void SetEffectActive(bool isActive) {
            effectActive = isActive;
        }

        public abstract void EndEffect();
        public abstract void StartEffect();
    }
}
