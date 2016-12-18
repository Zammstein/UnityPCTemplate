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
