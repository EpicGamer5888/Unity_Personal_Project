using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
 {
    public bool canSpawn = false;
    public float spawnTime = 120f;
    public float enemyStatModifier = 2;
    public int enemyCount = 0;
    public int enemySpawn = 0;
    public Transform[] spawnLocations;
    public GameObject enemytryPrefab;
    public List<GameObject> spawnedEnemies;
    private float spawnedAt;

    
        // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawning()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnTime);
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject obj = Instantiate(enemytryPrefab, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity);
                spawnedEnemies.Add(obj);
            }
            spawnedAt = Time.time;
            spawnTime = 10f;
            enemyCount *= 2;
            yield return new WaitUntil(() => spawnedEnemies.TrueForAll((GameObject obj) => obj == null) || Time.time > spawnedAt + 120);
            spawnedEnemies.Clear();
        }
    }
}
