using System;
using UnityEngine;

public class BossBGMStop : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource bossAudioSource;

    public static BossBGMStop Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Stop()
    {
        bgmAudioSource?.Stop();
        bossAudioSource?.Stop();
    }
}
