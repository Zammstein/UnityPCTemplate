using SMP.Utility;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace SMP.MovingUI {

    /// <summary>
    /// MovingUIController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// This class handles all changes made to control panel' UI slider(s) and applies the values to a canvas object.
    /// </summary>
    public class MovingUIController : MonoBehaviour {

        #region publics
        /// <summary>
        /// The canvas on which the transform mutation will be performed;
        /// </summary>
        public RectTransform rectTransform;

        /// <summary>
        /// Canvas slider displaying current UI plane scale setting.
        /// Scale does not affect the resolution of the canvas.
        /// </summary>
        public Slider scaleSlider;

        /// <summary>
        /// Canvas slider displaying current UI plane distance setting.
        /// </summary>
        public Slider distanceSlider;

        /// <summary>
        /// Canvas slider displaying current UI plane width and height setting.
        /// Width and height affect the canvas' resolution.
        /// </summary>
        public Slider widthHeightSlider;

        /// <summary>
        /// Bounds set are applied to the corresponding slider.
        /// </summary>
        public Vector2 scaleBounds;
        public Vector2 distanceBounds;
        public Vector2 widthHeightBounds;

        /// <summary>
        /// Mappings will be applied to the corresponding slider.
        /// </summary>
        public LinearMapping scaleMapping;
        public LinearMapping distanceMapping;
        public LinearMapping widthHeightMapping;

        /// <summary>
        /// UI text elements displaying the current value of the corresponding sliders.
        /// </summary>
        public Text scaleValueText;
        public Text distanceValueText;
        public Text widthHeightValueText;
        #endregion

        void Start() {
            scaleSlider.minValue = scaleBounds.x;
            scaleSlider.maxValue = scaleBounds.y;

            distanceSlider.minValue = distanceBounds.x;
            distanceSlider.maxValue = distanceBounds.y;

            widthHeightSlider.minValue = widthHeightBounds.x;
            widthHeightSlider.maxValue = widthHeightBounds.y;

        }

        /// <summary>
        /// Visual representations are updated with the mapped values.
        /// </summary>
        void Update() {
            scaleSlider.value = Meth.Normalize(scaleMapping.value, scaleSlider.maxValue, scaleSlider.minValue);
            distanceSlider.value = Meth.Normalize(distanceMapping.value, distanceSlider.maxValue, distanceSlider.minValue);
            widthHeightSlider.value = Meth.Normalize(widthHeightMapping.value, widthHeightSlider.maxValue, widthHeightSlider.minValue);

            rectTransform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, rectTransform.localScale.z);
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, distanceSlider.value);
            rectTransform.sizeDelta = new Vector2(widthHeightSlider.value, widthHeightSlider.value);

            scaleValueText.text = scaleSlider.value.ToString();
            distanceValueText.text = Mathf.Floor(distanceSlider.value).ToString();
            widthHeightValueText.text = Mathf.Floor(widthHeightSlider.value).ToString();
        }
    }
}
