using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            source = GetComponent<AudioSource>();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }

    public void PlayClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayClipVariation(AudioClip clip, int variationCount) {
        // assumes variations are equally spaced
        int variationIndex = Random.Range(0, variationCount);
        StartCoroutine(PlayTime(clip, clip.length / (float)variationCount * variationIndex, clip.length / (float)variationCount));
    }

    private IEnumerator PlayTime(AudioClip clip, float from, float dt) {
        source.time = from;
        source.clip = clip;
        source.Play();
        yield return new WaitForSecondsRealtime(dt);
        source.Stop();
    }
}
