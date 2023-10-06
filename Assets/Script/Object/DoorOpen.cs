using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openedRotation1;
    [SerializeField] private Vector3 openedRotation2;

    string DoorTag;
    public bool isOpen = false;
    Transform tr;

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
            transform.lossyScale / 2, transform.up * -1 * num, out hit, transform.rotation, 4f))
        {
            if (hit.collider.tag == "Player")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    tr = this.transform;
                    if (!isOpen)
                    {
                        if (num == 1) StartCoroutine(OpenDoor1(num));
                        else StartCoroutine(OpenDoor2(num));
                        isOpen = !isOpen;
                    }
                    else
                    {
                        if (num == 1) StartCoroutine(CloseDoor1(num));
                        else StartCoroutine(CloseDoor2(num));
                        isOpen = !isOpen;
                    }
                }
            }
        }
    }

    IEnumerator OpenDoor1(int _num)
    {
        //float _yAngle = 0f;

        //while (Mathf.Abs(transform.rotation.y) < 90)
        //{
        //    _yAngle += 0.1f * Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(-90 * _num, 0, 90)), _yAngle);
        //    yield return null;
        //}

        float t = 0f;
        float speed = 0.5f;

        Debug.Log("OpenDoor1");

        while (!Mathf.Approximately(t, 1f))
        {
            t = Mathf.Clamp(t + speed * Time.deltaTime, 0f, 1f);
            Debug.Log(t);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(closedRotation), Quaternion.Euler(openedRotation1), t);
            yield return null;
        }
    }

    IEnumerator OpenDoor2(int _num)
    {
        //float _yAngle = 0f;

        //while (Mathf.Abs(transform.rotation.y) < 90)
        //{
        //    _yAngle += 0.1f * Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(-90 * _num, 0, 90)), _yAngle);
        //    yield return null;
        //}

        float t = 0f;
        float speed = 0.5f;

        Debug.Log("OpenDoor2");

        while (!Mathf.Approximately(t, 1f))
        {
            t = Mathf.Clamp(t + speed * Time.deltaTime, 0f, 1f);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(closedRotation), Quaternion.Euler(openedRotation2), t);
            yield return null;
        }
    }
    IEnumerator CloseDoor1(int _num)
    {
        //float _yAngle = 1f;

        //while (Mathf.Abs(transform.rotation.y) < 90)
        //{
        //    _yAngle -= 0.1f * Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(-90 * _num, 0, 90)), _yAngle);
        //    yield return null;
        //}

        float t = 0f;
        float speed = 0.5f;

        Debug.Log("CloseDoor1");

        while (!Mathf.Approximately(t, 1f))
        {
            t = Mathf.Clamp(t + speed * Time.deltaTime, 0f, 1f);
            Debug.Log(t);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(openedRotation1), Quaternion.Euler(closedRotation), t);
            yield return null;
        }
    }
    IEnumerator CloseDoor2(int _num)
    {
        //float _yAngle = 1f;

        //while (Mathf.Abs(transform.rotation.y) < 90)
        //{
        //    _yAngle -= 0.1f * Time.deltaTime;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(-90 * _num, 0, 90)), _yAngle);
        //    yield return null;
        //}

        float t = 0f;
        float speed = 0.5f;

        Debug.Log("CloseDoor2");

        while (!Mathf.Approximately(t, 1f))
        {
            t = Mathf.Clamp(t + speed * Time.deltaTime, 0f, 1f);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(openedRotation2), Quaternion.Euler(closedRotation), t);
            yield return null;
        }
    }
}
