using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    //Can only have fixed size.
    //Cannot add or remove colliders from this array at runtime, 
    //as its size is determined at compile time. 
    //Faster than List but less flexible.
    [Header("Attack Details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask WhatIsenemy;
    private Animator animator;

    [Header("Movement Details")]
    [SerializeField] private bool FacingRight = true;
    [SerializeField] private float MoveSpeed = 3f;
    [SerializeField] private float JumpForce = 5f;
    private float MoveInputX;
    private bool canMove = true;
    private bool canJump = true;

    [Header("Collision Details")]
    [SerializeField] private float GroundCheckDistance;
    private bool IsGrounded;
    [SerializeField] private LayerMask GroundLayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovemnet();
        HandleAnimation();
        FlipHandle();
    }
    public void DamageEnemies()
    {
        Collider2D[] enmeyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsenemy);

        foreach (Collider2D enemy in enmeyColliders)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
        }
     
    }
    public void EnableJumpAndMovemnet(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }
    private void HandleAnimation()
    {
        animator.SetFloat("XVelocity", rb.linearVelocity.x);
        animator.SetBool("IsGrounded", IsGrounded);
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
    }

    private void HandleInput()
    {
        //Horizontal movement
        MoveInputX = Input.GetAxisRaw("Horizontal");


        //Jumping mechanics

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();


        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();
    }
    private void HandleMovemnet()
    {
        if (canMove == true)
            rb.linearVelocity = new Vector2(MoveInputX * MoveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

    }
    private void TryToJump()
    {
        if (IsGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }
    private void TryToAttack()
    {
        if (IsGrounded)
        {
            animator.SetTrigger("Attack");
        }
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
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayer);
    }
    private void FlipHandle()
    {
        if (rb.linearVelocity.x > 0 && FacingRight == false)
            PlayerFlip();
        else if (rb.linearVelocity.x < 0 && FacingRight == true)
            PlayerFlip();
    }
    private void PlayerFlip()
    {
        transform.Rotate(0f, 180f, 0f);
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
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, -GroundCheckDistance, 0f));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
