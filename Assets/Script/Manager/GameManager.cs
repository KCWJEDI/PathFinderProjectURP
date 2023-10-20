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
        if (Canvas_Pause.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Canvas_Pause.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OpenOption()
    {
        Canvas_Option.SetActive(true);
    }

    public void OptionBack()
    {
        Canvas_Option.SetActive(false);
    }

    public void PauseBack()
    {
        Canvas_Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
