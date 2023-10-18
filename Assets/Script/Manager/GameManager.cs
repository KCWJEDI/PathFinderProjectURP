using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Canvas_Pause;
    [SerializeField] private GameObject Canvas_Option;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Canvas_Pause.activeSelf)
            {
                Canvas_Pause.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Canvas_Pause.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }




}
