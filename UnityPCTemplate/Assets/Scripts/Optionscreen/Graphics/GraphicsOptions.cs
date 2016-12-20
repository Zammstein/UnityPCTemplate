using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsOptions : MonoBehaviour {
    #region Publics
    [Header("Dropdowns & Toggles")]
    public Dropdown screenResolutionDropdown;
    public Dropdown qualityLevelDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown shadowQualityDropdown;
    public Dropdown antiAliasingDropdown;
    public Toggle fullscreenEnabledToggle;
    public Toggle vSyncEnabledToggle;
    public Toggle anisotropicFilteringEnabledToggle;
    public Toggle reduceInputLagEnabledToggle;
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
        SetValuesFromModel();
    }

    private void SetValuesFromModel() {
        SetResolutionDropdown();
        SetFullscreenToggle();
        SetVsyncToggle();
        SetQualityLevelDropdown();
        SetTextureQualityDropdown();
        SetShadowDropdown();
        SetAntiAliasingDropdown();
        SetAnisotropicToggle();
        SetReduceInputLagToggle();
    }

    #region Resolution
    private void SetResolutionDropdown() {
        availibleScreenResolutions = Screen.resolutions;
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
    #region Fullscreen
    private void SetFullscreenToggle() {
        fullscreenEnabledToggle.isOn = graphicsOptionModel.GetFullscreenMode();
    }

    public void ChangeFullscreenMode() {
        graphicsOptionModel.SetFullscreenMode(fullscreenEnabledToggle.isOn);
        Screen.fullScreen = fullscreenEnabledToggle.isOn;
    }
    #endregion
    #region V-Sync
    private void SetVsyncToggle() {
        vSyncEnabledToggle.isOn = graphicsOptionModel.GetVSync();
    }

    public void ChangeVsyncMode() {
        if (vSyncEnabledToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        graphicsOptionModel.SetVSync(vSyncEnabledToggle.isOn);
    }
    #endregion
    #region Quality Level
    private void SetQualityLevelDropdown() {
        qualityLevels = QualitySettings.names;
        qualityLevelDropdown.options.Clear();

        for (int i = 0; i < qualityLevels.Length; i++) {
            qualityLevelDropdown.options.Add(new Dropdown.OptionData() { text = qualityLevels[i] });
        }

        qualityLevelDropdown.value = graphicsOptionModel.GetQualityLevel();

        qualityLevelDropdown.onValueChanged.AddListener(delegate {
            QualitySettings.SetQualityLevel(qualityLevelDropdown.value, false);
            graphicsOptionModel.SetQualityLevel(qualityLevelDropdown.value);
        });

        qualityLevelDropdown.RefreshShownValue();
    }
    #endregion
    #region Texture Quality
    private void SetTextureQualityDropdown() {
        textureQualityLevels = new string[] { "High", "Medium", "Low" };
        textureQualityDropdown.options.Clear();

        for (int i = 0; i < textureQualityLevels.Length; i++) {
            textureQualityDropdown.options.Add(new Dropdown.OptionData() { text = textureQualityLevels[i] });
        }

        textureQualityDropdown.value = graphicsOptionModel.GetTextureResolution();

        textureQualityDropdown.onValueChanged.AddListener(delegate {
            QualitySettings.masterTextureLimit = textureQualityDropdown.value;
            graphicsOptionModel.SetTextureResolution(textureQualityDropdown.value);
        });

        textureQualityDropdown.RefreshShownValue();
    }
    #endregion
    #region Shadow Quality
    private void SetShadowDropdown() {
        shadowQualityLevels = new string[] { "High", "Low", "Off" };
        shadowQualityDropdown.options.Clear();

        for (int i = 0; i < shadowQualityLevels.Length; i++) {
            shadowQualityDropdown.options.Add(new Dropdown.OptionData() { text = shadowQualityLevels[i] });
        }

        shadowQualityDropdown.value = graphicsOptionModel.GetShadowQuality();

        shadowQualityDropdown.onValueChanged.AddListener(delegate {
            if (shadowQualityDropdown.value == 2) {
                QualitySettings.shadows = ShadowQuality.Disable;
            } else if (shadowQualityDropdown.value == 1) {
                QualitySettings.shadows = ShadowQuality.HardOnly;
            } else if (shadowQualityDropdown.value == 0) {
                QualitySettings.shadows = ShadowQuality.All;
            } else {
                Debug.Log("[SHADOWS] Something went wrong!");
            }

            graphicsOptionModel.SetShadowQuality(shadowQualityDropdown.value);
        });

        shadowQualityDropdown.RefreshShownValue();
    }
    #endregion
    #region Anti-Aliasing
    private void SetAntiAliasingDropdown() {
        antiAliasingLevels = new int[] { 0, 2, 4, 8 };
        antiAliasingDropdown.options.Clear();

        for (int i = 0; i < antiAliasingLevels.Length; i++) {
            antiAliasingDropdown.options.Add(new Dropdown.OptionData() { text = antiAliasingLevels[i].ToString() });
        }

        antiAliasingDropdown.value = graphicsOptionModel.GetAntiAliasing();

        antiAliasingDropdown.onValueChanged.AddListener(delegate {
            QualitySettings.antiAliasing = antiAliasingLevels[antiAliasingDropdown.value];
            graphicsOptionModel.SetAntiAliasing(antiAliasingDropdown.value);
        });

        antiAliasingDropdown.RefreshShownValue();
    }
    #endregion
    #region Anisotropic Filtering
    private void SetAnisotropicToggle() {
        anisotropicFilteringEnabledToggle.isOn = graphicsOptionModel.GetAnisotropicFiltering();
    }

    public void ChangeAnisotropicFiltering() {
        if (anisotropicFilteringEnabledToggle.isOn)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

        graphicsOptionModel.SetAnisotropicFiltering(anisotropicFilteringEnabledToggle.isOn);
    }
    #endregion
    #region Reduce Input Lag
    private void SetReduceInputLagToggle() {
        reduceInputLagEnabledToggle.isOn = graphicsOptionModel.GetReduceInputLag();
    }

    public void ChangeReduceInputLag() {
        if (reduceInputLagEnabledToggle.isOn)
            QualitySettings.maxQueuedFrames = 1;
        else
            QualitySettings.maxQueuedFrames = 2;

        graphicsOptionModel.SetReduceInputLag(reduceInputLagEnabledToggle.isOn);
    }
    #endregion

    #region Button Listeners
    public void ResetToDefault() {
        graphicsOptionModel.ResetGraphicsSettings();
        SetValuesFromModel();
    }
    #endregion
}
