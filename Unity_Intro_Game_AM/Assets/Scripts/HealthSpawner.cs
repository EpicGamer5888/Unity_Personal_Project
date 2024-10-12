using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public bool canSpawnHealth = false;
    public float spawnTime = 10f;
    public int healthCount = 0;
    public int healthSpawn = 0;
    public Transform[] healthspawnLocations;
    public GameObject healthPickupPrefab;
    public List<GameObject> spawnedHealth;
    private float spawnedAt;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningHealth());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawningHealth()
    {
        while (canSpawnHealth)
        {
            yield return new WaitForSeconds(spawnTime);
            for (int i = 0; i < healthCount; i++)
            {
                GameObject obj = Instantiate(healthPickupPrefab, healthspawnLocations[Random.Range(0, healthspawnLocations.Length)].position, Quaternion.identity);
                spawnedHealth.Add(obj);
            }
            spawnedAt = Time.time;
            yield return new WaitUntil(() => spawnedHealth.TrueForAll((GameObject obj) => obj == null));
            spawnedHealth.Clear();
        }
    }
}