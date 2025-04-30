using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBeginVFX : MonoBehaviour
{
    [SerializeField]
    public List<Transform> VfxTransforms;
    public List<GameObject> VfxObjects;
    
    [SerializeField]
    private GameObject VFXObject;

    [SerializeField]     
    private AudioClip SpawnSFX;

    private AudioSource audioSource;
    private void Awake()
    {
        VfxTransforms = new List<Transform>();
        VfxObjects = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            VfxTransforms.Add(transform.GetChild(i));
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(StartSound());
        StartCoroutine(SpawnVFX());
    }

    IEnumerator StartSound()
    {
        for (int i = 0; i < 7; i++)
        {
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(SpawnSFX);
        }
    }
    IEnumerator SpawnVFX()
    {
        
        for (int i = 0; i < VfxTransforms.Count; i++)
        {
            var vfx = Instantiate(VFXObject, VfxTransforms[i].position, VfxTransforms[i].rotation);
            
            vfx.transform.SetParent(transform);
            VfxObjects.Add(vfx);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.3f);
        
        for (int i = 0; i < VfxObjects.Count; i++)
        {
            VfxObjects[i].GetComponent<Animator>().SetTrigger("Slash");
        }
    }
    
}
