using UnityEngine;

public class PooledEnemy : MonoBehaviour
{
    private EnemySpawner spawner;
    private Transform player;
    private float despawnDistance = 25f;


    private void Update()
    {
        if (player == null) return;

        // Despawn if too far from player
        if (Vector2.Distance(transform.position, player.position) > despawnDistance)
        {
            spawner.ReleaseEnemy(gameObject);
        }

        // Optional: fake health-based death
        if (Input.GetKeyDown(KeyCode.K)) // for testing kill
        {
            spawner.ReleaseEnemy(gameObject);
        }
    }

    public void SetSpawner(EnemySpawner s)
    {
        spawner = s;
        player = s.player;
    }


    public void Die()
    {
        gameObject.SetActive(false); 
    }
}
