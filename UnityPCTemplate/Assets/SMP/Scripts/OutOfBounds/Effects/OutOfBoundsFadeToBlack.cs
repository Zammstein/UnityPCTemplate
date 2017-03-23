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

        private void Start() {
            effectType = EFFECT_TYPE.FADE_TO_BLACK;
        }

        public override void EndEffect() {
            SteamVR_Fade.Start(Color.clear, fadeSpeed);
        }

        public override void StartEffect() {
            SteamVR_Fade.Start(Color.black, fadeSpeed);
        }
    }
}
