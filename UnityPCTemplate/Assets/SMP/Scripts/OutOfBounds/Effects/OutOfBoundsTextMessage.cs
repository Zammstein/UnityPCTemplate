using Core.EventSystem;
using SMP.CameraEffects;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsTextMessage
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// The Text Message effect will display a textual message in front of the player when out of bounds.
    /// </summary>
    public class OutOfBoundsTextMessage : OutOfBoundsEffect {

        /// <summary>
        /// The message displayed when the player enters the Out of Bounds area.
        /// </summary>
        public string message = "OUT OF BOUNDS!";

        private void Start() {
            effectType = EFFECT_TYPE.TEXT_MESSAGE;
        }

        public override void EndEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, false);
        }

        public override void StartEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, true, message);
        }
    }
}
