using UnityEngine;
using UnityEngine.EventSystems;

public class EquipWeaponSlot : BlankIcon
{
    protected override void Start()
    {
        slotType = SlotType.WEAPONSLOT;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemUIBase>();
        //이 슬롯의 인덱스와 슬롯타입 넘겨준다
        ItemManager.Instance.MoveItemInventory(MyIndex, slotType);
    }
}
