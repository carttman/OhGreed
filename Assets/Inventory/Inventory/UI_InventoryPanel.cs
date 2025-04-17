using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_InventoryPanel : MonoBehaviour
{
    private List<GameObject> BlankList;
    [SerializeField]
    public GameObject BlankIcon;
    
    public List<ItemUIBase> ItemList;
    
    private int InventorySize = 15;
    
    [SerializeField]
    private GridLayoutGroup BlankGridLayout;
    [SerializeField]
    private GridLayoutGroup ItemGridLayout;
    
    
    void Awake()
    {
        CreateInventory();
    }
    
    void Start()
    {
        ItemManager.Instance.OnItemAdded += AddItem;
    }

    void Update()
    {
        
    }

    private void CreateInventory()
    {
        BlankList = new List<GameObject>();
        ItemList = new List<ItemUIBase>();
        
        // Create Blank Icon
        for (int i = 0; i < InventorySize; i++)
        {
            BlankList.Add(Instantiate(BlankIcon, BlankGridLayout.transform));
        }
    }

    private void AddItem(ItemUIBase itemData)
    {
        if (InventorySize <= ItemList.Count) return;
        
        ItemList.Add(Instantiate(itemData, ItemGridLayout.transform));
    }
}
