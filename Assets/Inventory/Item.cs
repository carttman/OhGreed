using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    WEAPON,
    ACESSORY,
    
    NONE
}
public class Item : MonoBehaviour
{
    [HideInInspector]
    public string ItemName;
    [HideInInspector]
    public Sprite ItemIcon;
    [HideInInspector]
    public ItemType ItemType = ItemType.NONE;
    
    void Start()
    {
        //GetComponent<Image>().sprite = ItemSlotSprite;
    }

    void Update()
    {
        
    }
    
    
}
