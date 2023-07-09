using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public void PlayClip(AudioClip clip) {
        SoundManager.Instance.PlayClip(clip);
    }

    public void PlayMusic(AudioClip clip) {
        SoundManager.Instance.PlayMusic(clip);
    }

    public void StopMusic() {
        SoundManager.Instance.StopMusic();
    }

    public void PlayClipVariation(AudioClip clip, int variations) {
        SoundManager.Instance.PlayClipVariation(clip, variations);
    }
}
