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
    public GameObject SparkEffect;

    private void Start()
    {
        SparkEffect = GameObject.Find("Spark").gameObject;
        objectnames = this.gameObject.name.Split('_');
        CabinetNum = int.Parse(objectnames[1]);

        animator = GetComponent<Animator>();
        SparkEffect.SetActive(false);
    }

    private void Update()
    {
        if (!isUse)
        {
            SparkEffect.SetActive(true);
        }
    }
}
