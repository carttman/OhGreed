using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EquipWeaponGridPanel : MonoBehaviour
{
    public List<ItemEquipWeaponSlot> WeaponSlotList;
    public ItemEquipWeaponSlot FirstSlot;
    public ItemEquipWeaponSlot SecondSlot;

    public List<ItemUIBase> EquipItemList;
    
    public UI_InventoryPanel _InventoryItemPanel;
    void Start()
    {
        WeaponSlotList = new List<ItemEquipWeaponSlot>();
        WeaponSlotList.Add(FirstSlot);
        WeaponSlotList.Add(SecondSlot);
        
        EquipItemList = new List<ItemUIBase>();
        EquipItemList.Add(null);
        EquipItemList.Add(null);
        
        ItemManager.Instance.OnItemMoveToEquipSlot += OnItemEquiped;
    }
    
    public void OnItemEquiped(int targetIndex)
    {
        
        // 드래그 중인 아이템 슬롯의 인덱스
        var draggingIndex = ItemManager.Instance.draggingIconIndex;
        
        // 드래그 중인 아이템 -> 커서 위치에 있는 슬롯을 부모, 위치 
         _InventoryItemPanel.ItemList[draggingIndex].transform.SetParent(WeaponSlotList[targetIndex].transform);
         _InventoryItemPanel.ItemList[draggingIndex].transform.localPosition = Vector3.zero;
        
        // 현재 슬롯 위치에 드래그 중인 아이템 삽입
        EquipItemList[targetIndex] = _InventoryItemPanel.ItemList[draggingIndex];
        // 위치 변경 한 아이템의 인덱스 업데이트
        EquipItemList[targetIndex]._ItemIndex = targetIndex;
        
        ItemManager.Instance.draggingIconIndex = targetIndex;
        
        //기존 위치 비우기
        _InventoryItemPanel.ItemList[draggingIndex] = null;
    }
}
