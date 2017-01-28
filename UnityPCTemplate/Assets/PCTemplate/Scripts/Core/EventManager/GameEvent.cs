using UnityEngine.Events;

namespace Core.EventSystem {

    /// <summary>
    /// GameEvent
    /// <summary>
    /// Author: Sam Meyer
    /// <summary>
    /// Simple class to represent arguments passed with commands.
    /// </summary>
    public class GameEvent : UnityEvent<object[]> {
    }
}
