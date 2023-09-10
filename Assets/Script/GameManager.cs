using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public DialogueRunner ID;
    LineView lv;

    private void Start()
    {
        lv = ID.dialogueViews[0].transform.gameObject.GetComponent<LineView>();
    }

    private void Update()
    {

    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    ID.StartDialogue("start");
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

}
