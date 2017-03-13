using Core.EventSystem;
using UnityEngine;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsTrigger
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// The incoming trigger in this script will communicate an Out of Bounds message to the player of some sort.
    /// </summary>
    public class OutOfBoundsTrigger : MonoBehaviour {

        public OutOfBoundsEffect[] outOfBoundsEffects;
        public OutOfBoundsEffect defaultEffect;

        private OutOfBoundsEffect currentEffect;
        private int insideTriggersAmount;

        void Start() {
            EventManager.StartListening(OutOfBoundsEventTypes.GLOBAL_SET_OUT_OF_BOUNDS_EFFECT, OnEffectChanged);
            currentEffect = defaultEffect;
        }

        void OnDestroy() {
            EventManager.StopListening(OutOfBoundsEventTypes.GLOBAL_SET_OUT_OF_BOUNDS_EFFECT, OnEffectChanged);
        }

        private void OnEffectChanged(object[] arg0) {
            OutOfBoundsEffect.EFFECT_TYPE effectType = (OutOfBoundsEffect.EFFECT_TYPE)arg0[0];
            foreach (OutOfBoundsEffect effect in outOfBoundsEffects) {
                if (effect.effectType == effectType) {
                    if (currentEffect.EffectIsActive()) {
                        currentEffect.SetEffectActive(false);
                        currentEffect.EndEffect();

                        effect.SetEffectActive(true);
                        effect.StartEffect();
                    }
                    
                    currentEffect = effect;
                    break;
                }
            }
        }

        void OnTriggerEnter(Collider coll) {
            if (coll.tag == "MainCamera") {
                insideTriggersAmount++;
                if (insideTriggersAmount == 1) {
                    currentEffect.SetEffectActive(true);
                    currentEffect.StartEffect();
                }
            }
        }

        void OnTriggerExit(Collider coll) {
            if (coll.tag == "MainCamera") {
                insideTriggersAmount--;
                if (insideTriggersAmount <= 0) {
                    insideTriggersAmount = 0;
                    currentEffect.SetEffectActive(false);
                    currentEffect.EndEffect();
                }
            }
        }
    }
}
