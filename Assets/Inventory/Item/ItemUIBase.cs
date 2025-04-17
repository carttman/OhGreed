using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemUIBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler

{
    public ItemData itemData;

   
    private GameObject _draggingIcon; // 드래그 시 생성되는 임시 아이콘
    private Transform originalParent; // 아이템의 원래 부모
    private Vector3 originalPosition; // 아이템의 원래 위치

    void awake()
    {

    }
    
    void Start()
    {
        
        //Data에 저장된 이미지
        Image image = GetComponent<Image>();
        image.sprite = itemData.ItemIcon;    
        image.preserveAspect = true;
    }

    void Update()
    {
        
    }
    
   public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData == null || itemData.ItemIcon == null)
        {
            Debug.LogWarning("ItemData or itemIcon is null.");
            return;
        }

        Debug.LogWarning("드래그 시작");
        
        // 드래그 시작 시 원래 부모와 위치 저장
        originalParent = transform.parent;
        originalPosition = transform.position;
        
        _draggingIcon = ItemManager.Instance.draggingIcon;
        _draggingIcon.SetActive(true);
        var iconImage = _draggingIcon.GetComponentInChildren<Image>();
        iconImage.sprite = itemData.ItemIcon;
        iconImage.preserveAspect = true;
        // // 드래그 시 표시할 임시 아이콘 생성
        // draggingIcon = new GameObject("DraggingIcon");
        // draggingIcon.transform.SetParent(null); // 계층 구조에서 독립
        // draggingIcon.transform.localScale = Vector3.one;
        //
        // // 아이콘의 UI Image 생성 및 Sprite 설정
        // var iconCanvas = draggingIcon.AddComponent<Canvas>();
        // iconCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // iconCanvas.sortingOrder = 1;
        //
        // var iconImage = draggingIcon.AddComponent<Image>();
        // iconImage.sprite = itemData.ItemIcon;
        // iconImage.raycastTarget = false; // Raycast 방지
        //
        // // 크기 설정
        // var rectTransform = draggingIcon.GetComponent<RectTransform>();
        // rectTransform.sizeDelta = new Vector2(50, 50);
        // rectTransform.position = Input.mousePosition; // 드래그 시작 시 마우스 위치에 생성
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggingIcon)
        {
            // 드래그 중, 아이콘의 위치를 마우스 위치로 업데이트
            Debug.LogWarning("드래그 중");
            _draggingIcon.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggingIcon)
        {
            Debug.LogWarning("드래그 끝");
            _draggingIcon.SetActive(false);
        }
    //
    //     // Raycast로 드롭 위치 확인
    //     var results = new System.Collections.Generic.List<RaycastResult>();
    //     EventSystem.current.RaycastAll(eventData, results);
    //
    //     // foreach (var result in results)
    //     // {
    //     //     if (result.gameObject.CompareTag("Slot"))
    //     //     {
    //     //         // 슬롯에 드롭: 새로운 부모로 설정 및 위치 조정
    //     //         transform.SetParent(result.gameObject.transform);
    //     //         transform.localPosition = Vector3.zero;
    //     //         return;
    //     //     }
    //     // }
    //
    //     // 슬롯이 아닌 곳에 드롭: 원래 위치로 복구
    //     transform.SetParent(originalParent);
    //     transform.position = originalPosition;
    }


}
