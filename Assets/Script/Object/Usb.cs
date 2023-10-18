using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usb : MonoBehaviour
{
    public GameObject auraEffectObject;

    private void Start()
    {
        auraEffectObject = this.transform.GetChild(0).gameObject;
        StartCoroutine(twentySecWait());
    }

    IEnumerator twentySecWait()
    {
        yield return new WaitForSecondsRealtime(20f);
        auraEffectObject.SetActive(true);
    }
}