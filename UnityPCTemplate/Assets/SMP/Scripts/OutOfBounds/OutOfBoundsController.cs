using Core.EventSystem;
using UnityEngine;
using System.Collections.Generic;

namespace SMP.OutOfBounds {

    public class OutOfBoundsController : MonoBehaviour {

        private Dictionary<OutOfBoundsEffect.EFFECT_TYPE, OutOfBoundsEffect> attachedEffects = new Dictionary<OutOfBoundsEffect.EFFECT_TYPE, OutOfBoundsEffect>();
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

        private void CheckAttachedEffects() {
            foreach (OutOfBoundsEffect effect in GetComponents<OutOfBoundsEffect>()) {
                attachedEffects.Add(effect.GetEffectType(), effect);
            }
        }

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
