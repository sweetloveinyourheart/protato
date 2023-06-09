using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] ShotgunBullet bulletPrefab;
    [SerializeField] Transform firePos;

    bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShootBullet(bool isFacingRight)
    {
        for (int i = 0; i < 3; i++)
        {
            ShotgunBullet shotgunBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            shotgunBullet.Launch(isFacingRight, damage);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void UpdateWPState(bool state, bool isFacingRight)
    {
        if(!isShooting && state)
        {
            StartCoroutine(ShootBullet(isFacingRight));
            isShooting = true;
        }

        if(isShooting && !state)
        {
            isShooting = false;
        }

    }
}
