using SMP.Utility;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace SMP.MovingUI {
    
    public class MovingUIController : MonoBehaviour {

        /// <summary>
        /// The canvas on which the transform mutation will be performed;
        /// </summary>
        public RectTransform rectTransform;

        public Slider scaleSlider;
        public Slider distanceSlider;
        public Slider widthHeightSlider;

        public Vector2 scaleBounds;
        public Vector2 distanceBounds;
        public Vector2 widthHeightBounds;

        public LinearMapping scaleMapping;
        public LinearMapping distanceMapping;
        public LinearMapping widthHeightMapping;

        void Start() {
            scaleSlider.minValue = scaleBounds.x;
            scaleSlider.maxValue = scaleBounds.y;

            distanceSlider.minValue = distanceBounds.x;
            distanceSlider.maxValue = distanceBounds.y;

            widthHeightSlider.minValue = widthHeightBounds.x;
            widthHeightSlider.maxValue = widthHeightBounds.y;

        }

        void Update() {
            scaleSlider.value = Meth.Normalize(scaleMapping.value, scaleSlider.maxValue, scaleSlider.minValue);
            distanceSlider.value = Meth.Normalize(distanceMapping.value, distanceSlider.maxValue, distanceSlider.minValue);
            widthHeightSlider.value = Meth.Normalize(widthHeightMapping.value, widthHeightSlider.maxValue, widthHeightSlider.minValue);

            rectTransform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, rectTransform.localScale.z);
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, distanceSlider.value);
            rectTransform.sizeDelta = new Vector2(widthHeightSlider.value, widthHeightSlider.value);
        }
    }
}
