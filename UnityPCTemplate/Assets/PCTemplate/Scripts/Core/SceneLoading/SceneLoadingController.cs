using UnityEngine;
using UnityEngine.SceneManagement;
using Core.EventSystem;

namespace Core.SceneLoading {

    /// <summary>
    /// SceneLoadingController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Script that handles async loading of scenes. To load a scene call 'LOAD_SCENE' command 
    /// with a 'SceneLoadingController.Scenes' as argument. When a 'SceneTransitionFadingPanel' prefab 
    /// is active in a scene, this loading controller will call the 'FADE_OUT' command before 
    /// transitioning scenes. 
    /// </summary>
    public class SceneLoadingController : MonoBehaviour {

        /// <summary>
        /// Scenes represent all scenes active in the player' build list.
        /// Add new scenes here with the corresponding build index.
        /// </summary>
        public enum Scenes {
            SPLASH_SCREEN = 0,
            LOADING_SCENE = 1,
            MAIN_MENU = 2,
            OPTIONS_MENU = 3,
            GAME_SCENE = 4
        }
        
        /// <summary>
        /// The loading progress represented as a float between 0 and 1.
        /// </summary>
        private static float loadingProgress;

        /// <summary>
        /// The 'SceneLoadingController.Scenes' that is queued for loading.
        /// </summary>
        private Scenes sceneToBeLoaded;

        /// <summary>
        /// Async operation used for loading the next scene.
        /// </summary>
        private AsyncOperation async;

        /// <summary>
        /// Simple boolean set when the async progress reaches 0.9f. 
        /// The field 'async.isDone' cannot be used since the async progress
        /// never reaches 1. 
        /// </summary>
        private bool asyncComplete;

        /// <summary>
        /// Function is called after the 'LOAD_SCENE' event has been triggered. 
        /// If a 'SceneTransitionFadingPanel' is active in the current scene, the fade out command is
        /// triggered first before the loading scene is called.
        /// </summary>
        /// <param name="args0">Takes one argument: a 'SceneLoadingController.Scenes' to indicate wich scene
        /// should be loaded.</param>
        private void LoadScene(object[] args0) {
            sceneToBeLoaded = (Scenes)args0[0];
            
            FadingView fv = FindObjectOfType<FadingView>();
            if (fv != null)
                EventManager.TriggerEvent(SceneLoadingEventTypes.FADE_OUT, SceneLoadingEventTypes.FADE_OUT_COMPLETE);
            else
                EventManager.TriggerEvent(SceneLoadingEventTypes.FADE_OUT_COMPLETE);
        }

        void Start() {
            EventManager.StartListening(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, LoadScene);
            EventManager.StartListening(SceneLoadingEventTypes.FADE_OUT_COMPLETE, SceneReadyForSwitch);
            EventManager.StartListening(SceneLoadingEventTypes.FADE_OUT_LOADING_SCREEN_COMPLETE, SwitchAfterFadeOut);
        }

        void OnDestroy() {
            EventManager.StopListening(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, LoadScene);
            EventManager.StopListening(SceneLoadingEventTypes.FADE_OUT_COMPLETE, SceneReadyForSwitch);
            EventManager.StopListening(SceneLoadingEventTypes.FADE_OUT_LOADING_SCREEN_COMPLETE, SwitchAfterFadeOut);
        }

        /// <summary>
        /// Triggered when the fade out of the current scene has ended or no fading is necessary.
        /// This function will show the loading scene and start loading the next scene with a small delay (0.1f);
        /// </summary>
        /// <param name="arg0">Not used. Can be null.</param>
        void SceneReadyForSwitch(object[] arg0) {
            SceneManager.LoadScene((int)Scenes.LOADING_SCENE);
            Invoke("StartLoadingNewScene", 0.1f);
        }

        /// <summary>
        /// Use the unity scenemanager to start loading the next scene async.
        /// </summary>
        void StartLoadingNewScene() {
            async = SceneManager.LoadSceneAsync((int)sceneToBeLoaded);
            async.allowSceneActivation = false;
            asyncComplete = false;
        }

        /// <summary>
        /// If a scene is loading in the background, check its progress and call 'SwitchToNewScene' when ready.
        /// </summary>
        void Update() {
            if (async != null && !asyncComplete) {
                loadingProgress = async.progress;
                if (async.progress >= 0.9f) {
                    SwitchToNewScene();
                    asyncComplete = true;
                }
            }
        }

        /// <summary>
        /// If a 'SceneTransitionFadingPanel' is active in the loading scene, call for fading before transitioning to the new scene.
        /// </summary>
        void SwitchToNewScene() {
            FadingView fv = FindObjectOfType<FadingView>();
            if (fv != null)
                EventManager.TriggerEvent(SceneLoadingEventTypes.FADE_OUT, SceneLoadingEventTypes.FADE_OUT_LOADING_SCREEN_COMPLETE);
            else 
                async.allowSceneActivation = true;
        }

        void SwitchAfterFadeOut(object[] args0) {
            async.allowSceneActivation = true;
        }

        /// <summary>
        /// Encapsulation of the 'loadingProgress' field.
        /// </summary>
        /// <returns>A float between 0 and 1.</returns>
        public static float GetLoadingProgess() {
            return loadingProgress;
        }
    }
}