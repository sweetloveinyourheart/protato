using UnityEngine;

public class Axe : MonoBehaviour
{
    CircleCollider2D meleeColider;
    [SerializeField] float damage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        meleeColider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster)
        {
            monster.TakeDamage(damage);
        }
    }

    public void UpdateWPState(bool state)
    {
        meleeColider.enabled = state;
    }
}
