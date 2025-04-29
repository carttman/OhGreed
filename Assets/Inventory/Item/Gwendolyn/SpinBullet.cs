using System.Linq;
using UnityEngine;

public class SpinBullet : MonoBehaviour
{
    Vector3[] m_points = new Vector3[4];

    private float m_timerMax = 0;
    private float m_timerCurrent = 0;
    private float m_speed;

    private float tempSpeed;
    private float tempPointDistanceFromStartTr;
    private float tempPointDistanceFromEndTr;
    
    [SerializeField] 
    private float SearchRadius = 10f;
    public Transform _Target;
    
    public void Init(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        tempSpeed = _speed;
        tempPointDistanceFromStartTr = _newPointDistanceFromStartTr;
        tempPointDistanceFromEndTr = _newPointDistanceFromEndTr;
        
        m_speed = _speed;
        
        // 끝에 도착할 시간을 랜덤으로 줌.
        m_timerMax = Random.Range(0.8f, 1.0f);

        // 시작 지점.
        m_points[0] = _startTr.position; 

        // 시작 지점을 기준으로 랜덤 포인트 지정.
        m_points[1] = _startTr.position +
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) + // X (좌, 우 전체)
            (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) + // Y (아래쪽 조금, 위쪽 전체)
            (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward); // Z (뒤 쪽만)

        // 도착 지점을 기준으로 랜덤 포인트 지정.
        m_points[2] = _endTr.position +
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + // X (좌, 우 전체)
            (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) + // Y (위, 아래 전체)
            (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward); // Z (앞 쪽만)

        // 도착 지점.
        m_points[3] = _endTr.position;

        transform.position = _startTr.position;
    }

    void Update()
    {
        // 경과 시간 계산
        m_timerCurrent += Time.deltaTime * m_speed;

        // 베지어 곡선으로 X,Y,Z 좌표 얻기
        transform.position = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z, m_points[3].z)
        );

        if (_Target == null)
        {
            FindClosestEnemy();
        }
        
        // 베지어 곡선 완료 시 다시 추적에 필요한 새로운 곡선 업데이트
        if (m_timerCurrent >= m_timerMax)
        {
            m_timerCurrent = 0; // 타이머 초기화
            
            UpdateChasing(transform, _Target, m_speed, tempPointDistanceFromStartTr, tempPointDistanceFromEndTr); // 새로운 타겟 경로 생성
            
        }

    }

    /// <summary>
    /// 3차 베지어 곡선.
    /// </summary>
    /// <param name="a">시작 위치</param>
    /// <param name="b">시작 위치에서 얼마나 꺾일 지 정하는 위치</param>
    /// <param name="c">도착 위치에서 얼마나 꺾일 지 정하는 위치</param>
    /// <param name="d">도착 위치</param>
    /// <returns></returns>
    private float CubicBezierCurve(float a, float b, float c, float d)
    {
        // (0~1)의 값에 따라 베지어 곡선 값을 구하기 때문에, 비율에 따른 시간을 구했다.
        float t = m_timerCurrent / m_timerMax; // (현재 경과 시간 / 최대 시간)

        // 방정식.
        /*
        return Mathf.Pow((1 - t), 3) * a
            + Mathf.Pow((1 - t), 2) * 3 * t * b
            + Mathf.Pow(t, 2) * 3 * (1 - t) * c
            + Mathf.Pow(t, 3) * d;
        */

        // 이해한대로 편하게 쓰면.
        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 타겟과 충돌했을 때
        {
            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(.1f);
        }
    }


   
    public void UpdateChasing(Transform _startTr, Transform _endTr, float _speed, float _newPointDistanceFromStartTr, float _newPointDistanceFromEndTr)
    {
        m_speed = _speed;

        // 끝에 도착할 시간을 랜덤으로 줌.
        m_timerMax = Random.Range(0.8f, 1.0f);

        // 시작 지점.
        m_points[0] = _startTr.position; 

        // 시작 지점을 기준으로 랜덤 포인트 지정.
        m_points[1] = _startTr.position +
                      (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * _startTr.right) + // X (좌, 우 전체)
                      (_newPointDistanceFromStartTr * Random.Range(-0.15f, 1.0f) * _startTr.up) + // Y (아래쪽 조금, 위쪽 전체)
                      (_newPointDistanceFromStartTr * Random.Range(-1.0f, -0.8f) * _startTr.forward); // Z (뒤 쪽만)

        // 도착 지점을 기준으로 랜덤 포인트 지정.
        m_points[2] = _endTr.position +
                      (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.right) + // X (좌, 우 전체)
                      (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * _endTr.up) + // Y (위, 아래 전체)
                      (_newPointDistanceFromEndTr * Random.Range(0.8f, 1.0f) * _endTr.forward); // Z (앞 쪽만)

        // 도착 지점.
        m_points[3] = _endTr.position;

        transform.position = _startTr.position;
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
            _Target = closestCollider.transform;
            m_speed = tempSpeed;
        }
        else
        {
            _Target = null;
            m_speed = 0;
        }
    }
}
