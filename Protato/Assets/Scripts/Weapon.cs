using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] Character character;

    CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(character.isAttacking)
        {
            circleCollider2D.enabled = true;
        } else
        {
            circleCollider2D.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if(monster)
        {
            monster.TakeDamage(damage);
        }
    }
}
