using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    private bool isMoving;

    [SerializeField] float maxHp = 100f;
    [SerializeField] float hp = 100f;
    [SerializeField] float pushForce = 1f;

    [SerializeField] MonsterHealthbar healthBar;

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
            isMoving = true; // Disable Move temporarily
            StartCoroutine(PushBackAndEnableMovement(pushDirection));
        }
    }

    private IEnumerator PushBackAndEnableMovement(Vector2 pushDirection)
    {
        rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        isMoving = false; // Re-enable Move
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
