using System;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    private float startPos;
    public GameObject cam;
    public float parallaxEffect;

    // public Transform player;             
    // public Vector3 offset = Vector3.zero; 
    // public Camera _Camera;
    //
    void Start()
    {
        
        // _Camera = Camera.main;
        startPos = transform.position.x;

    }

    private void FixedUpdate()
    {

        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        // if (player = null) return;
        //
        // transform.position = new Vector3(_Camera.transform.position.x ,_Camera.transform.position.y, 0);
    }


}
