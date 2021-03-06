﻿using Core.EventSystem;
using Core.SceneLoading;
using Features.SaveLoad;
using UnityEngine;

namespace Features.Optionscreen {

    /// <summary>
    /// OptionMenuController
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Controls the switching of panels within the option menu.
    /// </summary>
    public class OptionMenuController : MonoBehaviour {
        /// <summary>
        /// All panels that are in the options menu, just add another one if needed.
        /// </summary>
        public enum OptionPanels {
            GAME_OPTIONS = 0,
            CONTROL_OPTIONS = 1,
            AUDIO_OPTIONS = 2,
            GRAPHICS_OPTIONS = 3
        }

        #region Publics
        [Header("Panel To Open First")]
        public OptionPanels firstPanel; //What panel should be opened first?

        [Space(10)]

        [Header("Option Menu Panels")]
        public GameObject gameOptions;      //Game Options Panel
        public GameObject controlOptions;   //Control Options Panel
        public GameObject audioOptions;     //Audio Options Panel
        public GameObject graphicsOptions;  //Graphics Options Panel
        #endregion

        #region Privates
        private GameObject[] optionPanels;          //Option Panels as GameObjects -> Use same order as OptionPanels Enum
        private SaveGameManager saveGameManager;    //SaveGame Manager
        #endregion

        private void Start() {
            optionPanels = new GameObject[] { gameOptions, controlOptions, audioOptions, graphicsOptions };
            saveGameManager = SaveGameManager.instance;
            SetActivePanel(firstPanel);
        }

        /// <summary>
        /// Saves the model and sets the new panel active
        /// </summary>
        /// <param name="panel">The next panel to enable</param>
        private void SetActivePanel(OptionPanels panel) {
            saveGameManager.SaveData();

            for (int i = 0; i < optionPanels.Length; i++) {
                optionPanels[i].SetActive(false);
            }

            optionPanels[(int)panel].SetActive(true);
        }

        #region Button listeners
        public void GameOptions() {
            SetActivePanel(OptionPanels.GAME_OPTIONS);
        }

        public void ControlOptions() {
            SetActivePanel(OptionPanels.CONTROL_OPTIONS);
        }

        public void AudioOptions() {
            SetActivePanel(OptionPanels.AUDIO_OPTIONS);
        }

        public void GraphicsOptions() {
            SetActivePanel(OptionPanels.GRAPHICS_OPTIONS);
        }

        public void BackToMainMenu() {
            saveGameManager.SaveData();
            EventManager.TriggerEvent(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, SceneLoadingController.Scenes.MAIN_MENU);
        }
        #endregion
    }
}