using DG.Tweening;
using UnityEngine;

public enum DoorType
{
    Dual,
    Mono
}

public enum Way
{
    U,
    D,
    R,
    L
}

public enum DoorLockType
{
    None,
    ClassDoor,
    PathWayDoor
}

public class Door : MonoBehaviour
{
    [SerializeField] private DoorType type;
    [SerializeField] public DoorLockType lockType;
    [SerializeField] private Way wayType;
    [SerializeField] private GameObject doorL;
    [SerializeField] private GameObject doorR;
    [SerializeField] private Vector3 originRotationR;
    [SerializeField] private Vector3 originRotationL;
    [SerializeField] public bool isOpen = false;

    private void Start()
    {
        originRotationL = doorL.transform.rotation.eulerAngles;
        if (type == DoorType.Dual)
        {
            originRotationR = doorR.transform.rotation.eulerAngles;
        }
    }

    private bool IsPlayerFront(Collider other)
    {
        var playerPos = other.transform.position;
        var doorPos = transform.position;
        return (wayType == Way.U && playerPos.z > doorPos.z) || (wayType == Way.D && playerPos.z < doorPos.z) ||
               (wayType == Way.L && playerPos.x < doorPos.x) || (wayType == Way.R && playerPos.x > doorPos.x);
    }
    
    private void OpenDoorL(Collider other)
    {
        if (type == DoorType.Mono)
            doorL.transform.DORotate(originRotationL + new Vector3(0, 0, -90), 0.5f, RotateMode.Fast);
        else if (IsPlayerFront(other))
            doorL.transform.DORotate(originRotationL + new Vector3(0,0,90), 0.5f, RotateMode.Fast);
        else
            doorL.transform.DORotate(originRotationL + new Vector3(0,0,-90), 0.5f, RotateMode.Fast);
    }
    
    private void CloseDoorL()
    {
        doorL.transform.DORotate(originRotationL, 0.5f, RotateMode.Fast);
    }
    
    private void OpenDoorR(Collider other)
    {
        if (IsPlayerFront(other))
            doorR.transform.DORotate(originRotationR + new Vector3(0,0,-90), 0.5f, RotateMode.Fast);
        else
            doorR.transform.DORotate(originRotationR + new Vector3(0,0,90), 0.5f, RotateMode.Fast);
    }
    
    private void CloseDoorR()
    {
        doorR.transform.DORotate(originRotationR, 0.5f, RotateMode.Fast);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // if (Input.GetMouseButtonDown(0))
           // {
                if (!isOpen && InventoryObject.inventoryObject.PlayerHaveKey(lockType))
                        isOpen = true;
                if (isOpen)
                {
                    if (type == DoorType.Dual)
                        OpenDoorR(other);
                    OpenDoorL(other);
           //     }
            }
        }
        if (!other.CompareTag("Professor")) return;
        if (type == DoorType.Dual)
            OpenDoorR(other);
        OpenDoorL(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == DoorType.Dual)
                CloseDoorR();
            CloseDoorL();
        }
        if (!other.CompareTag("Professor")) return;
        if (type == DoorType.Dual)
            CloseDoorR();
        CloseDoorL();
    }
}
