using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource ac;

    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if(ac == null)
            ac = GetComponent<AudioSource>();

        CancelInvoke();
        ac.clip = clip;
        ac.volume = soundEffectVolume;
        ac.Play();
        ac.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        Invoke("Disable", clip.length +2);
    }

    public void Disable()
    {
        ac.Stop();
        Destroy(this.gameObject);
    }

}
