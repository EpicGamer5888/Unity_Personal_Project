using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public bool canSpawnAmmo = false;
    public float spawnTime = 10f;
    public int ammoCount = 0;
    public int ammoSpawn = 0;
    public Transform[] ammospawnLocations;
    public GameObject AmmoBoxPrefab;
    public List<GameObject> spawnedAmmo;
    private float spawnedAt;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningAmmo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawningAmmo()
    {
        while (canSpawnAmmo)
        {
            yield return new WaitForSeconds(spawnTime);
            for (int i = 0; i < ammoCount; i++)
            {
                GameObject obj = Instantiate(AmmoBoxPrefab, ammospawnLocations[Random.Range(0, ammospawnLocations.Length)].position, Quaternion.identity);
                spawnedAmmo.Add(obj);
            }
            spawnedAt = Time.time;
            yield return new WaitUntil(() => spawnedAmmo.TrueForAll((GameObject obj) => obj == null) || Time.time > spawnedAt + 120);
            spawnedAmmo.Clear();
        }
    }
}
