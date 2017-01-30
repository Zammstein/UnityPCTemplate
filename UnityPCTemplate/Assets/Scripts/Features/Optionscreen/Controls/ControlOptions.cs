using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TeamUtility.IO;
using Features.Optionscreen.SaveModels;
using Features.SaveLoad;

namespace Features.Optionscreen.Controls {

    /// <summary>
    /// ControlOptions
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Contains the logic to update the control model, this will allow the user to change his keybindings.
    /// </summary>
    public class ControlOptions : MonoBehaviour {
        #region Publics
        public Text jumpController;
        public Text jumpKeyboard;

        public Text interactController;
        public Text interactKeyboard;

        public Text crouchController;
        public Text crouchKeyboard;
        #endregion

        #region Privates
        private ControlOptionModel controlOptionModel;
        private EventSystem eventSystem;
        #endregion

        private void OnEnable() {
            controlOptionModel = SaveGameManager.instance.GetModel(ControlOptionModel.ID) as ControlOptionModel;
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            SetKeybindingValues();
        }

        #region Input new Keybinding
        public void StartNewInputRemap(string keyName) {
            eventSystem.sendNavigationEvents = false;
            if (!InputManager.IsScanning) {
                StartCoroutine(InputNewKeybinding(keyName));
            }
        }

        private IEnumerator InputNewKeybinding(string keyName) {
            yield return new WaitForSeconds(0.2f);

            eventSystem.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Input new Key";

            if (InputManager.GetInputConfiguration(PlayerID.One).name != "KeyboardAndMouse") {
                InputManager.StartJoystickButtonScan((key, arg) => {
                    AxisConfiguration axisConfig = InputManager.GetAxisConfiguration(InputManager.GetInputConfiguration(PlayerID.One).name, keyName);
                    axisConfig.positive = (key == KeyCode.Backspace) ? KeyCode.None : key;
                    SetKeybindingValues();
                    return true;
                }, null, 5.0f, null);

                yield return new WaitForSeconds(1f);
                eventSystem.sendNavigationEvents = true;

            } else {
                InputManager.StartKeyboardButtonScan((key, arg) => {
                    AxisConfiguration axisConfig = InputManager.GetAxisConfiguration(InputManager.GetInputConfiguration(PlayerID.One).name, keyName);
                    axisConfig.positive = (key == KeyCode.Backspace) ? KeyCode.None : key;
                    SetKeybindingValues();
                    return true;
                }, 5.0f, null);
            }
        }
        #endregion

        private void SetKeybindingValues() {
            controlOptionModel.SetJumpKey("Windows_Gamepad", InputManager.GetAxisConfiguration("Windows_Gamepad", "Jump").positive.ToString());
            controlOptionModel.SetJumpKey("KeyboardAndMouse", InputManager.GetAxisConfiguration("KeyboardAndMouse", "Jump").positive.ToString());
            jumpController.text = InputManager.GetAxisConfiguration("Windows_Gamepad", "Jump").positive.ToString();
            jumpKeyboard.text = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Jump").positive.ToString();

            controlOptionModel.SetInteractKey("Windows_Gamepad", InputManager.GetAxisConfiguration("Windows_Gamepad", "Interact").positive.ToString());
            controlOptionModel.SetInteractKey("KeyboardAndMouse", InputManager.GetAxisConfiguration("KeyboardAndMouse", "Interact").positive.ToString());
            interactController.text = InputManager.GetAxisConfiguration("Windows_Gamepad", "Interact").positive.ToString();
            interactKeyboard.text = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Interact").positive.ToString();

            controlOptionModel.SetCrouchKey("Windows_Gamepad", InputManager.GetAxisConfiguration("Windows_Gamepad", "Crouch").positive.ToString());
            controlOptionModel.SetCrouchKey("KeyboardAndMouse", InputManager.GetAxisConfiguration("KeyboardAndMouse", "Crouch").positive.ToString());
            crouchController.text = InputManager.GetAxisConfiguration("Windows_Gamepad", "Crouch").positive.ToString();
            crouchKeyboard.text = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Crouch").positive.ToString();
        }

        #region Button Listeners
        public void ResetToDefault() {
            controlOptionModel.ResetAllKeybindings();
            SetKeybindingValues();
        }
        #endregion
    }
}