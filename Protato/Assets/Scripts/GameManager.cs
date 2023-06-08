using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] int numberOfMonsters = 10;
    [SerializeField] Transform[] spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnMonstersRepeatedly());
    }

    private IEnumerator SpawnMonstersRepeatedly()
    {
        while (numberOfMonsters > 0)
        {
            SpawnMonster();
            numberOfMonsters--;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnMonster()
    {
        // Generate a random position from spawn points
        int spawnPointIndex = Random.Range(0, 3);
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);

        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        Monster monsterMovement = monster.GetComponent<Monster>();
        monsterMovement.target = target;
        monsterMovement.moveSpeed = moveSpeed;
    }

}
