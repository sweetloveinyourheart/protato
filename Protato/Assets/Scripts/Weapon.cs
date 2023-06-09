using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons
{
    public static string Axe = "Axe";
    public static string Shotgun = "Shotgun";
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Transform wpPosition;

    GameObject weapon;

    public string currentWeapon = Weapons.Axe;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPrefab(currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentWeapon)
        {
            case "Axe":
                HandleAxe();
                return;
            case "Shotgun":
                HandleShotgun();
                return;

            default:
                return;
        }
    }

    void HandleAxe()
    {
        Axe axe = weapon.GetComponent<Axe>();
        if(axe)
        {
            axe.UpdateWPState(character.isAttacking);
        }
    }

    void HandleShotgun()
    {
        Shotgun shotgun = weapon.GetComponent<Shotgun>();
        if (shotgun)
        {
            shotgun.UpdateWPState(character.isAttacking, character.isFacingRight);
        }
    }

    void SpawnPrefab(string prefabName)
    {
        // Remove previos wp
        DeletePrefab();

        string path = "Weapons/" + prefabName;
        // Load the prefab by its name from the Resources folder
        GameObject prefab = Resources.Load<GameObject>(path);

        // Check if the prefab was loaded successfully
        if (prefab != null)
        {
            // Instantiate the prefab at a specific position and rotation
            GameObject instantiatedPrefab = Instantiate(prefab, wpPosition.position, wpPosition.rotation, wpPosition);

            instantiatedPrefab.transform.parent = wpPosition;
            weapon = instantiatedPrefab;
        }
        else
        {
            Debug.LogError("Prefab not found: " + prefabName);
        }
    }

    void DeletePrefab()
    {
        if (weapon != null)
        {
            Destroy(weapon);
        }
    }

    public void PickupWeapon(string wp)
    {
        SpawnPrefab(wp);
        currentWeapon = wp;
    }
}
