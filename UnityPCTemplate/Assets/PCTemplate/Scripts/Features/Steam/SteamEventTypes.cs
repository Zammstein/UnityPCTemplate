namespace Features.Steam {

    /// <summary>
    /// SteamEventTypes
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Events used to communicate with the Steam feature. 
    /// </summary>
    public class SteamEventTypes {

        /// <summary>
        /// This event can be triggered to unlock a specific Steam achievement
        /// passed as a string.
        /// 
        /// Argument(s) required:
        /// 1. string: Achievement string id.
        /// </summary>
        public const string GLOBAL_STEAM_UNLOCK_ACHIEVEMENT = "GLOBAL_STEAM_UNLOCK_ACHIEVEMENT";
    }
}
