using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]private bool FacingRight=true;
    private float MoveInputX;
    [SerializeField ]private float MoveSpeed=3f;
    [SerializeField] private float JumpForce=5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        PlayerMovement();
        HandleAnimation();
        FlipHandle();
    }
    private void HandleAnimation()
    {
        bool isMoving = rb.linearVelocity.x!=0;
        animator.SetBool("IsMoving", isMoving);
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
}
