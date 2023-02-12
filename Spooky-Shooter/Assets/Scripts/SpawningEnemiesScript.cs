using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemiesScript : MonoBehaviour
{
    [SerializeField] private GameObject[] monstersToSpawn;
    [SerializeField] private float timeBetweenSpawning = 3f;


    private int numberOfEnemies;
    private float countdown = 5f;

    private void Awake()
    {
        numberOfEnemies = monstersToSpawn.Length;
    }

    private void Update()
    {
        if (countdown <= 0)
        {
            SpawnEnemy();
            countdown = timeBetweenSpawning;
        }

        countdown -= Time.deltaTime;
    }

    private void SpawnEnemy() 
    {
        int enemyNumber = Random.Range(0, numberOfEnemies);
        Instantiate(monstersToSpawn[enemyNumber], transform.position, Quaternion.identity);
    }


}
