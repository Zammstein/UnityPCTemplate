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
    /// This class handles all changes made to control panel' FOV slider(s) and applies the values to the camera of the player.
    /// </summary>
    public class AdjustFOVController : MonoBehaviour {

        #region publics
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

        /// <summary>
        /// Canvas slider displaying current fov setting.
        /// </summary>
        public Slider fovSlider;

        /// <summary>
        /// Mapping will be applied to the corresponding slider.
        /// </summary>
        public LinearMapping sliderMapping;

        /// <summary>
        /// UI text element displaying the current value of the corresponding slider.
        /// </summary>
        public Text fovValueText;

        /// <summary>
        /// Bounds set are applied to the corresponding slider
        /// </summary>
        public Vector2 fovBounds;
        #endregion

        /// <summary>
        /// This camera object is assigned on Start with the VR camera or non-VR camera.
        /// FOV changes are applied to this.
        /// </summary>
        private Camera cameraToAdjust;

        void Start() {
            cameraToAdjust = vrCamera.gameObject.activeInHierarchy ? vrCamera : camera;
            fovSlider.minValue = fovBounds.x;
            fovSlider.maxValue = fovBounds.y;
        }

        /// <summary>
        /// Visual representations are updated with the mapped values.
        /// </summary>
        void Update() {
            fovSlider.value = Meth.NormalizeToScale(sliderMapping.value, fovSlider.maxValue, fovSlider.minValue);
            fovValueText.text = Mathf.Floor(fovSlider.value).ToString();
        }

        /// <summary>
        /// The 'apply' UI button triggers this method to start applying the fov value currently in the fov slider.
        /// </summary>
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
