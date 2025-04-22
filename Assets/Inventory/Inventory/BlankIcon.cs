using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public enum SlotType
{
    WEAPONSLOT,
    ACCESSORYSLOT,
    ITEMSLOT,
    
    NONE
}
public class BlankIcon : MonoBehaviour, IDropHandler
{
    public int MyIndex;
    
    public SlotType slotType;

    protected virtual void Start()
    {
        slotType = SlotType.ITEMSLOT;
    }

    //이 아이콘 위에 드랍 되었을 때
    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<ItemUIBase>();
        if (item)
        {
            //이 슬롯의 인덱스와 슬롯타입 넘겨준다
            ItemManager.Instance.MoveItemInventory(MyIndex, slotType);
        }
    }
}
