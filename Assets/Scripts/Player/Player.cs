using TreeEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform transformPlayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transformPlayer = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }


    private void PlayerMovement()
    {
        Vector2 InputVector = gameInput.GetMovementNormalized();
        Vector3 moveDir = new Vector3(InputVector.x, InputVector.y, 0f);

        float moveDistance = Time.deltaTime * moveSpeed;

        transform.position += moveDir * moveDistance;
    }
}
