namespace SMP.CameraEffects {
    /// <summary>
    /// CameraEffectsEventTypes
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Event types used to control different camera effects.
    /// </summary>
    public class CameraEffectsEventTypes {

        /// <summary>
        /// Dispatch this to toggle a camera blur effect.
        /// 
        /// Argument(s) required:
        /// 1. bool: enable or disable
        /// </summary>
        public const string GLOBAL_TOGGLE_BLUR_EFFECT = "CAMERA_EFFECTS_GLOBAL_TOGGLE_BLUR_EFFECT";

        /// <summary>
        /// Dispatch this to toggle a text message.
        /// 
        /// Argument(s) required:
        /// 1. bool: enable or disable
        /// 
        /// Argument(s) optional:
        /// 2. string: the message to display if argument 1 is true
        /// </summary>
        public const string GLOBAL_TOGGLE_TEXT_MESSAGE = "CAMERA_EFFECTS_GLOBAL_TOGGLE_TEXT_MESSAGE";
    }
}

