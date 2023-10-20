using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TIme : MonoBehaviour
{
    [SerializeField] private int MMTime;
    [SerializeField] private int SSTime = 0;
    [SerializeField] private PlayerTriggerDialogue PTD;
    public GameObject textObject;
    private TextMeshProUGUI textUGUI;
    [SerializeField] private GameObject Canvas_GameOver;

    private void Start()
    {
        textObject = this.transform.GetChild(0).gameObject;
        textUGUI = textObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        Invoke("startTimer", 1f);
    }


    void startTimer()
    {
        if (MMTime > 0 || SSTime > 0)
        {
            SSTime--;
        }
        textUGUI.text = string.Format("{0:00}" + " : " + "{1:00}", MMTime, SSTime);
        if (MMTime == 0 && SSTime == 0)
        {
            PTD.DialRunner.StartDialogue("TimeOverEnding");
        }
        else
        {
            if (SSTime == 0)
            {
                MMTime--;
                SSTime = 59;
            }
        }

        Invoke("startTimer", 1f);
    }

    public void TimesetZero()
    {
        if (PTD.DialRunner.CurrentNodeName == "TimeOverEnding")
        {
            Canvas_GameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
    }
}
