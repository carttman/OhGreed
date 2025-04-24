using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    
    public event Action<ItemUIBase, int> OnItemAdded;
    public event Action<int, SlotType> OnMoveToItemSlot;
    public event Action<int> OnMoveToEquipSlot;
    
    [SerializeField]
    private GameObject InventoryPanel;
    private bool InventoryIsOpened = false; 
    
    public ItemUIBase ItemUIBaseSword;
    public ItemUIBase ItemUIBaseBow;
    public ItemUIBase ItemUIBaseWand;
    
    public GameObject TempIcon;
    
    [HideInInspector]
    public int TempIconIndex;
    [HideInInspector]
    public SlotType TempSlotType;

    private GameObject WeaponObject;
    public GameObject Player;

    public IWeaponAttackable WeaponAttackable;

    public GameObject SkillSlot;
    public Sprite BlankIcon;
    private void Awake()
    {
       Singleton();
    }
    
    void Start()
    {
        InventoryPanel.SetActive(false);
        TempIcon.SetActive(false);
        
        OnItemAdded?.Invoke(ItemUIBaseSword, 0);
        OnItemAdded?.Invoke(ItemUIBaseBow, 1);
        OnItemAdded?.Invoke(ItemUIBaseWand, 2);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha5))
        // {
        //     OnItemAdded?.Invoke(ItemUIBaseSword);
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha6))
        // {
        //     OnItemAdded?.Invoke(ItemUIBaseBow);
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha7))
        // {
        //     OnItemAdded?.Invoke(ItemUIBaseWand);
        // }
        
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
                OnMoveToItemSlot?.Invoke(targetIndex, TempSlotType);
                break;
            }

            case SlotType.WEAPONSLOT:
            {
                OnMoveToEquipSlot?.Invoke(targetIndex);
                break;
            }
        }
    }

    public void SpawnItemObject(GameObject GO)
    {
        WeaponObject = Instantiate(GO, Player.transform);
        WeaponObject.transform.SetParent(Player.GetComponent<PlayerWeaponSocket>().WeaponSocket);
        WeaponObject.transform.localPosition = Vector3.zero;
        
        WeaponAttackable = WeaponObject.GetComponent<IWeaponAttackable>();
        
    }

    public void DestroyItemObject()
    {
        if (WeaponObject)
        {
            Destroy(WeaponObject);
        }
    }

    public void UpdateSkillIcon(Sprite SKillIcon)
    {
        SkillSlot.GetComponent<Image>().sprite = SKillIcon;
    }
    
    public void UnselectSkillIcon()
    {
        SkillSlot.GetComponent<Image>().sprite = BlankIcon;
    }

    public void InventoryOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (InventoryIsOpened)
            {
                InventoryPanel.SetActive(false);
                InventoryIsOpened = false;
            }
            else if (!InventoryIsOpened)
            {
                InventoryPanel.SetActive(true);
                InventoryIsOpened = true;
            }
        }
    }

    public void ActiveSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (WeaponObject)
            {
                WeaponAttackable.ItemSkill();
            }
        }
    }
}
