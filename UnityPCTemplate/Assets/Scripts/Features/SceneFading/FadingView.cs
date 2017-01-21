﻿using UnityEngine;
using System.Collections;
using Core.EventSystem;
using Core.SceneLoading;
/// <summary>
/// FadingView
/// <summary>
/// Author: Sam Meyer
/// <summary>
/// The FadingView must be placed in every scene as a black UI layer covering the entire camera view.
/// The LoadingController will use this to nicely fade between scenes.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class FadingView : MonoBehaviour {

    [Header("Attach this prefab to the canvas of a scene to enable the scene fading feature. Make sure this is the last child element in the list.")]

    public float fadeSpeed = 2f;

    private CanvasGroup overlayCanvasGroup;
     
    void Start() {
        overlayCanvasGroup = GetComponent<CanvasGroup>();

        overlayCanvasGroup.alpha = 1;
        overlayCanvasGroup.blocksRaycasts = false;

        EventManager.StartListening(SceneLoadingEventTypes.FADE_IN, FadeIn);
        EventManager.StartListening(SceneLoadingEventTypes.FADE_OUT, FadeOut);

        EventManager.TriggerEvent(SceneLoadingEventTypes.FADE_IN, SceneLoadingEventTypes.FADE_IN_COMPLETE);
    }

    void OnDestroy() {
        EventManager.StopListening(SceneLoadingEventTypes.FADE_IN, FadeIn);
        EventManager.StopListening(SceneLoadingEventTypes.FADE_OUT, FadeOut);
    }

    /// 
    /// Starts to fade in
    /// 
    private void FadeIn(object[] callbackEvent) {
        StopAllCoroutines();
        StartCoroutine(FadeInOverlay((string)callbackEvent[0]));
    }

    /// 
    /// Starts to fade out
    /// 
    private void FadeOut(object[] callbackEvent) {
        StopAllCoroutines();
        StartCoroutine(FadeOutOverlay((string)callbackEvent[0]));
    }

    /// 
    /// Fades in
    /// 
    IEnumerator FadeInOverlay(string callbackEvent) {
        overlayCanvasGroup.alpha = 1;
        overlayCanvasGroup.blocksRaycasts = false;
        while (overlayCanvasGroup.alpha > 0) {
            overlayCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        if (overlayCanvasGroup.alpha <= 0) {
            EventManager.TriggerEvent(callbackEvent);
        }
    }

    /// 
    /// Fades out
    /// 
    IEnumerator FadeOutOverlay(string callbackEvent) {
        overlayCanvasGroup.alpha = 0;
        overlayCanvasGroup.blocksRaycasts = true;
        while (overlayCanvasGroup.alpha < 1) {
            overlayCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        if (overlayCanvasGroup.alpha >= 1) {
            EventManager.TriggerEvent(callbackEvent);
        }
    }
}
