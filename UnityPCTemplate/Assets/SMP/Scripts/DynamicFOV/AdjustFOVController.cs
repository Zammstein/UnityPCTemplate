using Core.EventSystem;
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
        /// This value will be set on Start to overwrite the default values applied by the connected HMD.
        /// </summary>
        [Range(50f, 150f)]
        public float defaultFOV = 100f;

        /// <summary>
        /// FOV values will be applied to this camera
        /// </summary>
        public Camera cameraToAdjust;

        /// <summary>
        /// Total time it takes to fade out, apply new FOV value and fade back in.
        /// </summary>
        [Range(0.01f, 4f)]
        public float transitionTime = 1f;

        public Slider fovSlider;
        public LinearMapping sliderMapping;

        void Start() {
            cameraToAdjust.fieldOfView = defaultFOV;
            fovSlider.value = defaultFOV;
            sliderMapping.value = (defaultFOV - fovSlider.minValue) / (fovSlider.maxValue - fovSlider.minValue);
        }

        void Update() {
            fovSlider.value = ((fovSlider.maxValue - fovSlider.minValue) * sliderMapping.value) + fovSlider.minValue;
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
