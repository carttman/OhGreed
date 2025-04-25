using UnityEngine;

public class MagicWandObject : ItemObjectBase, IWeaponAttackable
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("완드 공격");
    }

    public void ItemSkill()
    {
        Debug.Log("완드 스킬");
        
    }
}
