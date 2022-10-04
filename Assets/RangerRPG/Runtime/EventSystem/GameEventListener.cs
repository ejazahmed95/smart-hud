using RangerRPG.Utility;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.Events;

namespace RangerRPG.EventSystem {
    public class GameEventListener : MonoBehaviour {
        [Tooltip("Event to register with.")] 
        public GameEvent gameEvent;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<IGameEventData> response;

        private void OnEnable() {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable() {
            gameEvent.UnregisterListener(this);
        }
        
        public void OnEventRaised(IGameEventData data = null) {
            Log.Debug($"GameObject {gameObject.name.Italic()} handling event {gameEvent.name.Italic()}", "EventListener");
            response.Invoke(data);
        }
    }
}