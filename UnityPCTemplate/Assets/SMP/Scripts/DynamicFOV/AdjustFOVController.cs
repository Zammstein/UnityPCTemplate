using SMP.Utility;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

namespace SMP.DynamicFOV {

    /// <summary>
    /// AdjustFOVController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// This class handles all changes made to the players camera fov value.
    /// </summary>
    public class AdjustFOVController : MonoBehaviour {

        /// <summary>
        /// FOV values will be applied to this camera when in vr
        /// </summary>
        public Camera vrCamera;

        /// <summary>
        /// FOV values will be applied to this camera when not in vr
        /// </summary>
        public new Camera camera;

        /// <summary>
        /// Total time it takes to fade out, apply new FOV value and fade back in.
        /// </summary>
        [Range(0.01f, 4f)]
        public float transitionTime = 1f;

        public Slider fovSlider;
        public LinearMapping sliderMapping;
        public Text fovValueText;
        public Vector2 fovBounds;

        private Camera cameraToAdjust;

        void Start() {
            cameraToAdjust = vrCamera.gameObject.activeInHierarchy ? vrCamera : camera;
            fovSlider.minValue = fovBounds.x;
            fovSlider.maxValue = fovBounds.y;
        }

        void Update() {
            fovSlider.value = Meth.Normalize(sliderMapping.value, fovSlider.maxValue, fovSlider.minValue);
            fovValueText.text = Mathf.Floor(fovSlider.value).ToString();
        }

        public void OnApplyFOV() {
            SteamVR_Fade.Start(Color.black, transitionTime / 2);
            Invoke("FadeInAndUpdateFOV", transitionTime / 2);
        }

        /// <summary>
        /// Method called after the camera fade out is completed. This changes the camera FOV and calls for fade back in.
        /// </summary>
        void FadeInAndUpdateFOV() {
            cameraToAdjust.fieldOfView = fovSlider.value;
            SteamVR_Fade.Start(Color.clear, transitionTime / 2);
        }
    }
}
