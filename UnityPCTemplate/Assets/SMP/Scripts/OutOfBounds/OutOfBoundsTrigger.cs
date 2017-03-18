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
