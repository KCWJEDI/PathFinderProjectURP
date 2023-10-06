using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    string[] objectnames;
    public int CabinetNum;
    public GameObject CabinetBox;
    public Animator animator;
    public bool isUse = true;

    public BoxCollider boxcollider;

    private void Start()
    {
        objectnames = this.gameObject.name.Split('_');
        CabinetNum = int.Parse(objectnames[1]);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
    }
}
