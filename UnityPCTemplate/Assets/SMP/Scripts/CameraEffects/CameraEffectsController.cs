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
        /// Image used to fade to when testing in the editor without VR HMD.
        /// </summary>
        public Image fadingCanvasImage;

        /// <summary>
        /// Canvas holding the message displayed when the player triggers an out of bounds area.
        /// </summary>
        public Canvas outOfBoundsMessageCanvas;

        /// <summary>
        /// The default unity camera blur script. disabled by default.
        /// </summary>
        private BlurOptimized blurScript;

        void Start() {
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, OnTextMessageToggled);
            EventManager.StartListening(CameraEffectsEventTypes.GLOBAL_FADE_SCREEN, OnScreenFadeTransition);

            blurScript = GetComponent<BlurOptimized>();
        }

        void OnDestroy() {
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_BLUR_EFFECT, OnBlurEffectToggled);
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_TOGGLE_TEXT_MESSAGE, OnTextMessageToggled);
            EventManager.StopListening(CameraEffectsEventTypes.GLOBAL_FADE_SCREEN, OnScreenFadeTransition);
        }

        private void OnScreenFadeTransition(object[] arg0) {
            Color color = (Color)arg0[0];
            float fadeSpeed = (float)arg0[1];

            // Handles the fading when in VR
            SteamVR_Fade.Start(color, fadeSpeed);

            // Handles the fading when testing in the editor
            fadingCanvasImage.color = color;
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
            outOfBoundsMessageCanvas.gameObject.SetActive(enabled);
            if (enabled)
                outOfBoundsMessageCanvas.GetComponentInChildren<Text>().text = (string)arg0[1];
        }
    }
}
