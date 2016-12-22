using UnityEngine;

namespace Networking.IdentifyingConnection {

    /// <summary>
    /// NetworkStatusModel
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Model containing information about the nework status. 
    /// </summary>
    public class NetworkStatusModel {

        /// <summary>
        /// Simplyfied representations of the different states the network can be in.
        /// </summary>
        public enum Status {
            CONNECTED,
            LIMITED,
            NO_CONNECTION,
            UNKNOWN
        }

        /// <summary>
        /// Field containing the status as mentioned above.
        /// </summary>
        public Status status { get; private set; }

        /// <summary>
        /// Connection test result in more detail.
        /// </summary>
        public ConnectionTesterStatus connectionTestResult { get; private set; }

        /// <summary>
        /// The message that can be shown to a user. This should not contain complicated information (in most cases).
        /// All possible types of messages come from the ConnectionUtility script.
        /// </summary>
        public string userDisplayedMessage { get; private set; }

        /// <summary>
        /// External IP of the device the game is running on.
        /// </summary>
        public string externalIP { get; private set; }

        /// <summary>
        /// Is a NAT punchthrough required with the current connection?
        /// </summary>
        public bool useNat { get; private set; }

        public NetworkStatusModel(Status status, ConnectionTesterStatus connectionTestResult, string userDisplayedMessage, string externalIP, bool useNat) {
            this.status = status;
            this.connectionTestResult = connectionTestResult;
            this.userDisplayedMessage = userDisplayedMessage;
            this.externalIP = externalIP;
            this.useNat = useNat;
        }
    }
}
