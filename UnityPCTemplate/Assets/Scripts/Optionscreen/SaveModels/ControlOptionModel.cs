using System;
using TeamUtility.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlOptionModel : SaveModel {
    public const string ID = "ControlOptionModel";

    Dictionary<string, string> crouchKeys = new Dictionary<string, string>();
    Dictionary<string, string> jumpKeys = new Dictionary<string, string>();
    Dictionary<string, string> interactKeys = new Dictionary<string, string>();

    public ControlOptionModel() {
        this.version = 1;

        crouchKeys["KeyboardAndMouse"] = "C";
        crouchKeys["XBox_360_Windows"] = "Joystick1Button1";

        jumpKeys["KeyboardAndMouse"] = "Space";
        jumpKeys["XBox_360_Windows"] = "Joystick1Button3";

        interactKeys["KeyboardAndMouse"] = "E";
        interactKeys["XBox_360_Windows"] = "Joystick1Button0";
    }

    #region Getters
    public object GetSpecificCrouchKey(string inputType) {
        return this.crouchKeys[inputType];
    }

    public object GetAllCrouchKeys() {
        return this.crouchKeys;
    }

    public object GetSpecificJumpKey(string inputType) {
        return this.jumpKeys[inputType];
    }

    public object GetAllJumpKeys() {
        return this.jumpKeys;
    }

    public object GetSpecificInteractKey(string inputType) {
        return this.interactKeys[inputType];
    }

    public object GetAllInteractKeys() {
        return this.interactKeys;
    }
    #endregion
    #region Setters
    public void SetCrouchKey(string inputType, string newKey) {
        this.crouchKeys[inputType] = newKey;
    }

    public void SetJumpKey(string inputType, string newKey) {
        this.jumpKeys[inputType] = newKey;
    }

    public void SetInteractKey(string inputType, string newKey) {
        this.interactKeys[inputType] = newKey;
    }
    #endregion

    public void SetKeybindingsFromModel() {
        AxisConfiguration jumpKeyboard = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Jump");
        jumpKeyboard.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificJumpKey("KeyboardAndMouse") as string);
        AxisConfiguration jumpController = InputManager.GetAxisConfiguration("XBox_360_Windows", "Jump");
        jumpController.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificJumpKey("XBox_360_Windows") as string);

        AxisConfiguration crouchKeyboard = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Crouch");
        crouchKeyboard.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificCrouchKey("KeyboardAndMouse") as string);
        AxisConfiguration crouchController = InputManager.GetAxisConfiguration("XBox_360_Windows", "Crouch");
        crouchController.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificCrouchKey("XBox_360_Windows") as string);

        AxisConfiguration interactKeyboard = InputManager.GetAxisConfiguration("KeyboardAndMouse", "Interact");
        interactKeyboard.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificInteractKey("KeyboardAndMouse") as string);
        AxisConfiguration interactController = InputManager.GetAxisConfiguration("XBox_360_Windows", "Interact");
        interactController.positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), this.GetSpecificInteractKey("XBox_360_Windows") as string);
    }

    public void ResetAllKeybindings() {
        crouchKeys["KeyboardAndMouse"] = "C";
        crouchKeys["XBox_360_Windows"] = "Joystick1Button1";

        jumpKeys["KeyboardAndMouse"] = "Space";
        jumpKeys["XBox_360_Windows"] = "Joystick1Button3";

        interactKeys["KeyboardAndMouse"] = "E";
        interactKeys["XBox_360_Windows"] = "Joystick1Button0";

        SetKeybindingsFromModel();
    }

    public override void UpgradeModel(SaveModel oldVersion) {
        crouchKeys = ((ControlOptionModel)oldVersion).GetAllCrouchKeys() as Dictionary<string, string>;
        jumpKeys = ((ControlOptionModel)oldVersion).GetAllJumpKeys() as Dictionary<string, string>;
        interactKeys = ((ControlOptionModel)oldVersion).GetAllInteractKeys() as Dictionary<string, string>;
    }
}
