using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour
{
    [SerializeField] private int maxEnemiesOnLvl;
    public int currentEnemiesAmount; // TODO: set only property  
    [SerializeField] private int spawnAmount;
    GameObject player;
    private string enemyPrefabName = "EnemyPrefab";
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;


    void Awake()
    {
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        int triggerAmount = maxEnemiesOnLvl - spawnAmount;
        if(currentEnemiesAmount < triggerAmount)
        {
            for(int i=0; i < spawnAmount; i++)
            {
                GameObject enemyPref = Resources.Load<GameObject>(enemyPrefabName);
                Vector3 spawnPoint = GetRandomPositionAroundPlayer();
                GameObject enemy = Instantiate(enemyPref, spawnPoint, Quaternion.identity);
                currentEnemiesAmount++;
            }
            


        }
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        float angle = Random.Range(0, 2 * Mathf.PI);

        float x = player.transform.position.x + distance * Mathf.Cos(angle);
        float z = player.transform.position.z + distance * Mathf.Sin(angle);

        return new Vector3(x, player.transform.position.y, z);
    }
}
