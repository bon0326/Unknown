using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] bgms;
    public AudioSource audioSource;
    private float bgmVolume=1;
    private float effectVolume = 1;
    void Start()
    {
        audioSource.clip = bgms[0];
        audioSource.volume = bgmVolume;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void SetVolume(float volume)
    {
        this.bgmVolume=volume;
        audioSource.volume = bgmVolume;
    }
    public float GetVolume()
    {
        return bgmVolume;
    }
    public void ChangeMusic(int index)
    {
        audioSource.Stop();
        audioSource.clip = bgms[index];
        audioSource.Play();
    }
}
