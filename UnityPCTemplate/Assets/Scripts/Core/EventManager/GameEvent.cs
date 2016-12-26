using UnityEngine.Events;

namespace Core.EventManager {

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
