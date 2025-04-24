using System.Collections;
using UnityEngine;

public class KatanaObject : ItemObjectBase
{
    [SerializeField]
    private GameObject AttackVFX;
    
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject SkillVFX;
    
    [SerializeField]
    private GameObject SkillReadyVFX;
    
    private GameObject SkillEffect;
    
    protected override void Start()
    {
        base.Start();
        _camera = Camera.main;
    }

    void Update()
    {
         SetDirection();
         
         if(SkillEffect) 
             SkillEffect.transform.position = _camera.transform.position + _camera.transform.forward * 0.5f;
    }
    
    public override void Attack()
    {
        Debug.Log("카타나 어택");

        var v = Instantiate(AttackVFX, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
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

    public override void ItemSkill()
    {
        Debug.Log("카타나 스킬");

        StartCoroutine(SkillState());
        // SkillEffect = Instantiate(SkillVFX, transform.position, Quaternion.identity);
        // Destroy(SkillEffect, 1f);
    }
    
    IEnumerator SkillState()
    {
       
        SkillEffect = Instantiate(SkillReadyVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(SkillEffect);
        
        SkillEffect = Instantiate(SkillVFX, transform.position, Quaternion.identity);
        Destroy(SkillEffect, 1f);
    }
}
