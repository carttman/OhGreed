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
    
    [SerializeField]
    private Transform CastingPoint;
    [SerializeField]
    private GameObject CastingVFX;
    public void Attack()
    {
        FindClosestEnemy();
        
        if (Target)
        {
            var VFX = Instantiate(AttackVFX, Target.position, Quaternion.identity);
            var castVFX = Instantiate(CastingVFX, CastingPoint.position, Quaternion.identity);
            castVFX.transform.SetParent(CastingPoint);
            Destroy(VFX, .7f);
            Destroy(castVFX, .7f);
        }
        else
        {
            var VFX = Instantiate(AttackVFX, transform.position + new Vector3(2 * ItemManager.Instance.Player.transform.localScale.x, -0.5f, 0) , Quaternion.identity);
            var castVFX = Instantiate(CastingVFX, CastingPoint.position, Quaternion.identity);
            castVFX.transform.SetParent(CastingPoint);
            Destroy(VFX, .7f);
            Destroy(castVFX, .7f);
        }
    }

    public void ItemSkill()
    {
        var arshaVFX = Instantiate(SkillVFX, transform.position + new Vector3(-2 * ItemManager.Instance.Player.transform.localScale.x, 2.5f, 0), Quaternion.identity);
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
