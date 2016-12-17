using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SplashscreenController
/// <summary>
/// Author: Thomas van Opstal
/// <summary>
/// Controller for the splashscreen. The time the screen active can be controlled within the editor.
/// </summary>
public class SplashscreenController : MonoBehaviour {
    [Header("Splashscreen Controller")]
    [Space(10)]
    [Header("Timing for the Splashscreen")]
    public float timeScreenIsActive; //!< Time the splashscreen waits will it loads the next scene
    [Space(5)]
    [Header("Scene to load")]
    public LoadingscreenController.Scenes scenes;

    private void Start() {
        StartCoroutine(GoToNextScene(timeScreenIsActive));
    }

    private IEnumerator GoToNextScene(float time) {
        yield return new WaitForSeconds(time);
        LoadingscreenController.LoadScene(scenes);
    }
}
