using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sprite;
    protected Animator anim;
    [Header("Health Details")]
    [SerializeField] private Material damageMaterial;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private float damageFlashDuration = 0.2f;
    //To Store multiple coutines and stop them when needed.
    // Can run multiple couroutines at the same time!!
    private Coroutine damageFlashCoroutine;
    //Can only have fixed size.
    //Cannot add or remove colliders from this array at runtime, 
    //as its size is determined at compile time. 
    //Faster than List but less flexible.
    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask WhatIsTarget;

    [Header("Movement Details")]
    [SerializeField] protected bool FacingRight = true;
    protected int FacingDir = 1;
    [SerializeField] protected float MoveSpeed = 3f;
    [SerializeField] private float JumpForce = 5f;
    private float MoveInputX;
    protected bool canMove = true;
    private bool canJump = true;

    [Header("Collision Details")]
    [SerializeField] private float GroundCheckDistance;
    protected bool IsGrounded;
    [SerializeField] private LayerMask GroundLayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }
    protected virtual void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovemnet();
        HandleAnimation();
        FlipHandle();
    }
    public void DamageTargets()
    {
        Collider2D[] enmeyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsTarget);

        foreach (Collider2D enemy in enmeyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            entityTarget.TakeDamage();


        }

    }

    protected virtual void TakeDamage()
    {
        currentHealth = currentHealth - 1;
        PlayDamageFeedback();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedback()
    {
        if (damageFlashCoroutine != null)
            StopCoroutine(damageFlashCoroutine);
        
        damageFlashCoroutine = StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
       Material originalMat =sprite.material;
       sprite.material = damageMaterial;
       yield return new WaitForSeconds(damageFlashDuration);
       sprite.material = originalMat;
    }
    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;
        rb.gravityScale = 12f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

    }

    public void EnableJumpAndMovemnet(bool enable)
    {
        canMove = enable;
        canJump = enable;
    }
    protected virtual void HandleAnimation()
    {
        anim.SetFloat("XVelocity", rb.linearVelocity.x);
        anim.SetFloat("YVelocity", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", IsGrounded);
    }

    private void HandleInput()
    {
        //Horizontal movement
        MoveInputX = Input.GetAxisRaw("Horizontal");


        //Jumping mechanics

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();


        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }
    protected virtual void HandleMovemnet()
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
    protected virtual void HandleAttack()
    {
        if (IsGrounded)
        {
            anim.SetTrigger("Attack");
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
    protected virtual void HandleCollision()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, GroundLayer);
    }
    protected virtual void FlipHandle()
    {
        if (rb.linearVelocity.x > 0 && FacingRight == false)
            PlayerFlip();
        else if (rb.linearVelocity.x < 0 && FacingRight == true)
            PlayerFlip();
    }
    protected virtual void PlayerFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        FacingRight = !FacingRight;
        FacingDir = FacingDir * -1;
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
