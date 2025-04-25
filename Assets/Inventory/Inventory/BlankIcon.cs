using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


public enum SlotType
{
    WEAPONSLOT,
    ACCESSORYSLOT,
    ITEMSLOT,
    
    NONE
}
public class BlankIcon : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int MyIndex;
    
    public SlotType slotType;
    
    [SerializeField]
    private Sprite HoverIcon;
    
    [SerializeField]
    private Sprite DefaultIcon;
    
    
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(HoverIcon)
            GetComponent<Image>().sprite = HoverIcon;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(DefaultIcon)
            GetComponent<Image>().sprite = DefaultIcon;
    }

}
