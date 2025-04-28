using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public Portal portal;
    private int enemiesRemaining = 0;

    public void RegisterEnemy()
    {
        enemiesRemaining++;
        Debug.Log($"적 등록됨! 남은 적 수 : {enemiesRemaining}");
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        Debug.Log($"적 처치! 남은 적 수 : {enemiesRemaining}");

        if (enemiesRemaining <= 0)
        {
            if (portal != null)
            {
                portal.ActivePortal();
            }
            else
            {
                Debug.LogWarning("Portal이 연결되지 않았어!");
            }
        }
    }
}
