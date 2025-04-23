using UnityEngine;

public class SwordObject : ItemObjectBase
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
        // FaceMouse();
        // Rotate180OnRightClick();
    }
    
    public override void Attack()
    {
        Debug.Log("소드 어택");

        var v = Instantiate(VFX, transform.position, Quaternion.identity);
        Destroy(v, 3f);
    }
    
    // private void FaceMouse()
    // {
    //     // 1. 마우스 위치 얻기
    //     Vector3 mousePosition = Input.mousePosition;
    //
    //     // 2. 마우스 위치를 월드 좌표로 변환
    //     mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.nearClipPlane));
    //
    //     // 3. 바라볼 방향 계산
    //     Vector3 direction = mousePosition - transform.position;
    //
    //     // Z축 회전 유지 (2D 게임) 또는 Y축 회전 설정 (3D 게임)
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 2D 게임 기준
    //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    // }
    //
    // private void Rotate180OnRightClick()
    // {
    //     // 마우스 우클릭 감지
    //     if (Input.GetMouseButtonDown(1)) // 1은 우클릭 버튼
    //     {
    //         // 현재 오브젝트 방향에서 180도 추가 회전
    //         transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180f); // Z축 기준
    //     }
    // }
}
