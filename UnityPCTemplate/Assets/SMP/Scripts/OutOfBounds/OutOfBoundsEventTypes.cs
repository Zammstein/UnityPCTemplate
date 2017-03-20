namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsEventTypes
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Event types used to communicate out of bounds triggers.
    /// </summary>
    public class OutOfBoundsEventTypes {

        /// <summary>
        /// Event used by this feature internally to notify that the player entered an out of bounds trigger area.
        /// 
        /// Argument(s) required:
        /// 1. OutOfBoundsEffect.EFFECT_TYPE: The effect type of the area
        /// </summary>
        public const string INTERNAL_PLAYER_ENTERED = "OUT_OF_BOUNDS_INTERNAL_PLAYER_ENTERED";

        /// <summary>
        /// Event used by this feature internally to notify that the player has left an out of bounds trigger area.
        /// 
        /// Argument(s) required:
        /// 1. OutOfBoundsEffect.EFFECT_TYPE: The effect type of the area
        /// </summary>
        public const string INTERNAL_PLAYER_EXIT = "OUT_OF_BOUNDS_INTERNAL_PLAYER_EXIT";
    }
}
