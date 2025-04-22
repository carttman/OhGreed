using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemUIBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler
{
    public ItemData itemData;
    public int _ItemIndex;   

    private GameObject _draggingIcon; // 드래그 시 생성되는 임시 아이콘
    [FormerlySerializedAs("_EquipType")] public SlotType slotType;
    
    void Start()
    {
        //Data에 저장된 이미지
        Image image = GetComponent<Image>();
        image.sprite = itemData.ItemIcon;    
        image.preserveAspect = true;
    }
    
   public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData == null || itemData.ItemIcon == null)
        {
            Debug.LogWarning("ItemData or itemIcon is null.");
            return;
        }

        //Debug.LogWarning("드래그 시작");
        
        //Debug.Log( $" dragging Icon Index : { ItemManager.Instance.draggingIconIndex}");
        ItemManager.Instance.TempIconIndex = _ItemIndex;
        
        _draggingIcon = ItemManager.Instance.TempIcon;
        _draggingIcon.SetActive(true);
        
        
        var iconImage = _draggingIcon.GetComponentInChildren<Image>();
        iconImage.sprite = itemData.ItemIcon;
        iconImage.preserveAspect = true;
        iconImage.raycastTarget = false;
        
        // =================================
        
        //아이템 매니저에 있는 임시아이템 타입 넣어주기
        slotType = GetComponentInParent<BlankIcon>().slotType;
        Debug.Log($"_EquipType : {slotType}");
        ItemManager.Instance.TempSlotType = slotType;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggingIcon)
        {
            //Debug.LogWarning("드래그 중");
            _draggingIcon.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggingIcon)
        {
            Debug.LogWarning("드래그 끝");
            
            var iconImage = _draggingIcon.GetComponentInChildren<Image>();
            iconImage.raycastTarget = true;

            _draggingIcon.SetActive(false);
            _draggingIcon = null;
        }
    }
    
    // public void OnDrop(PointerEventData eventData)
    // {
    //     Debug.Log($"OnDrop to BlankIcon : {_ItemIndex}");
    //     
    //     var item = eventData.pointerDrag.GetComponent<ItemUIBase>();
    //     
    //     Debug.Log(item.itemData.ItemName);
    //     
    //     ItemManager.Instance.MoveItem(_ItemIndex);
    // }
}
