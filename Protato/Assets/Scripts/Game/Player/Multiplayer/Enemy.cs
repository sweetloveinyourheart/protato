using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Enemy : Character
{
    public string userId;

    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hp = maxHp;
        rb.isKinematic = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {
            CheckResponse();            
        }
    }

    private async void CheckResponse()
    {
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();
        if (res != null)
        {
            string type = res["type"];
            JSONNode data = res["data"];

            if (type == ServerEvent.PlayerStateUpdated)
            {
                string playerId = data["userId"];
                if (playerId == userId)
                {
                    // Movement
                    float xPos = data["xPos"].AsFloat;
                    float yPos = data["yPos"].AsFloat;

                    Vector2 previousPos = transform.position;
                    Vector2 currentPos = new Vector2(xPos, yPos);
                    Vector2 movement = currentPos - previousPos; // check if the player is moving or not

                    transform.position = currentPos;

                    FlipCharacter(movement.x);
                    UpdateAnimation(movement);

                    // Attack
                    isAttacking = data["isAttacking"].AsBool;
                    if (isAttacking) StartCoroutine(Attack());

                    // Takehit
                    hp = data["hp"].AsFloat;
                    healthBar.UpdateHealthbar(hp, maxHp);

                    if (hp <= 0)
                    {
                        Die();
                    }
                }
            }
        }
    }

    protected override void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        MultiplayerManager.Instance.EnemyDead();
        gameObject.SetActive(false);
        isDead = true;
    }
}
