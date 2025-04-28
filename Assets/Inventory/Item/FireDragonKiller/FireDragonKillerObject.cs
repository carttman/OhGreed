using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FireDragonKillerObject : ItemObjectBase, IWeaponAttackable
{
    [SerializeField]
    private GameObject AttackVFX;
    
    private Camera _camera;

    [SerializeField]
    private GameObject FlameSnakePrefab;
    
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
    
    public void Attack()
    {
        Debug.Log("불검 어택");

        var v = Instantiate(AttackVFX, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90));
        Destroy(v, .3f);
    }
    
    private void SetDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
    
        Vector3 direction = mousePosition - transform.position;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void ItemSkill()
    {
        Debug.Log("불검 스킬");

        var playerScale = ItemManager.Instance.Player.transform.localScale;
        var firstSnakeObject = Instantiate(FlameSnakePrefab, transform.position + new Vector3(2, -0.5f, 0), Quaternion.identity);
        var secondSnakeObject = Instantiate(FlameSnakePrefab, transform.position + new Vector3(-2, -0.5f, 0), Quaternion.identity);
        
        firstSnakeObject.transform.localScale = playerScale;
        secondSnakeObject.transform.localScale = playerScale;
    }
    
}
