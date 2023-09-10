using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void MainGameStart()
    {
        SceneManager.LoadScene(1);
    }
}
