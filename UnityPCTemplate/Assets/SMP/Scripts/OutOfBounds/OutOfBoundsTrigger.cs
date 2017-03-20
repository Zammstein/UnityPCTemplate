using Core.EventSystem;
using UnityEngine;

namespace SMP.OutOfBounds {

    /// <summary>
    /// OutOfBoundsTrigger
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// The incoming trigger in this script will communicate an Out of Bounds effect.
    /// </summary>
    public class OutOfBoundsTrigger : MonoBehaviour {

        /// <summary>
        /// The effect that will be triggered.
        /// </summary>
        public OutOfBoundsEffect.EFFECT_TYPE effectType;


        void OnTriggerEnter(Collider coll) {
            if (coll.tag == "Player") {
                EventManager.TriggerEvent(OutOfBoundsEventTypes.INTERNAL_PLAYER_ENTERED, effectType);
            }
        }

        void OnTriggerExit(Collider coll) {
            if (coll.tag == "Player") {
                EventManager.TriggerEvent(OutOfBoundsEventTypes.INTERNAL_PLAYER_EXIT, effectType);
            }
        }
    }
}
