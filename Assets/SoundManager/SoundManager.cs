using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource sourceSFX;
    public AudioSource sourceBGM;

    public static SoundManager instance;

    [Range(0,1)]
    public float volSFX;

    [Range(0, 1)]
    public float volBGM;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
        if (instance != this)
            Destroy(gameObject);
    }

    public void PlaySoundEffect(SoundEffect soundEffect)
    {
        sourceSFX.clip = soundEffect.sfx;
        sourceSFX.pitch = Random.Range(soundEffect.minPitch, soundEffect.maxPitch);
        sourceSFX.volume = soundEffect.volume * volSFX;
        sourceSFX.Play();
    }
    public void PlaySoundEffect(SoundEffect[] soundEffects)
    {
        SoundEffect randomEffect = soundEffects[Random.Range(0, soundEffects.Length)];

        sourceSFX.clip = randomEffect.sfx;
        sourceSFX.pitch = Random.Range(randomEffect.minPitch, randomEffect.maxPitch);
        sourceSFX.volume = randomEffect.volume * volSFX;
        sourceSFX.Play();
    }
    public void PlaySoundEffect(SoundEffect soundEffect, AudioSource source)
    {
        source.clip = soundEffect.sfx;
        source.pitch = Random.Range(soundEffect.minPitch, soundEffect.maxPitch);
        source.volume = soundEffect.volume * volSFX;
        source.Play();
    }
    public void PlaySoundEffect(SoundEffect[] soundEffects, AudioSource source)
    {
        SoundEffect randomEffect = soundEffects[Random.Range(0, soundEffects.Length)];

        source.clip = randomEffect.sfx;
        source.pitch = Random.Range(randomEffect.minPitch, randomEffect.maxPitch);
        source.volume = randomEffect.volume * volSFX;
        source.Play();
    }

    public void PlayMusic(AudioClip music)
    {
        sourceBGM.Stop();
        sourceBGM.clip = music;
        sourceBGM.volume = 1f / volSFX;
        sourceBGM.Play();
    }
}
