using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject BlankIconPrefab;
    private List<GameObject> BlankIcons;
    private int InventorySize = 15;
    
    public List<ItemUIBase> InventoryItems;
    
    [SerializeField]
    private GridLayoutGroup InventoryGridLayout;

    public EquipWeaponPanel equipWeaponPanel;

   void Awake()
    {
        ItemManager.Instance.OnItemAdded += AddItem;
        ItemManager.Instance.OnMoveToItemSlot += MoveToItemSlot;
        
        CreateInventory();
    }
   
    private void OnDestroy()
    {
        ItemManager.Instance.OnItemAdded -= AddItem;
        ItemManager.Instance.OnMoveToItemSlot -= MoveToItemSlot;
    }

    private void CreateInventory()
    {
        BlankIcons = new List<GameObject>();
        InventoryItems = new List<ItemUIBase>();
        
        for (int i = 0; i < InventorySize; i++)
        {
            BlankIcons.Add(Instantiate(BlankIconPrefab, InventoryGridLayout.transform));
            BlankIcons[i].GetComponent<BlankIcon>().MyIndex = i;
            
            InventoryItems.Add(null);
        }
    }

    private void AddItem(ItemUIBase Item, int targetIndex)
    {
        if (InventoryItems[targetIndex] != null)
        {
            return;
        }
        InventoryItems[targetIndex] = Instantiate(Item, InventoryGridLayout.transform);
        InventoryItems[targetIndex].transform.SetParent(BlankIcons[targetIndex].transform);
        InventoryItems[targetIndex].transform.localPosition = Vector3.zero;
        InventoryItems[targetIndex]._ItemIndex = targetIndex;
    }
    
    private void MoveToItemSlot(int targetIndex, SlotType prevSlotType)
    {
        // 드래그 중인 아이템 슬롯의 인덱스
        var draggingIndex = ItemManager.Instance.TempIconIndex;
        var targetSlotType = BlankIcons[targetIndex].GetComponent<BlankIcon>().slotType;
        
        //제자리 드래그 방지
        if(draggingIndex == targetIndex && targetSlotType == prevSlotType) return;
        
        // 무기슬롯 -> 아이템 슬롯이라면, 무기슬롯에서 아이템 가져온다.
        if (prevSlotType == SlotType.WEAPONSLOT)
        {   
            if (InventoryItems[targetIndex]) return;
            // 인벤 아이템 슬롯에 자식으로 설정
            equipWeaponPanel.EquipItems[draggingIndex].transform.SetParent(BlankIcons[targetIndex].transform);
            equipWeaponPanel.EquipItems[draggingIndex].transform.localPosition = Vector3.zero;
            
            // 현재 슬롯 위치에 드래그 중인 아이템 삽입
            InventoryItems[targetIndex] = equipWeaponPanel.EquipItems[draggingIndex];
            // 위치 변경 한 아이템의 인덱스 업데이트
            InventoryItems[targetIndex]._ItemIndex = targetIndex;
            
            equipWeaponPanel.EquipItems[draggingIndex] = null;
            
            // 스폰된 무기 지운다
            ItemManager.Instance.DestroyItemObject();
            ItemManager.Instance.UnselectSkillIcon();
        }
        else
        {
            if (InventoryItems[targetIndex]) return;
            
            // 드래그 중인 아이템 -> 커서 위치에 있는 슬롯을 부모, 위치 
            InventoryItems[draggingIndex].transform.SetParent(BlankIcons[targetIndex].transform);
            InventoryItems[draggingIndex].transform.localPosition = Vector3.zero;
            
            // 현재 슬롯 위치에 드래그 중인 아이템 삽입
            InventoryItems[targetIndex] = InventoryItems[draggingIndex];
            // 위치 변경 한 아이템의 인덱스 업데이트
            InventoryItems[targetIndex]._ItemIndex = targetIndex;
            
            //기존 위치 비우기
            InventoryItems[draggingIndex] = null;
        }
    }
}
