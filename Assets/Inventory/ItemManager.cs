using System;
using TMPro;
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
    
    public GameObject TempItemDetailPanel;
    private void Awake()
    {
       Singleton();
    }
    
    void Start()
    {
        InventoryPanel.SetActive(false);
        TempIcon.SetActive(false);
        TempItemDetailPanel.SetActive(false);
        
        OnItemAdded?.Invoke(ItemUIBaseSword, 0);
        OnItemAdded?.Invoke(ItemUIBaseBow, 1);
        OnItemAdded?.Invoke(ItemUIBaseWand, 2);
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        //DontDestroyOnLoad(gameObject);
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
            WeaponAttackable = null;
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

    public void ActiveItemDetailPanel(Sprite itemIcon, Sprite skillIcon ,string itemName, string itemDescription, string skillDescription)
    {
        TempItemDetailPanel.SetActive(true);
        
        TempItemDetailPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = itemName;
        TempItemDetailPanel.transform.GetChild(1).GetComponent<Image>().sprite = itemIcon;
        TempItemDetailPanel.transform.GetChild(2).GetComponent<TMP_Text>().text = itemDescription;
        TempItemDetailPanel.transform.GetChild(4).GetComponent<Image>().sprite = skillIcon;
        TempItemDetailPanel.transform.GetChild(5).GetComponent<TMP_Text>().text = skillDescription;
        
        
    }

    public void DeactiveItemDetailPanel()
    {
        TempItemDetailPanel.SetActive(false);
        
    }
}
