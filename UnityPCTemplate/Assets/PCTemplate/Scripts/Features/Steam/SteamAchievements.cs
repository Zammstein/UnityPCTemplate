using UnityEngine;
using Steamworks;
using Core.EventSystem;
using System;
/// <summary>
/// SteamAchievements.cs
/// <summary>
/// Author: Thomas van Opstal
/// <summary>
/// Allows the unlocking of Steam achievements anywhere in code. This is initialized at the start of your game.
///</summary>
namespace Features.Steam {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SteamManager))]
    public class SteamAchievements : MonoBehaviour {
        #region Public
        public static SteamAchievements instance = null;
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
            EventManager.StartListening(SteamEventTypes.GLOBAL_STEAM_UNLOCK_ACHIEVEMENT, OnAchievementUnlocked);
        }

        private void OnDestroy() {
            EventManager.StopListening(SteamEventTypes.GLOBAL_STEAM_UNLOCK_ACHIEVEMENT, OnAchievementUnlocked);
        }

        ///<summary>
        /// First it checks if the achievement is already unlocked by the user, if it has not been unlocked it will be set to unlocked and the unlock will be saved (unlock)
        ///</summary>
        private void OnAchievementUnlocked(object[] arg0) {
            string achievementName = (string)arg0[0];
            if (SteamManager.Initialized) {
                bool AchievementUnlocked;
                SteamUserStats.GetAchievement(achievementName, out AchievementUnlocked); //Get current unlock state of the achievement

                if (!AchievementUnlocked) {
                    SteamUserStats.SetAchievement(achievementName); //Set the achievement to unlocked
                    Debug.Log("[SteamAchievements]: " + "Achievement: " + achievementName + " unlocked!");
                    SteamUserStats.StoreStats(); //Store the stats (unlocked achievement) - this also triggers the Achievement Popup
                }
            }
        }
    }
}
