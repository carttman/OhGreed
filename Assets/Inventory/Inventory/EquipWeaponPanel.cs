using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EquipWeaponPanel : MonoBehaviour
{
    [SerializeField]
    private List<EquipWeaponSlot> WeaponBlankSlots;
    [SerializeField]
    private EquipWeaponSlot FirstSlot;
    [SerializeField]
    private EquipWeaponSlot SecondSlot;

    public List<ItemUIBase> EquipItems;
    
    public UI_InventoryPanel _InventoryItemPanel;
    private void Start()
    {
        ItemManager.Instance.OnMoveToEquipSlot += OnEquiped;
        
        CreateWeaponSlot();
    }

    private void CreateWeaponSlot()
    {
        WeaponBlankSlots = new List<EquipWeaponSlot>();
        WeaponBlankSlots.Add(FirstSlot);
        WeaponBlankSlots.Add(SecondSlot);
        
        EquipItems = new List<ItemUIBase>();
        EquipItems.Add(null);
        EquipItems.Add(null);

    }
    
    private void OnEquiped(int targetIndex)
    {
        if (EquipItems[targetIndex]) return;
        
        // 드래그 중인 아이템 슬롯의 인덱스 저장
        var draggingIndex = ItemManager.Instance.TempIconIndex;
        
        // 드래그 중인 아이템 -> 커서 위치에 있는 슬롯을 부모, 위치 
         _InventoryItemPanel.InventoryItems[draggingIndex].transform.SetParent(WeaponBlankSlots[targetIndex].transform);
         _InventoryItemPanel.InventoryItems[draggingIndex].transform.localPosition = Vector3.zero;
        
        // 현재 슬롯 위치에 드래그 중인 아이템 삽입
        EquipItems[targetIndex] = _InventoryItemPanel.InventoryItems[draggingIndex];
        // 위치 변경 한 아이템의 인덱스 업데이트
        EquipItems[targetIndex]._ItemIndex = targetIndex;
        
        ItemManager.Instance.TempIconIndex = targetIndex;
        
        //기존 위치 비우기
        _InventoryItemPanel.InventoryItems[draggingIndex] = null;
        
        ItemManager.Instance.SpawnItemObject(EquipItems[targetIndex].ItemGameObject);
        
        //SpawnItem(targetIndex);
    }

    // public void SpawnItem(int targetIndex)
    // {
    //     Instantiate(EquipItems[targetIndex].ItemGameObject, ItemManager.Instance.Player.transform);
    // }
}
