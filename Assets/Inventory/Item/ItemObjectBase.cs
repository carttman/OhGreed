using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemObjectBase : MonoBehaviour, IWeaponAttackable
{
    public ItemData itemData;

    protected virtual void Start()
    {
        //Data에 저장된 이미지
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = itemData.ItemIcon;    
    }

    public virtual void Attack()
    {
        Debug.Log("아이템 어택");
    }

    public virtual void ItemSkill()
    {
        Debug.Log("아이템 스킬");
        
    }
}
