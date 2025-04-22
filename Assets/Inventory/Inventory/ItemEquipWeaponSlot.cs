using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEquipWeaponSlot : BlankIcon
{
    protected override void Start()
    {
        _EquipType = EquipType.WEAPONSLOT;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //Debug.Log($"OnDrop to ItemEquipWeaponSlot : {_ItemIndex}");
        
        var item = eventData.pointerDrag.GetComponent<ItemUIBase>();
        //빈 슬롯의 인덱스 넘겨준다
        ItemManager.Instance.MoveItem(_ItemIndex, _EquipType);
    }
}
