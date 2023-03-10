using System.Collections.Generic;
using RangerRPG.Core;
using UnityEngine;

namespace RangerRPG.EventSystem {
    [CreateAssetMenu(fileName = "GameEvent", menuName = "EAUnity/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject {
#if UNITY_EDITOR
        [Multiline] public string developerDescription = "";
#endif
        
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

        public void Raise(IGameEventData data = null) {
            Log.Info($"Event={name}", "EventRaised");
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(data);
        }

        public void RegisterListener(GameEventListener listener) {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener) {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}