using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector] public ItemSO data;

    /// <summary> How much of the item can be in this slot </summary>
    [HideInInspector] public int stackSize;

    [Space]
    public Image icon;
    public TextMeshProUGUI stackText;

    public bool IsEmpty { get; set; }

    public void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (data == null)
        {
            IsEmpty = true;

            icon.gameObject.SetActive(false);
            stackText.gameObject.SetActive(false);
        }
        else
        {
            IsEmpty = false;

            icon.gameObject.SetActive(true);
            stackText.gameObject.SetActive(true);
        }
    }

    public void AddItemToSlot(ItemSO _data, int _stackSize)
    {
        data = _data;
        stackSize = _stackSize;
    }
}
