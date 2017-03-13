using UnityEngine;

namespace SMP.Utility {

    /// <summary>
    /// SwitchMenuForVR
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Utility for switching the canvas rendering mode between the worldspace and ScreenSpaceCamera.
    /// </summary>
    public class SwitchMenuForVR : MonoBehaviour {

        public Canvas canvas; //!< Canvas in the scene
        public Camera mainCamera; //!< Main camera in the scene

        /// <summary>
        /// If VR is detected it will switch to worldspace, otherwise it will run in ScreenSpaceCamera mode 
        /// </summary>
        private void Start() {
            if (SteamVR.instance != null) {
                canvas.renderMode = RenderMode.WorldSpace;
            } else {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = mainCamera;
            }
        }
    }
}
