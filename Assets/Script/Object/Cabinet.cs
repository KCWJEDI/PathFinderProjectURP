using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    string[] objectnames;
    public int CabinetNum;

    public BoxCollider boxcollider;
    Animator animator;
    public bool isOpen = false;

    private void Start()
    {
        objectnames = this.gameObject.name.Split('_');
        CabinetNum = int.Parse(objectnames[1]);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("isOpen", isOpen);
    }
}
