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
    /// 
    /// </summary>
    public class LensFlareController : MonoBehaviour {

        public LensFlare flare;

        public Slider brightnessSlider;
        public LinearMapping linearMapping;
        public Text brightnessValueText;

        public Vector2 brightnessBounds;

        void Start() {
            brightnessSlider.minValue = brightnessBounds.x;
            brightnessSlider.maxValue = brightnessBounds.y;
        }

        void Update() {
            brightnessSlider.value = Meth.Normalize(linearMapping.value, brightnessSlider.maxValue, brightnessSlider.minValue);
            flare.brightness = brightnessSlider.value;
            brightnessValueText.text = Mathf.Floor(brightnessSlider.value).ToString();
        }
    }
}