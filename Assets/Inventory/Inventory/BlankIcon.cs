using System;
using UnityEngine;
using UnityEngine.EventSystems;


public enum EquipType
{
    WEAPONSLOT,
    ACCESSORYSLOT,
    ITEMSLOT,
    
    NONE
}
public class BlankIcon : MonoBehaviour, IDropHandler
{
    public int _ItemIndex;
    
    public EquipType _EquipType;

    protected virtual void Start()
    {
        _EquipType = EquipType.ITEMSLOT;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        //Debug.Log($"OnDrop to BlankIcon : {_ItemIndex}");
        
        var item = eventData.pointerDrag.GetComponent<ItemUIBase>();
        //빈 슬롯의 인덱스 넘겨준다
        ItemManager.Instance.MoveItem(_ItemIndex, _EquipType);
        
        
    }
}
