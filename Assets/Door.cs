using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DoorType
{
    Ah,
    Av,
    Bh,
    Bv
}

public class Door : MonoBehaviour
{
    [SerializeField] private DoorType type;
    [SerializeField] private GameObject doorL;
    [SerializeField] private GameObject doorR;

    private void DoorTypeAh(Collider other, bool op = false)
    {
        if (op)
        {
            doorL.transform.DORotate(new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
            doorR.transform.DORotate(new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
        }
        else
        {
            if (other.transform.position.z < this.transform.position.z)
            {
                doorL.transform.DORotate(new Vector3(-90, 0, -90), 0.7f, RotateMode.Fast);
                doorR.transform.DORotate(new Vector3(-90, 0, 90), 0.7f, RotateMode.Fast);
            }
            else
            {
                doorL.transform.DORotate(new Vector3(-90, 0, 90), 0.7f, RotateMode.Fast);
                doorR.transform.DORotate(new Vector3(-90, 0, -90), 0.7f, RotateMode.Fast);
            }
        }
    }
    
    private void DoorTypeAv(Collider other, bool op = false)
    {
        if (op)
        {
            doorL.transform.DORotate(new Vector3(-90, 0, 90), 0.7f, RotateMode.Fast);
            doorR.transform.DORotate(new Vector3(-90, 0, 90), 0.7f, RotateMode.Fast);
        }
        else
        {
            if (other.transform.position.x < this.transform.position.x)
            {
                doorL.transform.DORotate(new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
                doorR.transform.DORotate(new Vector3(-90, 0, 180), 0.7f, RotateMode.Fast);
            }
            else
            {
                doorL.transform.DORotate(new Vector3(-90, 0, 180), 0.7f, RotateMode.Fast);
                doorR.transform.DORotate(new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
            }
        }
    }

    private void DoorTypeBh(Collider other, bool op = false)
    {
        doorL.transform.DORotate(op ? new Vector3(-90, 0, -90) : new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
    }

    private void DoorTypeBv(Collider other, bool op = false)
    {
        doorL.transform.DORotate(op ? new Vector3(-90, 0, 90) : new Vector3(-90, 0, 0), 0.7f, RotateMode.Fast);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case DoorType.Ah:
                    DoorTypeAh(other);
                    break;
                case DoorType.Av:
                    DoorTypeAv(other);
                    break;
                case DoorType.Bh:
                    DoorTypeBh(other);
                    break;
                case DoorType.Bv:
                    DoorTypeBv(other);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (other.CompareTag("Professor"))
        {
            switch (type)
            {
                case DoorType.Ah:
                    DoorTypeAh(other);
                    break;
                case DoorType.Av:
                    DoorTypeAv(other);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case DoorType.Ah:
                    DoorTypeAh(other, true);
                    break;
                case DoorType.Av:
                    DoorTypeAv(other, true);
                    break;
                case DoorType.Bh:
                    DoorTypeBh(other, true);
                    break;
                case DoorType.Bv:
                    DoorTypeBv(other, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        if (other.CompareTag("Professor"))
        {
            switch (type)
            {
                case DoorType.Ah:
                    DoorTypeAh(other, true);
                    break;
                case DoorType.Av:
                    DoorTypeAv(other, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
