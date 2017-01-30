using UnityEngine;
using Steamworks;

namespace Features.Steam {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SteamManager))]
    public class SteamAvatar : MonoBehaviour {

        #region Avatar Size enum
        /// <summary>
        /// Small = (32 x 32)
        /// Medium = (64 x 64)
        /// Large = (128 x 128)
        /// </summary>
        public enum AVATAR_SIZE {
            SMALL,
            MEDIUM,
            LARGE
        }
        #endregion

        #region Initialization
        private static SteamAvatar steamAvatar;

        public static SteamAvatar instance {
            get {
                if (!steamAvatar) {
                    steamAvatar = FindObjectOfType(typeof(SteamAvatar)) as SteamAvatar;

                    if (!steamAvatar)
                        Debug.LogError("There needs to be one active SteamAvatar script on a GameObject in your scene.");
                }
                return steamAvatar;
            }
        }

        void Awake() {
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Get Avatar
        /// <summary>
        /// Get the SteamAvatar of a specific user.
        /// </summary>
        /// <param name="steamID">CSteamID of the player</param>
        /// <param name="size">AVATAR_SIZE defining the size to return</param>
        /// <returns>SteamAvatar as Texture2D</returns>
        public Texture2D GetSteamAvatar(CSteamID steamID, AVATAR_SIZE size) {
            if (SteamManager.Initialized) {
                int avatarID = -1;
                switch (size) {
                    case AVATAR_SIZE.SMALL:
                        avatarID = SteamFriends.GetSmallFriendAvatar(steamID);
                        break;
                    case AVATAR_SIZE.MEDIUM:
                        avatarID = SteamFriends.GetMediumFriendAvatar(steamID);
                        break;
                    case AVATAR_SIZE.LARGE:
                        avatarID = SteamFriends.GetLargeFriendAvatar(steamID);
                        break;
                }

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
                    Debug.LogError("[SteamAvatar]: " + "Failed to get Image");
                    return new Texture2D(0, 0);
                }
            } else {
                Debug.LogError("[SteamAvatar]: " + "SteamManager not Initialized");
                return new Texture2D(0, 0);
            }
        }
        #endregion
    }
}
