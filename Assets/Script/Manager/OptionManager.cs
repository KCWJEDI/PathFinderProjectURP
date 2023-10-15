using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    static public float BackgroundMusic;
    static public float EffectMusic;

    public Slider backgroundSlider;
    public Slider effectSlider;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        backgroundSlider.value = BackgroundMusic;
        effectSlider.value = EffectMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        BackgroundMusic = backgroundSlider.value;
        EffectMusic = effectSlider.value;
    }


}
