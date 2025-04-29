using UnityEngine;

public class PortalParticleSpawner : MonoBehaviour
{
    public ParticleSystem particlePrefab;      // 사용할 파티클 프리팹
    public Material[] particleMaterials;       // 여러 개의 머티리얼을 담을 배열

    public void SpawnPortalParticle(Vector3 position)
    {
        if (particlePrefab == null || particleMaterials.Length == 0)
            return;

        ParticleSystem ps = Instantiate(particlePrefab, position, Quaternion.identity);

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            renderer.material = particleMaterials[Random.Range(0, particleMaterials.Length)];
        }

        ps.Play();
        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
    }
}
