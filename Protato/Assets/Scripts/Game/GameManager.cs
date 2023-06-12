using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform target;
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] int numberOfMonsters = 10;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameResult gameResult;

    private int activeMonsters; // Track the number of active monsters
    private int score = 0; // 1 monster die equal to 1 point

    public static GameManager Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        activeMonsters = numberOfMonsters; // Set the initial count of active monsters
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
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);

        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        Monster monsterMovement = monster.GetComponent<Monster>();
        monsterMovement.target = target;
    }

    public void MonsterDestroyed()
    {
        activeMonsters--;
        score++;

        if (activeMonsters <= 0)
        {
            // All monsters have been destroyed
            // Set the game result to "Win"
            MatchManager.Instance.SaveResult(true, score);
            StartCoroutine(ShowResultCoroutine(true));

        }
    }

    public void PlayerDead()
    {
        MatchManager.Instance.SaveResult(false, score);
        StartCoroutine(ShowResultCoroutine(false));
    }

    private IEnumerator ShowResultCoroutine(bool isWinner)
    {
        yield return new WaitForSeconds(1f);
        gameResult.Show(isWinner, score);
        gameResult.gameObject.SetActive(true);
    }
}
