using System.Collections;
using System.Linq;
using UnityEngine;

public class Gwendolyn_Arsha : MonoBehaviour
{
    [SerializeField]
    private GameObject SpinMagic_CastingVFX;
    private GameObject TempSpinMagic_CastingVFX;
    [SerializeField]
    private GameObject SpinBulletVFX;

    private Animator _animator;
    
    [SerializeField]
    private Transform CastingPoint;
    [SerializeField]
    private Transform BulletSpawnPoint;
    
    [SerializeField] 
    private float SearchRadius = 10f;
    private Transform TargetTranform;

    [Header("미사일 기능 관련")]
    public float m_speed = 0.5f; // 미사일 속도.
    [Space(10f)]
    public float m_distanceFromStart = 6.0f; // 시작 지점을 기준으로 얼마나 꺾일지.
    public float m_distanceFromEnd = 3.0f; // 도착 지점을 기준으로 얼마나 꺾일지.
    [Space(10f)]
    public int m_shotCount = 12; // 총 몇 개 발사할건지.
    [Range(0, 1)] public float m_interval = 0.15f;
    public int m_shotCountEveryInterval = 2; // 한번에 몇 개씩 발사할건지.

    [SerializeField] 
    private AudioClip SpawnBulletSFX;
    [SerializeField]
    private AudioClip BulletStartSFX;
    void Start()
    {
        FindClosestEnemy();
        
        StartCoroutine(SpawnSpellMagic());

        StartCoroutine(DestroyCoroutine());
        
    }
    
    private IEnumerator SpawnSpellMagic()
    {
        yield return new WaitForSeconds(4);
        TempSpinMagic_CastingVFX = Instantiate(SpinMagic_CastingVFX, CastingPoint.position, transform.rotation);
        
        yield return new WaitForSeconds(1);
        StartCoroutine(CreateSpinBullet());
    }
    
    IEnumerator CreateSpinBullet()
    {
        if (TargetTranform == null)
        {
            TargetTranform = this.transform;
        }
        
        var audioSource = GetComponent<AudioSource>();
        
        int _shotCount = m_shotCount;
        while (_shotCount > 0)
        {
            audioSource.PlayOneShot(SpawnBulletSFX);
            for(int i = 0; i < m_shotCountEveryInterval; i++)
            {
                if(_shotCount > 0)
                {
                    GameObject bullet = Instantiate(SpinBulletVFX, CastingPoint.position, BulletSpawnPoint.rotation * Quaternion.Euler(0, 0, 45));
                    bullet.GetComponent<SpinBullet>().Init(CastingPoint.transform, TargetTranform, m_speed, m_distanceFromStart, m_distanceFromEnd);

                    _shotCount--;
                }
            }
            yield return new WaitForSeconds(m_interval);
        }
        yield return null;
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
            TargetTranform = closestCollider.transform;
        }
        else
        {
            TargetTranform = null;
        }
    }
    
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(8f);
        TempSpinMagic_CastingVFX.GetComponent<Animator>().SetTrigger("CastingEnd");
        
        yield return new WaitForSeconds(2f);
        Destroy(TempSpinMagic_CastingVFX);
        Destroy(gameObject);
    }
    
    public void PlaySound(AudioClip clip)
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }
}
