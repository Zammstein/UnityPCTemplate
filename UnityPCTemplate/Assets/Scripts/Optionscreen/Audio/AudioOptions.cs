using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour {
    #region Publics
    [Header("Sliders & Toggles")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundEffectVolumeSlider;
    public Toggle soundEnabledToggle;
    #endregion

    #region Privates
    private AudioOptionModel audioOptionModel;
    #endregion

    private void OnEnable() {
        audioOptionModel = SaveGameManager.instance.GetModel(AudioOptionModel.ID) as AudioOptionModel;
        SetValuesFromModel();
    }

    #region On value changed listeners
    public void OnMasterVolumeSliderValueChanged() {
        audioOptionModel.SetMasterVolume(masterVolumeSlider.value);
    }

    public void OnMusicVolumeSliderValueChanged() {
        audioOptionModel.SetMusicVolume(musicVolumeSlider.value);
    }

    public void OnSoundEffectVolumeSliderValueChanged() {
        audioOptionModel.SetSoundEffectVolume(soundEffectVolumeSlider.value);
    }

    public void OnSoundEnabledToggleValueChanged() {
        audioOptionModel.SetSoundEnabled(soundEnabledToggle.isOn);
    }
    #endregion

    private void SetValuesFromModel() {
        masterVolumeSlider.value = audioOptionModel.GetMasterVolume();
        musicVolumeSlider.value = audioOptionModel.GetMusicVolume();
        soundEffectVolumeSlider.value = audioOptionModel.GetSoundEffectVolume();
        soundEnabledToggle.isOn = audioOptionModel.GetSoundEnabled();
    }

    #region Button Listeners
    public void ResetToDefault() {
        audioOptionModel.ResetAudioSettings();
        SetValuesFromModel();
    }
    #endregion
}
