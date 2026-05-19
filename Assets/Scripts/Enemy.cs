using UnityEngine;

public class Enemy : Entity
{
    
    private bool playerDetected;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        HandleCollision();
        HandleAnimation();
        HandleMovemnet();
        FlipHandle();
        HandleAttack();
    }
    protected override void TakeDamage()
    {
        base.TakeDamage();
        
    }
    protected override void HandleAttack()
    {
        if (playerDetected)
            anim.SetTrigger("EnemyAttacks");
    }
    protected override void Die()
    {
       anim.SetTrigger("EnemyDead?");
       Destroy(gameObject, 1.25f);
    }

    protected override void HandleMovemnet()
    {
        if (canMove == true)
            rb.linearVelocity = new Vector2(FacingDir * MoveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    protected override void FlipHandle()
    {
        if (player == null) return;

        bool playerIsToTheRight = player.position.x > transform.position.x;

        if (playerIsToTheRight && !FacingRight)
            PlayerFlip();
        else if (!playerIsToTheRight && FacingRight)
            PlayerFlip();
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, WhatIsTarget);
    }
}