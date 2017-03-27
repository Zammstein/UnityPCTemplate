using Core.EventSystem;
using SMP.CameraEffects;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsBlurEffect
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// The blur effect toggles a camera blur for the player.
    /// </summary>
    public class OutOfBoundsBlurEffect : OutOfBoundsEffect {

        private void Awake() {
            effectType = EFFECT_TYPE.CAMERA_BLUR;
        } 

        public override void EndEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, false);
        }

        public override void StartEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, true);
        }
    }
}
