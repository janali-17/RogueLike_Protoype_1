using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;

    private void Update()
    {
        if (Player.transformPlayer == null) return;

        Vector2 direction = ((Vector2)Player.transformPlayer.position - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
