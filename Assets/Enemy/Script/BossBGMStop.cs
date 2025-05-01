using System;
using UnityEngine;

public class BossBGMStop : MonoBehaviour
{
    public AudioSource _AudioSource;
    public AudioClip BossBGM;
    
    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        ItemManager.Instance.Player.GetComponent<PlayerHealth>().bgmSource = _AudioSource;
    }
}
