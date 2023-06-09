using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform target;
    private bool isMoving;

    [SerializeField] float maxHp = 100f;
    [SerializeField] float hp = 100f;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] GameObject dieEffect;
    [SerializeField] MonsterHealthbar healthBar;
    [SerializeField] Supply supply;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.Setup(transform);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            if (!isMoving)
                Move(direction);
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
            // Apply a force to push the monster back
            Vector2 pushDirection = (target.position.x < transform.position.x) ? transform.right : -transform.right;
            StartCoroutine(PushBackAndEnableMovement(pushDirection));
        }
    }

    private IEnumerator PushBackAndEnableMovement(Vector2 pushDirection)
    {
        yield return new WaitForSeconds(0.1f);
        isMoving = false; // Re-enable Move
    }

    private void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);

        // Randomly determine whether to spawn a supply or not
        float spawnChance = Random.value;
        Debug.Log(spawnChance);
        if (spawnChance <= supply.spawnChance)
        {
            Instantiate(supply, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
