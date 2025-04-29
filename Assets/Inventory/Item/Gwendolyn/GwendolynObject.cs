using System.Linq;
using UnityEngine;

public class GwendolynObject : ItemObjectBase, IWeaponAttackable
{
    [SerializeField] 
    private GameObject AttackVFX;
    [SerializeField] 
    private GameObject SkillVFX;
    
    [SerializeField] 
    private float SearchRadius = 10f;
    private Transform Target;
    
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

        FindClosestEnemy();
        
        
        if (Target)
        {
            var VFX = Instantiate(AttackVFX, Target.position, Quaternion.identity);
            Destroy(VFX, .7f);
        }
        else
        {
            var VFX = Instantiate(AttackVFX, transform.position + new Vector3(2, 0, 0) * ItemManager.Instance.Player.transform.localScale.x, Quaternion.identity);
            
            Destroy(VFX, .7f);
        }
    }

    public void ItemSkill()
    {
        Debug.Log("완드 스킬");
        var arshaVFX = Instantiate(SkillVFX, transform.position, Quaternion.identity);
    }
    
    private void FindClosestEnemy()
    {
        // SearchRadius 반경 내에서 모든 콜라이더를 탐색
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SearchRadius);

        // "Enemy" 레이어에 속한 가장 가까운 오브젝트 찾기
        Collider2D closestCollider = colliders
            .Where(c => c.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            .OrderBy(c => Vector2.Distance(transform.position, c.transform.position))
            .FirstOrDefault();

        // 가장 가까운 Enemy의 Transform을 Target으로 설정
        if (closestCollider != null)
        {
            Target = closestCollider.transform;
        }
        else
        {
            Target = null;
        }
    }
}
