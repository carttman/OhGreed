using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public Portal portal;
    private int enemiesRemaining = 0;
    
    public void RegisterEnemy()
    {
        enemiesRemaining++;
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;

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
