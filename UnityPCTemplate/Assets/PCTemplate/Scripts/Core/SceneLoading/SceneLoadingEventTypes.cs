namespace Core.SceneLoading {

    /// <summary>
    /// SceneLoadingEventTypes
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Events used to communicate with the SceneLoadingController. 
    /// </summary>
    public class SceneLoadingEventTypes {

        /// <summary>
        /// Dispatch this to transition between the current scene and the scene given as argument. 
        /// 
        /// Argument(s) required:
        /// 1. SceneLoadingController.Scenes: The scene that the SceneLoadingController will load and move to.
        /// </summary>
        public const string LOAD_SCENE = "SCENE_LOADING_EVENTS_LOAD_SCENE";

        /// <summary>
        /// The following commands are used when the scene fading feature is enabled in a scene.
        /// No Arguments required.
        /// </summary>
        public const string FADE_IN = "SCENE_LOADING_EVENTS_FADE_IN";
        public const string FADE_IN_COMPLETE = "SCENE_LOADING_EVENTS_FADE_IN_COMPLETE";
        public const string FADE_OUT = "SCENE_LOADING_EVENTS_FADE_OUT";
        public const string FADE_OUT_COMPLETE = "SCENE_LOADING_EVENTS_FADE_OUT_COMPLETE";
        public const string FADE_OUT_LOADING_SCREEN_COMPLETE = "SCENE_LOADING_EVENTS_FADE_OUT_LOADING_SCREEN_COMPLETE";
    }
}
