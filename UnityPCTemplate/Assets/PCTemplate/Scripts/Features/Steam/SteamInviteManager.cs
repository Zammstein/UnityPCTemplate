using System;
using UnityEngine;
using Steamworks;

namespace Features.Steam
{
    [DisallowMultipleComponent]
    [RequireComponent (typeof (SteamManager))] //Make sure the SteamManager is always active aswell.
    public class SteamInviteManager : MonoBehaviour
    {
        #region Public
        public static SteamInviteManager instance = null; //Static instance of SteamInviteManager which allows it to be accessed by any other script.
        #endregion

        #region Private
        private ulong current_LobbyID; //id of the lobby the player is currently in.
        private ulong lobbyToJoinID; //id of the lobby that should be joined.
        #endregion

        #region Protected
        protected CallResult<LobbyCreated_t> Callresult_onLobbyCreated;
        protected Callback<GameLobbyJoinRequested_t> Callback_gameLobbyJoinRequested;
        protected Callback<LobbyEnter_t> Callback_lobbyEnter;
        #endregion

        private void Awake() {
            #region Make sure only one instance exists
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            #endregion
        }

        private void Start() {
            if (SteamManager.Initialized) {
                #region Subscribe to certain Steam callbacks/callresults
                Callresult_onLobbyCreated = CallResult<LobbyCreated_t>.Create(OnLobbyCreated);
                Callback_gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
                Callback_lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
                #endregion
            }
        }

        private void ParseStartUpParams(string[] startParams) {
            for (int i = 0; i < startParams.Length; i++) {
                if (startParams[i] == "+connect_lobby") {
                    lobbyToJoinID = ulong.Parse(startParams[i + 1]);
                    //Code that should be excecuted when "+connect_lobby" param is found by player accepting an invite while outside of the game.
                }
            }
        }

        #region Excecute on steam Callbacks/Callresults
        /// <summary>
        /// Excecuted when a player creates a steam lobby.
        /// </summary>
        /// <param name="result">Contains the id of the lobby.</param>
        /// <param name="bIOFailure">Has an error occured?</param>
        private void OnLobbyCreated(LobbyCreated_t result, bool bIOFailure) {
            current_LobbyID = (ulong)result.m_ulSteamIDLobby; //Set the current lobby ID.
            Debug.Log("[STEAM][SteamInviteManager]: Lobby successfully created!");
            //Code that should be excecuted when the player created a lobby.
        }

        /// <summary>
        /// Excecuted when a player accepts an ingame invitation to join a game (always from a friend).
        /// </summary>
        /// <param name="result">Contains the lobby id and the id of the friend that invite the player.</param>
        private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result) {
            lobbyToJoinID = (ulong)result.m_steamIDLobby; //Set the id of the lobby that should be joined.
            Debug.Log("[STEAM][SteamInviteManager]: Accepted game invite!");
            //Code that should be excecuted when the player accepts an invite while ingame.
        }

        /// <summary>
        /// Excecute when a player has entered a steam lobby.
        /// </summary>
        /// <param name="result">Contains the id of the lobby the player joined, also contains permissions player has in lobby.</param>
        private void OnLobbyEntered(LobbyEnter_t result) {
            current_LobbyID = (ulong)result.m_ulSteamIDLobby; //Set the current lobby ID.
            Debug.Log("[STEAM][SteamInviteManager]: Lobby successfully joined!");
            //Code that should be excecuted when the player has joined the lobby.
        }
        #endregion

        #region Getters
        /// <summary>
        /// Get the steam lobby id the player is currently in.
        /// </summary>
        /// <returns>returns the current_LobbyID.</returns>
        public ulong GetCurrentSteamLobbyID() {
            return current_LobbyID;
        }

        /// <summary>
        /// Get the steam lobby id of the lobby that should be joined.
        /// </summary>
        /// <returns>returns the lobbyToJoinID.</returns>
        public ulong GetSteamLobbyToJoinID() {
            return lobbyToJoinID;
        }
        #endregion
    }
}
