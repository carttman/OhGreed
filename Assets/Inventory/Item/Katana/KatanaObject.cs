using System;
using System.Collections;
using UnityEngine;

public class KatanaObject : ItemObjectBase, IWeaponAttackable
{
    [SerializeField]
    private GameObject AttackVFX;
    
    private Camera _camera;

    [SerializeField]
    private GameObject SkillVFX;
    
    [SerializeField]
    private GameObject SkillReadyVFX;
    [SerializeField]
    private GameObject SkillBeginVFX;
    
    private GameObject SkillEffect;

    [SerializeField] 
    private AudioClip SkillReadySFX;
    
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
    
    public void Attack()
    {
        var v = Instantiate(AttackVFX, transform.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        Destroy(v, .5f);
    }
    
    private void SetDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
    
        mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
    
        Vector3 direction = mousePosition - transform.position;
    
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }

    public void ItemSkill()
    {
        StartCoroutine(SkillState());
    }
    
    IEnumerator SkillState()
    {
        SkillEffect = Instantiate(SkillReadyVFX, transform.position, Quaternion.identity);
        Destroy(SkillEffect, 0.4f);
        
        var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(SkillReadySFX);
        
        var SkillBegin = Instantiate(SkillBeginVFX, transform.position, Quaternion.identity);
        SkillBegin.transform.SetParent(_camera.transform);
        
        yield return new WaitForSeconds(1.2f);
        
        Destroy(SkillBegin, 0.8f);
        //Destroy(SkillBegin);
        SkillEffect = Instantiate(SkillVFX, transform.position, Quaternion.identity);
        Destroy(SkillEffect, 1f);
    }
}
