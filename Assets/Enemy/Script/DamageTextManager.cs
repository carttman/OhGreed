using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager Instance;
    public GameObject damageText;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void SpawnDamageText(Vector3 position, float damage)
    {
        GameObject text = Instantiate(damageText, position, Quaternion.identity);
        text.GetComponent<DamageText>().Setup(damage);
    }
}
