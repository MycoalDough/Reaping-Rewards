using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongSystem : MonoBehaviour
{
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public Animator anim;
    public TextMeshProUGUI song;
    private AudioSource audioSource;
    private bool isPlayingClip1 = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudioClip();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            isPlayingClip1 = !isPlayingClip1;
            PlayAudioClip();
        }
    }

    private void PlayAudioClip()
    {
        anim.StopPlayback();
        anim.Play("NoneState", -1, 0f); // The string "NoneState" is just a placeholder
        anim.Play("Songs", 0, 0f); 
        if (isPlayingClip1)
        {
            audioSource.clip = audioClip1;
            audioSource.Play();
            song.text = "Back Home";
        }
        else
        {
            audioSource.clip = audioClip2;
            audioSource.Play();
            song.text = "Campfire";

        }
    }
}
