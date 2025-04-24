using UnityEngine;

public class KatanaObject : ItemObjectBase
{
    public GameObject VFX;
    
    private Camera _camera;

    protected override void Start()
    {
        _camera = Camera.main;
        base.Start();
        Debug.Log(itemData.ItemName);
        
    }

    void Update()
    {
         SetDirection();
    }
    
    public override void Attack()
    {
        Debug.Log("소드 어택");

        var v = Instantiate(VFX, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        Destroy(v, .5f);
    }
    
    private void SetDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
    
        mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
    
        Vector3 direction = mousePosition - transform.position;
    
        // Z축 회전 유지 (2D 게임) 또는 Y축 회전 설정 (3D 게임)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
    
}
