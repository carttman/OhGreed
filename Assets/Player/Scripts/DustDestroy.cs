using System;
using UnityEngine;

public class DustDestroy : MonoBehaviour
{
    private void Start()
    {
        DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject, 1f);
    }
}
