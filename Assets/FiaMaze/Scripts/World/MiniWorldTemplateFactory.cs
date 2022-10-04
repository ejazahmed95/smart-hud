using UnityEngine;

namespace FiaMaze.World {
    
    [CreateAssetMenu(fileName = "MiniWorldTemplateFactory", menuName = "Game/WorldTemplateFactory")]
    public class MiniWorldTemplateFactory: ScriptableObject {
        [SerializeField] private MiniWorld defaultTemplate;
        
        public MiniWorld GetTemplateForWorldCell(WorldCellInfo cell) {
            return defaultTemplate;
        }
    }
}