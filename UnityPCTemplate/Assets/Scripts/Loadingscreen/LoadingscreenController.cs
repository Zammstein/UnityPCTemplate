using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

/// <summary>
/// LoadingscreenController
/// <summary>
/// Author: Sam Meijer
/// <summary>
/// Script that handles async loading of scenes, this presents a loading screen to the player when a new scene is loaded.
/// </summary>
public class LoadingscreenController : MonoBehaviour {
    public enum Scenes {
        SPLASH_SCREEN = 0,
        LOADING_SCENE = 1,
        MAIN_MENU = 2,
        OPTIONS_MENU = 3,
        GAME_SCENE = 4
    }

    private static Scenes sceneToBeLoaded;
    private AsyncOperation async;

    public Text loadingPercentageTextField;

    public static void LoadScene(Scenes scene) {
        sceneToBeLoaded = scene;
        SceneManager.LoadScene((int)Scenes.LOADING_SCENE);
    }

    private void Start() {
        StartCoroutine(LoadGame());
    }

    private void Update() {
        if (async != null) {
            loadingPercentageTextField.text = (Math.Ceiling(async.progress * 100)).ToString() + "%";
            if (async.progress >= 0.90f) {
                async.allowSceneActivation = true;
            }
        }
    }

    private IEnumerator LoadGame() {
        async = SceneManager.LoadSceneAsync((int)sceneToBeLoaded);
        async.allowSceneActivation = false;
        yield return async;
    }
}
