using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RangerRPG.Inventory {
    public class ItemSlotUI : MonoBehaviour {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemCount;

        public ItemSlotUI Init(ItemData data) {
            iconImage.sprite = data.icon;
            itemName.text = data.displayName;
            return this;
        }
    }
}