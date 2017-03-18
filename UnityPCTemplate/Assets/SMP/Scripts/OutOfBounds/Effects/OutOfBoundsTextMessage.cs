using Core.EventSystem;
using SMP.CameraEffects;

namespace SMP.OutOfBounds {
    public class OutOfBoundsTextMessage : OutOfBoundsEffect {

        private void Start() {
            effectType = EFFECT_TYPE.TEXT_MESSAGE;
        }

        public override void EndEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, false);
        }

        public override void StartEffect() {
            EventManager.TriggerEvent(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, true);
        }
    }
}
