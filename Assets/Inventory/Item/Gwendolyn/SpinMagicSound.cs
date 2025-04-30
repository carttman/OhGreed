using UnityEngine;

public class SpinMagicSound : MonoBehaviour
{
    
    public void PlaySound(AudioClip clip)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }
}
