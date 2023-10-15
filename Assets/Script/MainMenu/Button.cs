using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject canvas_Fade;
    public Animator animator;
    [SerializeField] private GameObject optionObject;

    public void GameStart()
    {
        canvas_Fade.SetActive(true);
        animator.SetBool("isStart", true);
    }

    public void Option()
    {
        optionObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BTN_Back()
    {
        optionObject.SetActive(false);
    }
}
