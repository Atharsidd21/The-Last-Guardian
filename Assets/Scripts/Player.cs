using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [Header("Movement Details")]
    [SerializeField]private bool FacingRight=true;
    [SerializeField ]private float MoveSpeed=3f;
    [SerializeField] private float JumpForce=5f;
    private float MoveInputX;

    [Header("Collision Details")]
    [SerializeField] private float GroundCheckDistance;
    private bool IsGrounded ;
    [SerializeField] private LayerMask GroundLayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleCollision();
        PlayerMovement();
        HandleAnimation();
        FlipHandle();
    }
    private void HandleAnimation()
    {
        animator.SetFloat("XVelocity", rb.linearVelocity.x);
        animator.SetBool("IsGrounded", IsGrounded);
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
    }

    private void PlayerMovement()
    {
        //Horizontal movement
        MoveInputX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(MoveInputX * MoveSpeed, rb.linearVelocity.y);

        //Jumping mechanics

        if (Input.GetKeyDown(KeyCode.Space)&& IsGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);

    }
    //Points
    /*Raycast is a powerful tool in Unity that allows you to check for collisions and interactions in a specific direction.
    In this case, we use it to check if the player is grounded by casting a ray downwards from the player's position.
    If the ray hits an object on the GroundLayer within the specified GroundCheckDistance,
    we consider the player to be grounded, allowing them to jump.
    This method is crucial for implementing jumping mechanics,
    as it ensures that the player can only jump when they are on the ground, preventing mid-air jumps and
    adding realism to the gameplay.*/
    private void HandleCollision()
    { 
        IsGrounded= Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayer);
    }
    private void FlipHandle()
    {
        if (rb.linearVelocity.x > 0 && FacingRight== false)
            PlayerFlip();
       else if (rb.linearVelocity.x < 0 && FacingRight == true)
            PlayerFlip();
    }
    private void PlayerFlip() 
    { 
        transform.Rotate(0f,180f,0f);
        FacingRight = !FacingRight;
    }
    //Gizmos to show the ground check distance
    /*This will help us visualize the distance at which the player checks for the ground,
    which is crucial for implementing jumping mechanics and ensuring the player can only jump when grounded.*/
    /*It will draw a line from the player's position downwards, indicating the distance
    at which the player checks for the ground.*/
    //This is useful for debugging and fine-tuning the player's jump mechanics.
    //Its is only for visualization purposes and does not affect the actual gameplay mechanics.
    //Can do dectection without Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position +new Vector3(0f, -GroundCheckDistance, 0f));
    }
}
