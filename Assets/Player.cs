using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private float MoveInputX;
    [SerializeField ]private float MoveSpeed=3f;
    [SerializeField] private float JumpForce=5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        //Horizontal movement
        MoveInputX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(MoveInputX * MoveSpeed, rb.linearVelocity.y);

        //Jumping mechanics

        if (Input.GetKeyDown(KeyCode.Space))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
    }

}
