using UnityEngine;
using UnityEngine.Pool;


public class Pistol : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireDelay = 1.5f;

    private float fireTimer = 0f;
    private IObjectPool<Bullet> bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () =>
            {
                GameObject bulletGO = Instantiate(bulletPrefab);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                bullet.SetPool(bulletPool);
                return bullet;
            },
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 50
        );
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireDelay)
        {
            fireTimer = 0f;

            Transform target = GetClosestEnemy();
            if (target != null)
            {
                Bullet bullet = bulletPool.Get();
                bullet.transform.position = transform.position;
                bullet.SetTarget(target); // ? direction will be set here
            }
        }
    }

    private Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
