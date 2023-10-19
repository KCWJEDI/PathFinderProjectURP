using Unity.VisualScripting;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public static InventoryObject inventoryObject = null;
    private float scrollWheel;
    private int scrollInt = 0;
    public Slot SlotObject;

    [SerializeField]
    private Transform slotParent;

    public Slot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        SlotObject = slots[0].GetComponent<Slot>();
    }

    private void Start()
    {
        if (inventoryObject != null)
            return;
        
        inventoryObject = this;
        slots[0].active = true;
    }
    public bool PlayerHaveKey(DoorLockType lockType)
    {
        switch (lockType)
        {
            case DoorLockType.ClassDoor:
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    Debug.Log(slots[i].KeyType);
                    if (slots[i].KeyType == "ClassKey")
                    {
                        // 열쇠 제거하기
                        slots[i].Reset();
                        return true;
                    }
                }
                return false;
            }
            case DoorLockType.PathWayDoor:
            {
                foreach (var t in slots)
                {
                    if (t.KeyType == "PathKey")
                    {
                        //열쇠 제거하기
                        t.Reset();
                        return true;
                    }
                }

                return false;
            }
            case DoorLockType.None:
            default:
                return true;
        }
    }



    private void Update()
    {
        ScrollWheelInventory();
    }

    private void ScrollWheelInventory()
    {
        scrollWheel = Mathf.Clamp(Input.GetAxisRaw("Mouse ScrollWheel") * 10, -1, 1);

        if (scrollWheel != 0)
        {
            SlotObject.Outline.enabled = false;
            SlotObject.SlotItem.GetComponent<UnityEngine.UI.Image>().color = Color.white;

            scrollInt += (int)scrollWheel;
            if (scrollInt >= slots.Length - 1)
            {
                scrollInt = slots.Length - 1;
            }
            if (scrollInt <= 0)
            {
                scrollInt = 0;
            }
        }
        SlotObject = slots[scrollInt];
        SlotObject.Outline.enabled = true;
        SlotObject.SlotItem.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.7f, 0.7f, 1);
    }


}
