using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    [HideInInspector] public InventoryManager inventory;

    public bool windowOpen;

    private CameraLook cam;

    public void Awake()
    {
        cam = GetComponentInChildren<CameraLook>();
        inventory = GetComponentInChildren<InventoryManager>();
    }

    public void Update()
    {
        cam.lockCursor = !windowOpen;
        cam.canMove = !windowOpen;

        windowOpen = inventory.opened;
    }
}
