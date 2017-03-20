using SMP.Utility;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace SMP.DynamicFOV {

    /// <summary>
    /// LensFlareController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// This class handles all changes made to the control panel' Lens Flare slider(s) and applies the values to the Flare object in the scene.
    /// </summary>
    public class LensFlareController : MonoBehaviour {

        #region publics
        /// <summary>
        /// FLare setting changes are applied to this flare object.
        /// </summary>
        public LensFlare flare;

        /// <summary>
        /// Canvas slider displaying current brightness setting.
        /// </summary>
        public Slider brightnessSlider;

        /// <summary>
        /// Mapping will be applied to the corresponding slider.
        /// </summary>
        public LinearMapping linearMapping;

        /// <summary>
        /// UI text element displaying the current value of the corresponding slider.
        /// </summary>
        public Text brightnessValueText;

        /// <summary>
        /// Bounds set are applied to the corresponding slider
        /// </summary>
        public Vector2 brightnessBounds;
        #endregion

        void Start() {
            brightnessSlider.minValue = brightnessBounds.x;
            brightnessSlider.maxValue = brightnessBounds.y;
        }

        /// <summary>
        /// Visual representations are updated with the mapped values.
        /// </summary>
        void Update() {
            brightnessSlider.value = Meth.Normalize(linearMapping.value, brightnessSlider.maxValue, brightnessSlider.minValue);
            flare.brightness = brightnessSlider.value;
            brightnessValueText.text = Mathf.Floor(brightnessSlider.value).ToString();
        }
    }
}