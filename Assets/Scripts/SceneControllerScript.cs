using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerScript : MonoBehaviour
{
    [SerializeField] private int maxEnemiesOnLvl;
    public int currentEnemiesAmount; // TODO: set only property  
    [SerializeField] private int spawnAmount;
    GameObject player;
    private string enemyPrefabName = "EnemyPrefab";
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;


    [Space(10)] 
    [Header("Main Goal")]
    [SerializeField] private Slider mainGoalSlider;
    [SerializeField] private int timeOFRound;
    private float timer;
    [SerializeField] float[] keyPointForSpawn = new float[3];
    




    void Awake()
    {
        player = GameObject.Find("Player");
        mainGoalSlider.maxValue = timeOFRound;
        mainGoalSlider.minValue = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
        CheckMainGoalProgress();



    }

    void CheckMainGoalProgress()
    {
        
        if (timer < timeOFRound)
        {
            timer += Time.deltaTime;
            mainGoalSlider.value = timer;
        }
        else
        {
            timer = 4;
            Debug.Log("You win! Load nextScene");
        }

        if (timer >= keyPointForSpawn[0]&& timer < keyPointForSpawn[1])
        {
            Debug.Log("Кількість мобів збільшено 1");
            // Код для першого значення
        }
        else if (timer >= keyPointForSpawn[1] && timer < keyPointForSpawn[2])
        {
            Debug.Log("Кількість мобів збільшено 2");
            // Код для другого значення
        }
        else if (timer >= keyPointForSpawn[2])
        {
            Debug.Log("Кількість мобів збільшено 3");
            // Код для третього значення
        }
    }
    

    void SpawnEnemies()
    {
        int triggerAmount = maxEnemiesOnLvl - spawnAmount;
        if(currentEnemiesAmount < triggerAmount)
        {
            for(int i=0; i < spawnAmount; i++)
            {
                GameObject enemyPref = Resources.Load<GameObject>(enemyPrefabName);
                Vector3 spawnPoint = SetSpawnPosition();
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

    Vector3 SetSpawnPosition()
    {
        Vector3 spawnPoint = GetRandomPositionAroundPlayer();
        RaycastHit hit;
        bool positionFound = false;
        while(!positionFound)
        {
            Ray ray = new Ray(spawnPoint, Vector3.down);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    positionFound = true;
                }
            }
            else { spawnPoint = GetRandomPositionAroundPlayer(); }
        }
        
        return spawnPoint;

    }
}
