using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    // 오브젝트 정보 저장 Bool값
    public bool active;
    // Slot의 Outline 컴포넌트
    public Outline Outline;
    // Item에서 가져온 오브젝트 이름
    public string ObjectName;

    //[HideInInspector]
    // 클릭한 오브젝트의 아이템 스크립트
    public Item item;
    //[HideInInspector]
    // 슬롯 내부의 아이템 UI
    public GameObject SlotItem;
    //[HideInInspector]
    // 슬롯 내부의 아이템 UI의 이미지
    public Sprite SlotItemImage;

    [SerializeField]
    private Sprite DefaultSlotItemImage;

    private void Start()
    {
        SlotItem = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (item != null)
        {
            ObjectName = item.name;
            SlotItemImage = item.ItemImage;
            SlotItem.GetComponent<Image>().sprite = SlotItemImage;
        }

    }

    public void Reset()
    {
        if (ObjectName != "PhoneItem")
        {
            active = false;
            ObjectName = null;
            item = null;
            SlotItemImage = null;
            SlotItem.GetComponent<Image>().sprite = DefaultSlotItemImage;
        }
    }

    
}


    