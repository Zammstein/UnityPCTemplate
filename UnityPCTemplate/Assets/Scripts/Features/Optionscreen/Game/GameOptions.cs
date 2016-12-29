using Features.Optionscreen.SaveModels;
using Features.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Optionscreen.Game {

    /// <summary>
    /// GameOptions
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Contains the logic to update the game model, this will allow the user to change several game options.
    /// </summary>
    public class GameOptions : MonoBehaviour {
        #region Publics
        [Header("Sliders & Toggles")]
        public Slider mouseSensitivitySlider;       //Mouse Sensitivity Slider
        public Slider controllerSensitivitySlider;  //Controller Sensitivity Slider
        public Toggle mouseYInvertedToggle;         //Mouse Y Inverted Toggle
        public Toggle controllerYInvertedToggle;    //Controller Y Inverted Toggle
        public Toggle toggleCrouchToggle;           //Toggle Crouch Toggle
        #endregion

        #region Privates
        private GameOptionModel gameOptionModel;    //Game Option Model
        #endregion

        private void OnEnable() {
            gameOptionModel = SaveGameManager.instance.GetModel(GameOptionModel.ID) as GameOptionModel;
            SetValuesFromModel();
        }

        #region On value changed listeners
        public void OnMouseSensitivitySliderValueChanged() {
            gameOptionModel.SetMouseSensitivity(mouseSensitivitySlider.value);
        }

        public void OnControllerSensitivitySliderValueChanged() {
            gameOptionModel.SetControllerSensitivity(controllerSensitivitySlider.value);
        }

        public void OnMouseYInvertedValueChanged() {
            gameOptionModel.SetMouseYInverted(mouseYInvertedToggle.isOn);
        }

        public void OnControllerYInvertedValueChanged() {
            gameOptionModel.SetControllerYInverted(controllerYInvertedToggle.isOn);
        }

        public void OnToggleCrouchValueChanged() {
            gameOptionModel.SetHoldToCrouch(toggleCrouchToggle.isOn);
        }
        #endregion

        private void SetValuesFromModel() {
            mouseSensitivitySlider.value = gameOptionModel.GetMouseSensitivity();
            controllerSensitivitySlider.value = gameOptionModel.GetControllerSensitivity();
            mouseYInvertedToggle.isOn = gameOptionModel.GetMouseYInverted();
            controllerYInvertedToggle.isOn = gameOptionModel.GetControllerYInverted();
            toggleCrouchToggle.isOn = gameOptionModel.GetHoldToCrouch();
        }

        #region Button Listeners
        public void ResetToDefault() {
            gameOptionModel.ResetGameSettings();
            SetValuesFromModel();
        }
        #endregion
    }
}
