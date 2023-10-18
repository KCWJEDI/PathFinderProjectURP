using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocked : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
