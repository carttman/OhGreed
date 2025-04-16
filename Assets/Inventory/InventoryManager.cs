using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    public event Action<Item> OnItemAdded;
    
    [SerializeField]
    private GameObject InventoryPanel;
    private bool InventoryIsOpened = false; 
    
    public Item ItemObject;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    
    void Start()
    {
        InventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && InventoryIsOpened)
        {
            InventoryPanel.SetActive(false);
            InventoryIsOpened = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !InventoryIsOpened)
        {
            InventoryPanel.SetActive(true);
            InventoryIsOpened = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnItemAdded?.Invoke(ItemObject);
        }
        
    }
    
}
