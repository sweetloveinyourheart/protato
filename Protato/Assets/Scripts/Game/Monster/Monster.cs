using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform target;

    [SerializeField] float maxHp = 100f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float damage = 5f;

    [SerializeField] GameObject dieEffect;
    [SerializeField] MonsterHealthbar healthBar;
    [SerializeField] Supply supply;

    Rigidbody2D rb;
    bool isMoving;
    bool isAttacking = false; // Flag indicating whether the monster is currently attacking

    bool canAttacking = false; // Flag indicating whether the monster can attack the player
    float hp = 100f;
    Character character;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.Setup(transform);
        character = FindObjectOfType<Character>();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            if (!isMoving)
                Move(direction);
        }

        if(isAttacking && canAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private void Move(Vector2 direction)
    {
        Vector2 velocity = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthBar.UpdateHealthbar(hp, maxHp);

        if(hp <= 0)
        {
            Die();
        }
        else
        {
            // Push the monster back
            StartCoroutine(PushBackAndEnableMovement());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = false;
        character.TakeDamage(damage);
        yield return new WaitForSeconds(0.5f);
        isAttacking = true;
    }

    private IEnumerator PushBackAndEnableMovement()
    {
        yield return new WaitForSeconds(0.1f);
        isMoving = false; // Re-enable Move
    }

    private void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        // Randomly determine whether to spawn a supply or not
        float spawnChance = Random.value;
        if (spawnChance <= supply.spawnChance)
        {
            Instantiate(supply, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.CompareTag("Player");
        if(isPlayer)
        {
            canAttacking = true;
            isAttacking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        bool isPlayer = collision.collider.CompareTag("Player");
        if (isPlayer)
        {
            canAttacking = false;
            isAttacking = false;
        }
    }
}
