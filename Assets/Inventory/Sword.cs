using UnityEngine;

public class Sword : Item
{
    public Sprite itemIcon;
    void Awake()
    {
        ItemName = "Sword";
        ItemType = ItemType.WEAPON;
        ItemIcon = itemIcon;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
