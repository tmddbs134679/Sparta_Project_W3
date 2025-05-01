using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource SoundSourcePrefab;


    private void Awake()
    {
        instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    private void ChangeBackGroundMusic(AudioClip musicClip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.SoundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }
}
