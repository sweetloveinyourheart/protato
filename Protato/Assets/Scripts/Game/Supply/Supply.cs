using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SupplyItems
{
    Shotgun
}

public class Supply : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem pickupEffect;

    public float spawnChance = 0.25f;

    SupplyItems supplyItem;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        RandomSupply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomSupply()
    {
        SupplyItems randomItem = GetRandomEnumValue<SupplyItems>();
        supplyItem = randomItem;

        // update sprite
        int itemIndex = Convert.ToInt32(randomItem);
        spriteRenderer.sprite = sprites[itemIndex];
    }

    static T GetRandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        var random = new System.Random();
        int index = random.Next(values.Length);
        return (T)values.GetValue(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.CompareTag("Player");

        if(isPlayer)
        {
            switch (supplyItem)
            {
                case SupplyItems.Shotgun:
                    weapon.PickupWeapon("Shotgun");
                    Destroy(gameObject);
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                    return;

                default:
                    Destroy(gameObject);
                    return;
            }
        }

    }

}
