using System;
using Core.EventSystem;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace SMP.CameraEffects {

    [RequireComponent(typeof(BlurOptimized))]
    public class CameraEffectsController : MonoBehaviour {

        public Canvas OutOfBoundsMessageCanvas;

        private BlurOptimized blurScript;

        void Start() {
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, OnTextMessageToggled);

            blurScript = GetComponent<BlurOptimized>();
        }

        void OnDestroy() {
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, OnTextMessageToggled);
        }

        private void OnBlurEffectToggled(object[] arg0) {
            blurScript.enabled = (bool)arg0[0];
        }

        private void OnTextMessageToggled(object[] arg0) {
            OutOfBoundsMessageCanvas.gameObject.SetActive((bool)arg0[0]);
        }
    }
}
