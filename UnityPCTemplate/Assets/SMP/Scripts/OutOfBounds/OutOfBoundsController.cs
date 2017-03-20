using Core.EventSystem;
using UnityEngine;
using System.Collections.Generic;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsController
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Keeps track of the active out of bounds triggers. 
    /// Note that multiple triggers with different effects can be active at the same time.
    /// </summary>
    public class OutOfBoundsController : MonoBehaviour {

        /// <summary>
        /// The OutOfBoundsEffect scripts attached to the same gameObject.
        /// </summary>
        private Dictionary<OutOfBoundsEffect.EFFECT_TYPE, OutOfBoundsEffect> attachedEffects = new Dictionary<OutOfBoundsEffect.EFFECT_TYPE, OutOfBoundsEffect>();

        /// <summary>
        /// Dictionary of the amount of active triggers per effect type.
        /// </summary>
        private Dictionary<OutOfBoundsEffect.EFFECT_TYPE, int> activeEffects = new Dictionary<OutOfBoundsEffect.EFFECT_TYPE, int>();

        void Start() {
            CheckAttachedEffects();
            EventManager.StartListening(OutOfBoundsEventTypes.INTERNAL_PLAYER_ENTERED, OnPlayerEntered);
            EventManager.StartListening(OutOfBoundsEventTypes.INTERNAL_PLAYER_EXIT, OnPlayerExit);
        }

        void OnDestroy() {
            EventManager.StopListening(OutOfBoundsEventTypes.INTERNAL_PLAYER_ENTERED, OnPlayerEntered);
            EventManager.StopListening(OutOfBoundsEventTypes.INTERNAL_PLAYER_EXIT, OnPlayerExit);
        }

        /// <summary>
        /// Scan this gameObject for all attached OutOfBoundsEffect scripts.
        /// </summary>
        private void CheckAttachedEffects() {
            foreach (OutOfBoundsEffect effect in GetComponents<OutOfBoundsEffect>()) {
                attachedEffects.Add(effect.GetEffectType(), effect);
            }
        }

        /// <summary>
        /// Function called when the player left an out of bounds area. 
        /// If the player is no longer in an area of the same type of effect, the effect will be toggled off.
        /// </summary>
        /// <param name="arg0">The OutOfBoundsEffect.EFFECT_TYPE of the area</param>
        private void OnPlayerExit(object[] arg0) {
            OutOfBoundsEffect.EFFECT_TYPE type = (OutOfBoundsEffect.EFFECT_TYPE)arg0[0];
            int activeAmount = 0;
            if (activeEffects.TryGetValue(type, out activeAmount)) {
                activeAmount--;
                if (activeAmount <= 0) {
                    ToggleEffect(type, false);
                }
                activeEffects[type] = activeAmount;
            } else {
                Debug.LogError("[OutOfBoundsController]\t" + "Player exit bounds trigger called before enter.");
            }
        }

        /// <summary>
        /// Function called when the player enters an out of bounds area.
        /// If the effect wasn't active before, it will be toggled on.
        /// </summary>
        /// <param name="arg0">The OutOfBoundsEffect.EFFECT_TYPE of the area</param>
        private void OnPlayerEntered(object[] arg0) {
            OutOfBoundsEffect.EFFECT_TYPE type = (OutOfBoundsEffect.EFFECT_TYPE)arg0[0];
            int activeAmount = 0;
            if (activeEffects.TryGetValue(type, out activeAmount)) {
                if (activeAmount <= 0) {
                    activeAmount = 1;
                    ToggleEffect(type, true);
                } else {
                    activeAmount++;
                }
                activeEffects[type] = activeAmount;
            } else {
                activeEffects.Add(type, 1);
                ToggleEffect(type, true);
            }
        }

        /// <summary>
        /// Toggles an OutOfBoundsEffect.EFFECT_TYPE on / off. 
        /// </summary>
        /// <param name="type">The OutOfBoundsEffect.EFFECT_TYPE that will be toggled</param>
        /// <param name="enable">Should the effect be enabled / disabled</param>
        private void ToggleEffect(OutOfBoundsEffect.EFFECT_TYPE type, bool enable) {
            if (!attachedEffects.ContainsKey(type)) {
                Debug.LogError("[OutOfBoundsController]\t" + "Out of bounds trigger calls for " + type + " but no effect script has been attached to the controller.");
                return;
            }

            if (enable)
                attachedEffects[type].StartEffect();
            else
                attachedEffects[type].EndEffect();
        }
    }
}
