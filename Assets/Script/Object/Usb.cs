using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usb : MonoBehaviour
{
    public GameObject auraEffectObject;


    private void OnEnable()
    {
        auraEffectObject = this.transform.GetChild(0).gameObject;
        // usb활성화 및 코루틴 시작
        StartCoroutine(twentySecWait());

    }
    IEnumerator twentySecWait()
    {
        yield return new WaitForSecondsRealtime(100f);
        auraEffectObject.SetActive(true);
    }
}