using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicSource;
    private float volume = .5f;
    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = volume;
    }
    public void IncreaseVolume()
    {
        volume += 0.1f;
        volume = Mathf.Clamp01(volume);
        musicSource.volume = volume;    
    }

    public void DecreaseVolume()
    {
        volume -= 0.1f;
        volume = Mathf.Clamp01(volume);
        musicSource.volume = volume;
    }
    public float GetVolume()
    {
        return volume;
    }
}
