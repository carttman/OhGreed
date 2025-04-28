using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private float Distance = 100f;
    
    [HideInInspector]
    public float currScale;

    [SerializeField] 
    private float NormalSpeed = 10f; // 정상 속도
    [SerializeField] 
    private float SlowSpeed = 2f; // 느려진 속도

    private CircleCollider2D _circleCollider;
    private Tweener moveTweener; // DOTween Tweener 저장
    
    [SerializeField]
    private Transform Target;
    private Vector3 moveDirection; // 처음 타겟 방향 저장

    [SerializeField] 
    private float SearchRadius = 10f;

    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        
        FindClosestEnemy();
        
        if (Target != null)
        {
            // 타겟 방향으로 단위 방향 벡터 계산
            moveDirection = (Target.position - transform.position).normalized;

            // 자신을 타겟 방향으로 회전
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // 타겟이 없을 경우 이동 방향을 기본값으로 설정
            moveDirection = transform.right * currScale;
        }


        MoveForward(NormalSpeed);
    }
    
    private void MoveForward(float speed)
    {
        // 저장된 이동 방향으로 지정된 거리만큼 이동
        moveTweener = transform.DOMove(transform.position + moveDirection * Distance, Distance / speed)
            .SetEase(Ease.Linear)
            .OnComplete(() => Destroy(gameObject));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            StartCoroutine(DamageCoroutine(enemyHealth));
        }
    }

    private IEnumerator DamageCoroutine(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(1);
        _circleCollider.enabled = false;
        
        // 이동 속도 느리게 조정
        moveTweener.Kill(); // 기존 Tweener 취소
        MoveForward(SlowSpeed);
        
        yield return new WaitForSeconds(0.5f);
        
        // 이동 속도 정상으로 복구
        moveTweener.Kill(); // 느린 이동 Tweener 취소
        MoveForward(NormalSpeed);

        _circleCollider.enabled = true;
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
