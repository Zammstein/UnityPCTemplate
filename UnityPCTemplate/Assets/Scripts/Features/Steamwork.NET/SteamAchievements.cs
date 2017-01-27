using UnityEngine;
using Steamworks;
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

        ///<summary
        /// First it checks if the achievement is already unlocked by the user, if it has not been unlocked it will be set to unlocked and the unlock will be saved 
        ///</summary>
        public static void UnlockAchievement(string AchievementName) {
            if (SteamManager.Initialized) {
                bool AchievementUnlocked;
                SteamUserStats.GetAchievement(AchievementName, out AchievementUnlocked); //Get current unlock state of the achievement

                if (!AchievementUnlocked) {
                    SteamUserStats.SetAchievement(AchievementName); //Set the achievement to unlocked
                    Debug.Log("[SteamAchievements]: " + "Achievement: " + AchievementName + " unlocked!");
                    SteamUserStats.StoreStats(); //Store the stats (unlocked achievement) - this also triggers the Achievement Popup
                }
            }
        }
    }
}
