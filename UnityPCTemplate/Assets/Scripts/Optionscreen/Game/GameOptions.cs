using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour {
    #region Publics
    [Header("Sliders & Toggles")]
    public Slider mouseSensitivitySlider;
    public Slider controllerSensitivitySlider;
    public Toggle mouseYInverted;
    public Toggle controllerYInverted;
    public Toggle toggleCrouch;
    #endregion

    #region Privates
    private GameOptionModel gameOptionModel;
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
        gameOptionModel.SetMouseYInverted(mouseYInverted.isOn);
    }

    public void OnControllerYInvertedValueChanged() {
        gameOptionModel.SetControllerYInverted(controllerYInverted.isOn);
    }

    public void OnToggleCrouchValueChanged() {
        gameOptionModel.SetHoldToCrouch(toggleCrouch.isOn);
    }
    #endregion

    private void SetValuesFromModel() {
        mouseSensitivitySlider.value = gameOptionModel.GetMouseSensitivity();
        controllerSensitivitySlider.value = gameOptionModel.GetControllerSensitivity();
        mouseYInverted.isOn = gameOptionModel.GetMouseYInverted();
        controllerYInverted.isOn = gameOptionModel.GetControllerYInverted();
        toggleCrouch.isOn = gameOptionModel.GetHoldToCrouch();
    }

    #region Button Listeners
    public void ResetToDefault() {
        gameOptionModel.ResetGameSettings();
        SetValuesFromModel();
    }
    #endregion
}
