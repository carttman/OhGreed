using UnityEngine;

public class SwordObject : ItemObjectBase
{
    public GameObject VFX;
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

        var v = Instantiate(VFX, transform.position, Quaternion.identity);
        Destroy(v, 3f);
    }
}
