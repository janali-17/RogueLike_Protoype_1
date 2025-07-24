using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private Vector2 direction;
    private float timer;
    private IObjectPool<Bullet> bulletPool;

    public void SetTarget(Transform enemy)
    {
        float shootRadius = 10f;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = null;
        float minDist = shootRadius;

        foreach (GameObject Enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= minDist)
            {
                minDist = dist;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            direction = (closestEnemy.position - transform.position).normalized;
        }
        else
        {
            direction = Vector2.right; // fallback
        }

        timer = 0f;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            bulletPool.Release(this);
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PooledEnemy>().Die();
            bulletPool.Release(this);
            Debug.Log("Bullet Hitting SOMEthing");

        }
    }
}
