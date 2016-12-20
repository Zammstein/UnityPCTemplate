using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// EventManager
/// <summary>
/// Author: Sam Meyer
/// <summary>
/// EventManager class from the official Unity web page. Minor modifications where made to be able to 
/// send data with a command in the form of an object. This class' gameobject is marked as 
/// 'DontDestroyOnLoad'. Place it in the first scene of the application to make sure every script in 
/// every scene can use events to communicate.
/// </summary>

public class EventManager : MonoBehaviour {

    #region Initialization
    private Dictionary<string, GameEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) 
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                else 
                    eventManager.Init();
            }
            return eventManager;
        }
    }

    void Start() {
        EventManager callInit = instance;
    }

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, GameEvent>();
        }
    }
    #endregion

    /// <summary>
    /// Call this function to register a function to a certain event.
    /// </summary>
    /// <param name="eventName">String used for registering the event on. When this string is passed in 
    /// the TriggerEvent function, all functions that registered to that string will be called.</param>
    /// <param name="listener">The function called when the event is triggered. This function needs to
    /// have an object array as only argument. From this array you can retrieve all arguments passed.</param>
    public static void StartListening(string eventName, UnityAction<object[]> listener) {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Call this function to deregister a function from a certain event.
    /// </summary>
    /// <param name="eventName">String used for registering the event on. When this string is passed in 
    /// the TriggerEvent function, all functions that registered to that string will be called.</param>
    /// <param name="listener">The function called when the event is triggered. This function needs to
    /// have an object array as only argument. From this array you can retrieve all arguments passed.</param>
    public static void StopListening(string eventName, UnityAction<object[]> listener) {
        if (eventManager == null) return;
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// This function triggers all functions that have been registered to the passed eventName. The arguments
    /// given will also be passed to each of these functions.
    /// </summary>
    /// <param name="eventName">This string will be used to check wich function is registered by this string.</param>
    /// <param name="arguments">An ambiguous amount of arguments that can be passed together with the event.
    /// This can be omitted if no arguments should be passed.</param>
    public static void TriggerEvent(string eventName, params object[] arguments) {
        GameEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(arguments);
        }
    }
}
