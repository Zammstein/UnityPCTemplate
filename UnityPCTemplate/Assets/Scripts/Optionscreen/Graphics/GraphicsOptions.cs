using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsOptions : MonoBehaviour {
    #region Publics
    [Header("Dropdowns & Toggles")]
    public Dropdown screenResolutionDropdown;
    //public Dropdown qualityLevelDropdown;
    //public Dropdown textureQualityDropdown;
    //public Dropdown shadowQualityDropdown;
    //public Dropdown antiAliasingDropdown;
    //public Toggle fullscreenEnabledToggle;
    //public Toggle vSyncEnabledToggle;
    //public Toggle anisotropicFilteringEnabledToggle;
    //public Toggle reduceInputLagEnabledToggle;
    #endregion

    #region Privates
    private Resolution[] availibleScreenResolutions;
    private string[] qualityLevels;
    private string[] textureQualityLevels;
    private string[] shadowQualityLevels;
    private int[] antiAliasingLevels;

    private GraphicsOptionModel graphicsOptionModel;
    #endregion

    private void OnEnable() {
        graphicsOptionModel = SaveGameManager.instance.GetModel(GraphicsOptionModel.ID) as GraphicsOptionModel;
        availibleScreenResolutions = Screen.resolutions;
        qualityLevels = QualitySettings.names;
        textureQualityLevels = new string[] { "High", "Medium", "Low" };
        shadowQualityLevels = new string[] { "High", "Low", "Off" };
        antiAliasingLevels = new int[] { 0, 2, 4, 8 };
        SetValuesFromModel();
    }

    private void SetValuesFromModel() {
        SetResolutionDropdown();
    }

    #region Resolution
    private void SetResolutionDropdown() {
        screenResolutionDropdown.options.Clear();

        for (int i = 0; i < availibleScreenResolutions.Length; i++) {
            screenResolutionDropdown.options.Add(new Dropdown.OptionData() { text = ResToString(availibleScreenResolutions[i]) });

            if (ResToString(availibleScreenResolutions[i]) == ResToString(Screen.currentResolution)) {
                screenResolutionDropdown.value = i;
            }
        }

        screenResolutionDropdown.onValueChanged.AddListener(delegate {
            Screen.SetResolution(availibleScreenResolutions[screenResolutionDropdown.value].width, availibleScreenResolutions[screenResolutionDropdown.value].height, false);
            Screen.fullScreen = graphicsOptionModel.GetFullscreenMode();
        });

        screenResolutionDropdown.RefreshShownValue();
    }

    private string ResToString(Resolution res) {
        return res.width + " x " + res.height;
    }
    #endregion
}
