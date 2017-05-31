using Core.EventSystem;
using SMP.CameraEffects;
using UnityEngine;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsFadeToBlack
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// The Fade to Black effect will fade the player' camera to black when entered an out of bounds area.
    /// </summary>
    public class OutOfBoundsFadeToBlack : OutOfBoundsEffect {

        /// <summary>
        /// The speed used to fade between values
        /// </summary>
        public float fadeSpeed = 0.5f;

        private void Awake() {
            effectType = EFFECT_TYPE.FADE_TO_BLACK;
        }

        public override void EndEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_FADE_SCREEN, Color.clear, fadeSpeed);
        }

        public override void StartEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_FADE_SCREEN, Color.black, fadeSpeed);
        }
    }
}
