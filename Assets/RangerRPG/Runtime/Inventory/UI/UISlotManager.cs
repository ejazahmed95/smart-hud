using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISlotManager : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private TextMeshProUGUI m_itemName;

    [SerializeField]
    private TextMeshProUGUI m_itemNumber;

    public void Set(ItemStack i_itemStack) {

        m_icon.sprite = i_itemStack.data.icon;
        m_itemName.text = i_itemStack.data.displayName;
    }

}
