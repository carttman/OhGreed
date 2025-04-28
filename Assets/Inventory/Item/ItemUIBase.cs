using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemUIBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData itemData;
    public int _ItemIndex;   

    private GameObject _draggingIcon; 
    public SlotType slotType;

    public GameObject ItemGameObject;

    public GameObject ItemDetailPanel;
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
        
        //Debug.Log( $" dragging Icon Index : { ItemManager.Instance.draggingIconIndex}");
        ItemManager.Instance.TempIconIndex = _ItemIndex;
        
        _draggingIcon = ItemManager.Instance.TempIcon;
        _draggingIcon.SetActive(true);
        
        
        var iconImage = _draggingIcon.GetComponentInChildren<Image>();
        iconImage.sprite = itemData.ItemIcon;
        iconImage.preserveAspect = true;
        iconImage.raycastTarget = false;
        
        
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
            //Debug.LogWarning("드래그 끝");
            
            var iconImage = _draggingIcon.GetComponentInChildren<Image>();
            iconImage.raycastTarget = true;

            _draggingIcon.SetActive(false);
            _draggingIcon = null;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        
        ItemManager.Instance.ActiveItemDetailPanel(itemData.ItemIcon, itemData.ItemSkillIcon, itemData.ItemName, 
            itemData.ItemDescription, itemData.SkillDescription);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        
        ItemManager.Instance.DeactiveItemDetailPanel();
    }
}
