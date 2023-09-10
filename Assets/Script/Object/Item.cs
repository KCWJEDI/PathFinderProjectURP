using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string ItemName;
    public Sprite ItemImage;

    private void Start()
    {
        ItemName = this.gameObject.name;
        ItemImage = this.gameObject.GetComponent<Image>().sprite;
    }
}
