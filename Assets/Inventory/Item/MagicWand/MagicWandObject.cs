using UnityEngine;

public class MagicWandObject : ItemObjectBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Attack()
    {
        Debug.Log("완드 공격");
    }

    public override void ItemSkill()
    {
        Debug.Log("완드 스킬");
        
    }
}
