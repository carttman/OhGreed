using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    
    public event Action<ItemUIBase> OnItemAdded;
    
    public event Action<int, SlotType> OnItemMovedItem;
    public event Action<int> OnItemMoveToEquipSlot;
    
    [SerializeField]
    private GameObject InventoryPanel;
    private bool InventoryIsOpened = false; 
    
    public ItemUIBase ItemUIBaseSword;
    public ItemUIBase ItemUIBaseBow;
    public ItemUIBase ItemUIBaseWand;
    
    public GameObject TempIcon;
    public int TempIconIndex;
    public SlotType TempSlotType;
    
    private void Awake()
    {
       Singleton();
    }
    
    void Start()
    {
        InventoryPanel.SetActive(false);
        TempIcon.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            OnItemAdded?.Invoke(ItemUIBaseSword);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            OnItemAdded?.Invoke(ItemUIBaseBow);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            OnItemAdded?.Invoke(ItemUIBaseWand);
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
    }
    
    public void MoveItemInventory(int targetIndex, SlotType slotType)
    {
        switch (slotType)
        {
            case SlotType.ITEMSLOT:
            {
                OnItemMovedItem?.Invoke(targetIndex, TempSlotType);
                break;
            }

            case SlotType.WEAPONSLOT:
            {
                OnItemMoveToEquipSlot?.Invoke(targetIndex);
                break;
            }
        }
    }
}
