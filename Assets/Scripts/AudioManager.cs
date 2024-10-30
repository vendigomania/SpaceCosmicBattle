using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource ClickSource;

    public AudioSource BlastSource;
    public AudioSource LaserSource;
    public AudioSource BoomSource;
    public AudioSource PulseSource;
    public AudioSource ThunderSource;

    public AudioSource ReloadSource;

    private static AudioManager Instance;

    public static bool SoundActive = true;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public static bool MusicActive
    {
        get => Instance.Music.enabled;
        set => Instance.Music.enabled = value;
    }

    public static void Click()
    {
        if(SoundActive) Instance.ClickSource.Play();
    }

    public static void Blast()
    {
        if (SoundActive) Instance.BlastSource.Play();
    }

    public static void Laser()
    {
        if (SoundActive) Instance.LaserSource.Play();
    }

    public static void Boom()
    {
        if (SoundActive) Instance.BoomSource.Play();
    }

    public static void Pulse()
    {
        if (SoundActive) Instance.PulseSource.Play();
    }

    public static void Thunder()
    {
        if (SoundActive) Instance.ThunderSource.Play();
    }

    public static void Reload()
    {
        if (SoundActive) Instance.ReloadSource.Play();
    }
}
