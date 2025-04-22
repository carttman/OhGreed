using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    
    public event Action<ItemUIBase> OnItemAdded;
    
    public event Action<int, EquipType> OnItemMovedItem;
    public event Action<int> OnItemMoveToEquipSlot;
    
    [SerializeField]
    private GameObject InventoryPanel;
    private bool InventoryIsOpened = false; 
    
    public ItemUIBase ItemUIBaseSword;
    public ItemUIBase ItemUIBaseBow;
    public ItemUIBase ItemUIBaseWand;
    
    public GameObject draggingIcon;
    public int draggingIconIndex;

    public EquipType draggingIconSlotType;
    
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
        draggingIcon.SetActive(false);
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

    public void MoveItem(int index, EquipType equipType)
    {
        // 리스트 두개 넘기기
        
        switch (equipType)
        {
            case EquipType.ITEMSLOT:
            {
                OnItemMovedItem?.Invoke(index, draggingIconSlotType);
                break;
            }

            case EquipType.WEAPONSLOT:
            {
                OnItemMoveToEquipSlot?.Invoke(index);
                break;
            }
        }
    }
}
