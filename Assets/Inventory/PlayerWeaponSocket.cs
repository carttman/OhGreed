using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSocket : MonoBehaviour
{
    public Transform WeaponSocket;
    
    public List<Transform> ConePoints;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        FaceMouse();
    }
    
    private void FaceMouse()
    {
        // 1. 마우스 위치 얻기
        Vector3 mousePosition = Input.mousePosition;

        // 2. 마우스 위치를 월드 좌표로 변환
        mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.nearClipPlane));

        // 3. 바라볼 방향 계산
        Vector3 direction = mousePosition - WeaponSocket.position;

        // Z축 회전 유지 (2D 게임) 또는 Y축 회전 설정 (3D 게임)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 2D 게임 기준
        WeaponSocket.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
