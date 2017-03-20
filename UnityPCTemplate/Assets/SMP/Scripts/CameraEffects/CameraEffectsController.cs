using Core.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace SMP.CameraEffects {

    /// <summary>
    /// CameraEffectsController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Controlling different camera effects
    /// </summary>
    [RequireComponent(typeof(BlurOptimized))]
    public class CameraEffectsController : MonoBehaviour {

        /// <summary>
        /// Canvas holding the message displayed when the player triggers an out of bounds area.
        /// </summary>
        public Canvas OutOfBoundsMessageCanvas;

        /// <summary>
        /// The default unity camera blur script. disabled by default.
        /// </summary>
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

        /// <summary>
        /// Enables / disables the out of bounds canvas and sets the message.
        /// </summary>
        /// <param name="arg0">See CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE for more info.</param>
        private void OnTextMessageToggled(object[] arg0) {
            bool enabled = (bool)arg0[0];
            OutOfBoundsMessageCanvas.gameObject.SetActive(enabled);
            if (enabled)
                OutOfBoundsMessageCanvas.GetComponentInChildren<Text>().text = (string)arg0[1];
        }
    }
}
