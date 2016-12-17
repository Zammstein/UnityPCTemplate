using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

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
    [Space(10)]
    [Header("Buttons (same order as panels)")]
    public GameObject gameButton;
    public GameObject controlButton;
    public GameObject audioButton;
    public GameObject graphicsButton;
    #endregion

    #region Privates
    private GameObject[] optionPanels;
    private GameObject[] optionButtons;
    private EventSystem eventSystem;
    #endregion

    private void Start() {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        optionPanels = new GameObject[] { gameOptions, controlOptions, audioOptions, graphicsOptions };
        optionButtons = new GameObject[] { gameButton, controlButton, audioButton, graphicsButton };
        SetActivePanel(firstPanel);
    }

    private void SetActivePanel(OptionPanels panel) {
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
    #endregion
}