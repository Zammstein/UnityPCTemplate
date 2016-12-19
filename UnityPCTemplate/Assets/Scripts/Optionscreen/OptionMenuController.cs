using UnityEngine;

/// <summary>
/// OptionMenuController
/// <summary>
/// Author: Thomas van Opstal
/// <summary>
/// Controls the switching of panels within the option menu.
/// </summary>
public class OptionMenuController : MonoBehaviour {
    public enum OptionPanels {
        GAME_OPTIONS = 0,
        CONTROL_OPTIONS = 1,
        AUDIO_OPTIONS = 2,
        GRAPHICS_OPTIONS = 3
    }

    #region Publics
    [Header("Panel To Open First")]
    public OptionPanels firstPanel;

    [Space(10)]

    [Header("Option Menu Panels")]
    public GameObject gameOptions;
    public GameObject controlOptions;
    public GameObject audioOptions;
    public GameObject graphicsOptions;
    #endregion

    #region Privates
    private GameObject[] optionPanels;
    private SaveGameManager saveGameManager;
    #endregion

    private void Start() {
        optionPanels = new GameObject[] { gameOptions, controlOptions, audioOptions, graphicsOptions };
        saveGameManager = SaveGameManager.instance;
        SetActivePanel(firstPanel);
    }

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
        LoadingscreenController.LoadScene(LoadingscreenController.Scenes.MAIN_MENU);
    }
    #endregion
}