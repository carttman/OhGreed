using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject BlankIconPrefab;
    private List<GameObject> BlankList;
    private int InventorySize = 15;
    
    public List<ItemUIBase> ItemList;
    
    [SerializeField]
    private GridLayoutGroup InventoryGridLayout;
   
    void Start()
    {
        ItemManager.Instance.OnItemAdded += AddItem;
        ItemManager.Instance.OnItemMoved += MoveItem;
        
        CreateInventory();
    }

    private void CreateInventory()
    {
        BlankList = new List<GameObject>();
        ItemList = new List<ItemUIBase>();
        
        for (int i = 0; i < InventorySize; i++)
        {
            BlankList.Add(Instantiate(BlankIconPrefab, InventoryGridLayout.transform));
            BlankList[i].GetComponent<BlankIcon>()._ItemIndex = i;
            
            ItemList.Add(null);
        }
    }

    private void AddItem(ItemUIBase ItemIcon)
    {
        //if (InventorySize <= ItemList.Count) return;
        
        ItemList[0] = Instantiate(ItemIcon, InventoryGridLayout.transform);
        ItemList[0].transform.SetParent(BlankList[0].transform);
        ItemList[0].transform.localPosition = Vector3.zero;
        
        ItemIcon._ItemIndex = 0;
    }
    
    private void MoveItem(int targetIndex)
    {
        // 드래그 중인 아이템 슬롯의 인덱스
        var draggingIndex = ItemManager.Instance.draggingIconIndex;
        
        // 드래그 중인 아이템 -> 커서 위치에 있는 슬롯을 부모, 위치 
        ItemList[draggingIndex].transform.SetParent(BlankList[targetIndex].transform);
        ItemList[draggingIndex].transform.localPosition = Vector3.zero;
        
        // 현재 슬롯 위치에 드래그 중인 아이템 삽입
        ItemList[targetIndex] = ItemList[draggingIndex];
        // 위치 변경 한 아이템의 인덱스 업데이트
        ItemList[targetIndex]._ItemIndex = targetIndex;
        
        ItemManager.Instance.draggingIconIndex = targetIndex;
        
        //기존 위치 비우기
        ItemList[draggingIndex] = null;
    }
}
