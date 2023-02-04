using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    private float timeSinceLastPlay;
    [SerializeField] private float timeDelayForSoundCombo;
    private int audioClipIndex;
    // Start is called before the first frame update
    void Start()
    {
        audioClipIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void PlaySound()
    {
        Debug.Log(timeSinceLastPlay);
        Debug.Log(Time.time + timeDelayForSoundCombo);
        if (Time.time < timeSinceLastPlay + timeDelayForSoundCombo)
        {
            timeSinceLastPlay = Time.time;
            audioSource.clip = audioClips[audioClipIndex];
            audioSource.Play();
            audioClipIndex++;
            if (audioClipIndex == audioClips.Length) audioClipIndex = 0;
        } else
        {
            timeSinceLastPlay = Time.time;
            audioClipIndex = 0;
            audioSource.clip = audioClips[audioClipIndex];
            audioSource.Play();

        }
    }
}
