using Core.EventSystem;
using SMP.CameraEffects;

namespace SMP.OutOfBounds {
    public class OutOfBoundsBlurEffect : OutOfBoundsEffect {
        public override void EndEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, false);
        }

        public override void StartEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, true);
        }
    }
}
