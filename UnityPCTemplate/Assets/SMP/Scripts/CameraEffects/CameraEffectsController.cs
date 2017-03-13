using Core.EventSystem;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace SMP.CameraEffects {

    [RequireComponent(typeof(BlurOptimized))]
    public class CameraEffectsController : MonoBehaviour {
        private BlurOptimized blurScript;

        void Start() {
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);

            blurScript = GetComponent<BlurOptimized>();
        }

        void OnDestroy() {
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);
        }

        private void OnBlurEffectToggled(object[] arg0) {
            blurScript.enabled = (bool)arg0[0];
        }
    }
}
