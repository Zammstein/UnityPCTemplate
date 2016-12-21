namespace Networking.IdentifyingConnection {

    /// <summary>
    /// NetworkConnectionEventTypes
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Events used to communicate with the ConnectionUtility. 
    /// </summary>
    public class NetworkConnectionEventTypes {


        /// <summary>
        /// Dispatch this to ask for information about the current internet connection 
        /// of the device the game is running on. 
        /// 
        /// Argument(s) required:
        /// 1. bool: Should NAT punchthrough be taken in consideration
        /// </summary>
        public const string TEST_CONNECTION_STATUS = "TEST_CONNECTION_STATUS";

        /// <summary>
        /// Listen to this event to get information about the current internet connection
        /// of the device the game is running on. 
        /// 
        /// Argument(s) returned:
        /// 1. NetworkStatusModel: The model containing the connection info.
        /// </summary>
        public const string CONNECTION_STATUS_UPDATE = "CONNECTION_STATUS_UPDATE";
    }
}

