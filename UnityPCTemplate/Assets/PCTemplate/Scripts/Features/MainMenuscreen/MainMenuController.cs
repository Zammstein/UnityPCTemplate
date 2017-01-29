using Core.EventSystem;
using Core.SceneLoading;
using Features.Steam;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MainMenuScreen {

    /// <summary>
    /// MainMenuController
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Controls all the elements in the Main Menu
    /// </summary>
    public class MainMenuController : MonoBehaviour {
        #region Publics
        [Header("Main Menu Controller")]

        [Space(10)]

        [Header("Main Menu Screen Buttons")]
        public bool disableOnClick; //!< This switches the buttons to uninteractable when one is pressed
        public Button[] mainMenuButtons; //!< All the buttons on the main menu

        [Space(10)]

        [Header("Scenes to Load")]
        public SceneLoadingController.Scenes gameScene;
        public SceneLoadingController.Scenes optionScene;
        #endregion

        private void Start() {
            SetButtonState(true);
        }

        /// <summary>
        /// Sets the buttons in the main menu to the desired state when clicked
        /// </summary>
        /// <param name="state"></param>
        private void SetButtonState(bool state) {
            for (int i = 0; i < mainMenuButtons.Length; i++) {
                mainMenuButtons[i].interactable = state;
            }
        }

        #region Button Listeners
        public void StartGame() {
            SetButtonState(disableOnClick);
            EventManager.TriggerEvent(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, gameScene);
        }

        public void Options() {
            SetButtonState(disableOnClick);
            EventManager.TriggerEvent(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, optionScene);
        }

        public void ExitGame() {
            SetButtonState(disableOnClick);
            Application.Quit();
        }
        #endregion
    }
}