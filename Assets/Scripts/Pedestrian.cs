﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Vector2 moveSpeedInterval;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 pickPocketMoneyInterval;
    [SerializeField] private Color pickpocketedColor;
    [SerializeField] private float pickPocketWalkspeedMultiplier = 1.5f;
    [SerializeField] private Animator animator;

    private float moveSpeed = 6f;
    private bool pickpocketed = false;
    private int direction = 1;

    private void Start()
    {
        moveSpeed = UnityEngine.Random.Range(moveSpeedInterval.x, moveSpeedInterval.y);

        animator.Play("PedWalk" + UnityEngine.Random.Range(1, 5));
        animator.SetFloat("walkspeed", moveSpeed / 1.2f);
    }

    void Update()
    {
        Move();
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        if(direction == 1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void Move()
    {
        rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime);
    }

    public int Pickpocket(float multiplier)
    {
        if (!pickpocketed)
        {
            int moneyStolen = (int) (UnityEngine.Random.Range(pickPocketMoneyInterval.x, pickPocketMoneyInterval.y) * multiplier);
            spriteRenderer.color = pickpocketedColor;
            pickpocketed = true;

            moveSpeed *= pickPocketWalkspeedMultiplier;

            return moneyStolen;
        }

        return 0;
    }
}
