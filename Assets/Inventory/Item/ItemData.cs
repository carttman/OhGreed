using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    WEAPON,
    ACESSORY,
    
    NONE
}

[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Objects/ItemList")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public int ItemID = 0;
    public Sprite ItemIcon;
    public ItemType ItemType = ItemType.NONE;
    public Sprite ItemSkillIcon;
    public string ItemDescription;
    public string SkillDescription;
}
