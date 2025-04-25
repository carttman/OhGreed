using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public GameObject InventoryCanvas;
    public GameObject MainCamera;
    public GameObject FollowCamera;
    
    private void Awake()
    {
        Singleton();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("TestScene");
        }
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(InventoryCanvas);
        DontDestroyOnLoad(MainCamera);
        DontDestroyOnLoad(FollowCamera);
    }
}
