using UnityEngine;
using UnityEngine.UI;

namespace Core.SceneLoading {

    /// <summary>
    /// SceneLoadingView
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Simple script used for updating the progress bar in the Loading scene. 
    /// </summary>
    public class SceneLoadingView : MonoBehaviour {

        /// <summary>
        /// The slider in the loading scene. Its value will be set equal to the SceneLoadingController' loading progress.
        /// </summary>
        public Slider progresBar;

        void Update() {
            progresBar.value = SceneLoadingController.GetLoadingProgess();
        }
    }
}
