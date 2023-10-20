using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    static public float BackgroundMusic = 1;
    static public float EffectMusic = 1;

    public Slider backgroundSlider;
    public Slider effectSlider;

    public AudioSource ProfessorAudio;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        backgroundSlider.value = BackgroundMusic;
        effectSlider.value = EffectMusic;

        ProfessorAudio = GameObject.Find("ProfessorParent").GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        BackgroundMusic = backgroundSlider.value;
        EffectMusic = effectSlider.value;

        if (ProfessorAudio != null)
        {
            ProfessorAudio.volume = EffectMusic;
        }
    }



}
