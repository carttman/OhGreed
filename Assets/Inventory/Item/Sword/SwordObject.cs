using UnityEngine;

public class SwordObject : ItemObjectBase
{
    protected override void Start()
    {
        base.Start();
        Debug.Log(itemData.ItemName);
        
    }

    void Update()
    {
        
    }
    
    public override void Attack()
    {
        Debug.Log("소드 어택");
    }
}
