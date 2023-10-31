using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PauseSystem : MonoBehaviour
{
    public float savedTime;
    public GameObject canvas;
    public bool paused = false;

    public AudioSource[] audioSources;
    public AudioSource music;

    private bool isMuted = true;
    public TextMeshProUGUI sfxState;
    public TextMeshProUGUI songState;
    private bool isMutedSong = true;


    public void ToggleMute()
    {
        isMuted = !isMuted;

        foreach (var source in audioSources)
        {
            source.enabled = isMuted;
        }
        sfxState.text = isMuted ? "ON SFX" : "OFF SFX";
    }

    public void ToggleSong()
    {
        isMutedSong = !isMutedSong;
        music.gameObject.SetActive(isMutedSong);
        songState.text = isMutedSong ? "ON MUSIC" : "OFF MUSIC";

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                savedTime = Time.timeScale;
                Time.timeScale = 0;
                canvas.SetActive(true);
            }
            else
            {
                Time.timeScale = savedTime;
                canvas.SetActive(false);
            }
        }
    }
}
