using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string ItemName;
    [SerializeField] public string KeyName;
    public Sprite ItemImage;

    private void Start()
    {
        ItemName = this.gameObject.name;
    }
}
