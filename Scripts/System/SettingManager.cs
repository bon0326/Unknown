using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingManager : Singleton<SoundManager>
{
    public Slider Evolume;
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Evolume.value = gameManager.soundManager.GetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        EvolumeSlider();

    }
    public void EvolumeSlider()
    {
        gameManager.soundManager.SetVolume(Evolume.value);
    }
}
