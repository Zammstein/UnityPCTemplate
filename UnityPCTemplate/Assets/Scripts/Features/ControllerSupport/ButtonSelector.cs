using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Features.ControllerSupport
{
    /// <summary>
    /// ButtonSelector.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Everytime a new scene is loaded the first button in the scene will be set as selected in the eventsystem.
    /// </summary>
    [DisallowMultipleComponent]
    public class ButtonSelector : MonoBehaviour
    {
        #region Public
        public static ButtonSelector instance = null;
        #endregion

        #region Private
        private EventSystem currentEventSystem; //EventSystem in the current Scene
        #endregion

        private void Awake() {
            #region Make sure only one instance exists
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            #endregion
        }

        private void Start() {
            //Get the first EventSystem on startup
            currentEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

            if (currentEventSystem != null)
                currentEventSystem.SetSelectedGameObject(GetFirstButtonInScene());
        }

        /// <summary>
        /// This function is called when unity has loaded a scene. Get the EventSystem in the new scene and set the first button in the hierarchy.
        /// </summary>
        /// <param name="level">What level is Loaded?</param>
        private void OnLevelWasLoaded(int level) {
            //Get the new EventSystem in the newly loaded Scene
            currentEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

            if (currentEventSystem != null)
                currentEventSystem.SetSelectedGameObject(GetFirstButtonInScene());
        }

        /// <summary>
        /// Look for the first active button in the hierarchy
        /// </summary>
        /// <returns>Return first active button in hierarchy if the scene has one</returns>
        private GameObject GetFirstButtonInScene() {
            Button firstButtonInScene = (Button)FindObjectOfType(typeof(Button));

            if (firstButtonInScene != null)
                return firstButtonInScene.gameObject;
            else
                return null;
        }
    }
}
