using UnityEngine;
using System;
using System.Net;
using Core.EventSystem;

namespace Features.Networking.IdentifyingConnection {

    /// <summary>
    /// ConnectionUtillity
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// This utillity script can be used to check the current internet connection of the 
    /// device the game is running on. Use the events specified in NetworkConnectionEventTypes
    /// script to ask/retrieve information.
    /// </summary>
    public class ConnectionUtility : MonoBehaviour {
        #region Publics
        [Header("Messages displayed to users about their internet connection status")]
        public string goodConnection = "Internet connection available.";
        public string limitedConnection = "Your current internet connection is limited. You might experience some troubles playing online.";
        public string noConnection = "You are not connected to the internet.";
        #endregion

        #region Privates
        private string testMessage = "Test in progress";
        private bool doneTesting = true;
        private bool probingPublicIP = false;
        private ConnectionTesterStatus connectionTestResult = ConnectionTesterStatus.Undetermined;
        private NetworkStatusModel.Status connectionStatus = NetworkStatusModel.Status.UNKNOWN;
        private float timer;
        private bool useNat;
        private bool careForNATPunchthrough;
        private string externalip = "";
        #endregion

        void Start() {
            EventManager.StartListening(NetworkConnectionEventTypes.GLOBAL_TEST_CONNECTION_STATUS, OnConnectionStatusRequest);
        }

        void OnDestroy() {
            EventManager.StopListening(NetworkConnectionEventTypes.GLOBAL_TEST_CONNECTION_STATUS, OnConnectionStatusRequest);
        }

        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        void OnConnectionStatusRequest(object[] arg0) {
            careForNATPunchthrough = (bool)arg0[0];
            TestConnection();
        }

        void Update() {
            if (!doneTesting)
                TestConnectionNAT();
        }

        /// <summary>
        /// Check basic internet connection. 
        /// </summary>
        void TestConnection() {
            try {
                externalip = new WebClient().DownloadString("http://icanhazip.com");
            } catch (Exception e) {
                Debug.LogError(e.StackTrace);
                externalip = "";
            }

            if (Network.HavePublicAddress() || externalip != "") {
                // start testing connection
                doneTesting = false;
                connectionTestResult = ConnectionTesterStatus.Undetermined;
            } else {
                // dont even start testen connection
                connectionStatus = NetworkStatusModel.Status.NO_CONNECTION;
                connectionTestResult = ConnectionTesterStatus.Error;
                testMessage = noConnection;

                NetworkStatusModel model = new NetworkStatusModel(connectionStatus, connectionTestResult, testMessage, externalip, useNat);
                EventManager.TriggerEvent(NetworkConnectionEventTypes.GLOBAL_CONNECTION_STATUS_UPDATE, model);
            }
        }

        /// <summary>
        /// Test internet connection for more detail.
        /// </summary>
        void TestConnectionNAT() {
            connectionTestResult = Network.TestConnectionNAT();
            switch (connectionTestResult) {
                case ConnectionTesterStatus.Error:
                    testMessage = noConnection;
                    doneTesting = true;
                    connectionStatus = NetworkStatusModel.Status.NO_CONNECTION;
                    break;

                case ConnectionTesterStatus.Undetermined:
                    testMessage = noConnection;
                    doneTesting = false;
                    connectionStatus = NetworkStatusModel.Status.UNKNOWN;
                    break;

                case ConnectionTesterStatus.PublicIPIsConnectable:
                    testMessage = goodConnection;
                    useNat = false;
                    doneTesting = true;
                    connectionStatus = NetworkStatusModel.Status.CONNECTED;
                    break;

                // This case is a bit special as we now need to check if we can 
                // circumvent the blocking by using NAT punchthrough
                case ConnectionTesterStatus.PublicIPPortBlocked:
                    testMessage = limitedConnection;
                    useNat = false;
                    if (!careForNATPunchthrough) {
                        testMessage = goodConnection;
                        useNat = false;
                        doneTesting = true;
                        connectionStatus = NetworkStatusModel.Status.CONNECTED;
                        break;
                    }
                    // If no NAT punchthrough test has been performed on this public 
                    // IP, force a test
                    if (!probingPublicIP) {
                        connectionTestResult = Network.TestConnectionNAT();
                        probingPublicIP = true;
                        timer = Time.time + 10;
                    }
                    // NAT punchthrough test was performed but we still get blocked
                    else if (Time.time > timer) {
                        probingPublicIP = false;        
                        useNat = true;
                        doneTesting = true;
                        connectionStatus = NetworkStatusModel.Status.LIMITED;
                    }
                    break;

                case ConnectionTesterStatus.PublicIPNoServerStarted:
                    testMessage = goodConnection;
                    useNat = true;
                    doneTesting = true;
                    connectionStatus = NetworkStatusModel.Status.CONNECTED;
                    break;

                case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
                case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
                case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
                case ConnectionTesterStatus.NATpunchthroughFullCone:
                    if (!careForNATPunchthrough) {
                        testMessage = goodConnection;
                        useNat = false;
                        doneTesting = true;
                        connectionStatus = NetworkStatusModel.Status.CONNECTED;
                        break;
                    }

                    testMessage = limitedConnection;
                    useNat = true;
                    doneTesting = true;
                    connectionStatus = NetworkStatusModel.Status.LIMITED;
                    break;

                default:
                    testMessage = noConnection;
                    connectionStatus = NetworkStatusModel.Status.NO_CONNECTION;
                    break;
            }

            // If done testing, dispatch an update with a NetworkStatusModel containing all information necessary.
            if (doneTesting) {
                NetworkStatusModel model = new NetworkStatusModel(connectionStatus, connectionTestResult, testMessage, externalip, useNat);
                EventManager.TriggerEvent(NetworkConnectionEventTypes.GLOBAL_CONNECTION_STATUS_UPDATE, model);
            }
        }
    }
}