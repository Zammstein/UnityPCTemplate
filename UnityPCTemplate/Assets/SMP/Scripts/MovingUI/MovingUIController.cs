using UnityEngine;
using UnityEngine.UI;

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

        public void OnScaleSliderValueChanged() {
            rectTransform.localScale = new Vector3(scaleBounds.x * scaleSlider.value, scaleBounds.y * scaleSlider.value, rectTransform.localScale.z);
        }

        public void OnDistanceSliderValueChanged() {
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, distanceBounds.x + (distanceSlider.value * (distanceBounds.y - distanceBounds.x)));
        }

        public void OnWidthHeightSliderValueChanged() {
            rectTransform.sizeDelta = new Vector2(widthHeightSlider.value * widthHeightBounds.x, widthHeightSlider.value * widthHeightBounds.y);
        }
    }
}
