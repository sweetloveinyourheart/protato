using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D bulletRigidbody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletLifetime = 1f;

    float damage;

    // Start is called before the first frame update
    void Start()
    {
        //bulletRigidbody.freezeRotation = true;
        Destroy(gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(bool isFacingRight, float dmg)
    {
        damage = dmg;

        if (isFacingRight)
        {
            bulletRigidbody.velocity = transform.right * bulletSpeed;
            spriteRenderer.flipX = false;
        }
        else
        {
            bulletRigidbody.velocity = -transform.right * bulletSpeed;
            spriteRenderer.flipX = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster)
        {
            monster.TakeDamage(damage);
            Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
