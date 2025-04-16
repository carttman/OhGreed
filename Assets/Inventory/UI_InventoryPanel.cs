using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryPanel : MonoBehaviour
{
    private List<GameObject> BlankList;
    [SerializeField]
    public GameObject BlankIcon;
    
    public List<Item> ItemList;
    
    private int InventorySize = 15;
    
    [SerializeField]
    private GridLayoutGroup GridLayout;
    [SerializeField]
    private GridLayoutGroup ItemGridLayout;
    
    
    void Awake()
    {
        CreateInventory();
        InventoryManager.Instance.OnItemAdded += AddItem;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void CreateInventory()
    {
        BlankList = new List<GameObject>();
        ItemList = new List<Item>();
        
        // Create Blank Icon
        for (int i = 0; i < InventorySize; i++)
        {
            BlankList.Add(Instantiate(BlankIcon, GridLayout.transform));
        }
    }

    private void AddItem(Item item)
    {
        ItemList.Add(Instantiate(item, ItemGridLayout.transform));
    }
}
