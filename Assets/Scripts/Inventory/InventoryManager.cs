using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector] public bool opened = false;


    [Header("Settings")]
    public KeyCode inventoryKey = KeyCode.E;
    public int inventorySize = 24;

    [Header("Refs")]
    public GameObject slotTemplate;

    [Tooltip("What GameObject will hold all the inventory content (e.g. slots)")]
    public Transform contentHolder;

    [Tooltip("Includes all different types of slots (e.g. container)")]
    [SerializeField] private Slot[] allSlots;

    /// <summary> Only the inventory slots </summary>
    private Slot[] inventorySlots;

    public void Start()
    {
        GenerateSlots();
    }

    public void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
            // Invert the value of opened (false = true, true = false)
            opened = !opened;

        if (opened)
            transform.localPosition = new Vector3(0, 0, 0);
        else
            transform.localPosition = new Vector3(-10000, 0, 0);
    }

    private void GenerateSlots()
    {
        List<Slot> _inventorySlots = new List<Slot>();
        List<Slot> _allSlots = new List<Slot>();

        // Copy allSlots into _allSlots
        for (int i = 0; i < allSlots.Length; i++)
        {
            _allSlots.Add(allSlots[i]);
        }

        // Create slots and add to local lists
        for (int i = 0; i < inventorySize; i++)
        {
            Slot slot = Instantiate(slotTemplate.gameObject, contentHolder).GetComponent<Slot>();

            _inventorySlots.Add(slot);
            _allSlots.Add(slot);
        }

        inventorySlots = _inventorySlots.ToArray();
        allSlots = _allSlots.ToArray();
    }
}
