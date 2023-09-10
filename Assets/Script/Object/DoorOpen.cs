using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    string DoorTag;

    private void Start()
    {

    }

    private void Update()
    {
        DoorTag = this.gameObject.tag;

        BoxRay(DoorTag);
    }
    
    public int num = 1;

    private void BoxRay(string _tags)
    {
        RaycastHit hit;
        if (_tags == "LeftDoor")
            num = 1;
        else if (_tags == "RightDoor")
            num = -1;

        Debug.DrawRay(transform.position + transform.forward * 1f * num + transform.right * -0.5f * num, transform.up * -1 * num * 2f, Color.cyan);
        if (Physics.BoxCast(transform.position + transform.forward * 1f * num + transform.right * -0.5f * num,
            transform.lossyScale / 2, transform.up * -1 * num, out hit, transform.rotation, 2f))
        {
            if (hit.collider.tag == "Player")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Correct");
                    StartCoroutine(OpenDoor(num));
                }
            }
        }
    }

    IEnumerator OpenDoor(int _num)
    {
        float _yAngle = 0f;

        while (transform.rotation.y == 90 * _num)
        {
            _yAngle += 0.01f * _num;
            transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 90 * _num, 0, 0), _yAngle);
        }
        yield return null;
    }
}
