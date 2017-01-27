using UnityEngine;
using Steamworks;

namespace Features.Steam {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SteamManager))]
    public class SteamAvatar : MonoBehaviour {
        #region Public
        public static SteamAvatar instance = null;
        #endregion

        private void OnAwake() {
            #region Make sure only one instance exists
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            #endregion
        }

        /// <summary>
        /// Get the Small sized(32x32) SteamAvatar of a specific user.
        /// </summary>
        /// <param name="steamID">CSteamID of the player</param>
        /// <returns>SteamAvatar as Texture2D</returns>
        public Texture2D GetSmallSteamAvatar(CSteamID steamID) {
            if (SteamManager.Initialized) {
                int avatarID = SteamFriends.GetSmallFriendAvatar(steamID);
                uint imageWidth;
                uint imageHeight;
                bool success = SteamUtils.GetImageSize(avatarID, out imageWidth, out imageHeight);

                if (success && imageWidth > 0 && imageHeight > 0) {
                    byte[] image = new byte[imageWidth * imageHeight * 4];
                    Texture2D avatarTexture = new Texture2D((int)imageWidth, (int)imageHeight, TextureFormat.RGBA32, false, true);
                    success = SteamUtils.GetImageRGBA(avatarID, image, (int)(imageWidth * imageHeight * 4));

                    if (success) {
                        avatarTexture.LoadRawTextureData(image);
                        avatarTexture.Apply();

                        //Convert to PNG to increase quality
                        byte[] avatarPNG = avatarTexture.EncodeToPNG();
                        avatarTexture = null;
                        avatarTexture = new Texture2D(1, 1);
                        avatarTexture.LoadImage(avatarPNG);
                    }

                    return avatarTexture;
                } else {
                    Debug.Log("[SteamAvatar]: " + "Failed to get Image");
                    return new Texture2D(0, 0);
                }
            } else {
                Debug.Log("[SteamAvatar]: " + "SteamManager not Initialized");
                return new Texture2D(0, 0);
            }
        }

        /// <summary>
        /// Get the Medium sized(64x64) SteamAvatar of a specific user.
        /// </summary>
        /// <param name="steamID">CSteamID of the player</param>
        /// <returns>SteamAvatar as Texture2D</returns>
        public Texture2D GetMediumSteamAvatar(CSteamID steamID) {
            if (SteamManager.Initialized) {
                int avatarID = SteamFriends.GetMediumFriendAvatar(steamID);
                uint imageWidth;
                uint imageHeight;
                bool success = SteamUtils.GetImageSize(avatarID, out imageWidth, out imageHeight);

                if (success && imageWidth > 0 && imageHeight > 0) {
                    byte[] image = new byte[imageWidth * imageHeight * 4];
                    Texture2D avatarTexture = new Texture2D((int)imageWidth, (int)imageHeight, TextureFormat.RGBA32, false, true);
                    success = SteamUtils.GetImageRGBA(avatarID, image, (int)(imageWidth * imageHeight * 4));

                    if (success) {
                        avatarTexture.LoadRawTextureData(image);
                        avatarTexture.Apply();

                        //Convert to PNG to increase quality
                        byte[] avatarPNG = avatarTexture.EncodeToPNG();
                        avatarTexture = null;
                        avatarTexture = new Texture2D(1, 1);
                        avatarTexture.LoadImage(avatarPNG);
                    }

                    return avatarTexture;
                } else {
                    Debug.Log("[SteamAvatar]: " + "Failed to get Image");
                    return new Texture2D(0, 0);
                }
            } else {
                Debug.Log("[SteamAvatar]: " + "SteamManager not Initialized");
                return new Texture2D(0, 0);
            }
        }
    }
}
