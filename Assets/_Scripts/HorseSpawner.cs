using UnityEngine;
using System.Collections;

public class HorseSpawner : MonoBehaviour
{
    public GameObject[] horsePrefabs;


    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 1.5f;


    public Vector3 spawnOffset;
    public float laneWidth = 3f;


    public float minScale = 0.95f;
    public float maxScale = 1.05f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnHorse();

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    void SpawnHorse()
    {
        if (horsePrefabs.Length == 0)
            return;


        int index = Random.Range(0, horsePrefabs.Length);


        float laneOffset = Random.Range(-laneWidth, laneWidth);

        Vector3 pos = transform.position + spawnOffset + new Vector3(laneOffset, 0f, 0f);


        GameObject horse = Instantiate(horsePrefabs[index], pos, transform.rotation);


        float scale = Random.Range(minScale, maxScale);
        horse.transform.localScale = new Vector3(scale, scale, scale);
    }
}