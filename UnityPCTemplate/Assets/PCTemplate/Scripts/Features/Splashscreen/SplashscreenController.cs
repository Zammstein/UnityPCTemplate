﻿using UnityEngine;
using System.Collections;
using Core.SceneLoading;
using Core.EventSystem;

namespace Features.Splashscreen {

    /// <summary>
    /// SplashscreenController
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Controller for the splashscreen. The time the screen active can be controlled within the editor.
    /// </summary>
    public class SplashscreenController : MonoBehaviour {
        #region Publics
        [Header("Splashscreen Controller")]

        [Space(10)]

        [Header("Timing for the Splashscreen")]
        public float timeScreenIsActive; //!< Time the splashscreen waits will it loads the next scene

        [Space(5)]

        [Header("Scene to load")]
        public SceneLoadingController.Scenes scenes;
        #endregion

        private void Start() {
            StartCoroutine(GoToNextScene(timeScreenIsActive));
        }

        private IEnumerator GoToNextScene(float time) {
            yield return new WaitForSeconds(time);
            EventManager.TriggerEvent(SceneLoadingEventTypes.GLOBAL_LOAD_SCENE, scenes);
        }
    }
}