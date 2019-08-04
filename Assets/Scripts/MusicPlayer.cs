using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip defaultClip;

    Track currentTrack;
    AudioSource audioSource;

    void Start()
    {
        //currentTrack = FindObjectOfType<Track>();

        //int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        //if (numMusicPlayers > 1)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        DontDestroyOnLoad(gameObject);
        PlayTrackSong();
    }

    public void PlayTrackSong() {
        try
        {
            currentTrack = FindObjectOfType<Track>();
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = currentTrack.song;
            audioSource.Play();
        }
        catch {
            PlayGivenSong(defaultClip);
        }
    }

    public void PlayGivenSong(AudioClip song) {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;
        audioSource.Play();
    }

    void Update()
    {
        
    }
}
