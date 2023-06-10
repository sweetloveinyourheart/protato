using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isFacingRight = true;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] CharacterHealthbar healthBar;
    [SerializeField] float maxHp = 100f;

    private Rigidbody2D rb;
    private Animator animator;
    private float attackSpeed = 0.3f;
    private float hp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hp = maxHp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        Move(movement);
        FlipCharacter(moveX);
        UpdateAnimation(movement);
    }

    private void Move(Vector2 movement)
    {
        Vector2 velocity = movement * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + velocity);
    }

    private void FlipCharacter(float moveX)
    {
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimation(Vector2 movement)
    {
        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isRunning", isMoving);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackSpeed);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthBar.UpdateHealthbar(hp, maxHp);

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}