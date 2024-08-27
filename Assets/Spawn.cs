using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulpitSpawner : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float pulpitSpawnTime = 2.5f;
    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
   
    private const float pulpitSize = 9f;
    private List<Vector2> occupiedPositions = new List<Vector2>();

    void Start()
    {
        Debug.Log("PulpitSpawner started");
        StartCoroutine(SpawnPulpits());
    }

    IEnumerator SpawnPulpits()
    {
        Debug.Log("SpawnPulpits coroutine started");
        while (true)
        {
            Vector2 spawnPosition = GetValidSpawnPosition();
            Debug.Log($"Attempting to spawn pulpit at position: {spawnPosition}");
           
            if (spawnPosition != Vector2.positiveInfinity)
            {
                GameObject newPulpit = Instantiate(pulpitPrefab, new Vector3(spawnPosition.x, 0, spawnPosition.y), Quaternion.identity);
                occupiedPositions.Add(spawnPosition);
                Debug.Log($"Pulpit spawned at {spawnPosition}");
                float destroyTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
                StartCoroutine(DestroyPulpitAfterDelay(newPulpit, spawnPosition, destroyTime));
                Debug.Log($"Pulpit scheduled for destruction in {destroyTime} seconds");
            }
            else
            {
                Debug.Log("Failed to find valid spawn position");
            }
            yield return new WaitForSeconds(pulpitSpawnTime);
        }
    }

    Vector2 GetValidSpawnPosition()
    {
        if (occupiedPositions.Count == 0)
        {
            return new Vector2(0, 0);
        }
        List<Vector2> validPositions = new List<Vector2>();
        foreach (Vector2 occupied in occupiedPositions)
        {
            Vector2[] adjacentPositions = new Vector2[]
            {
                occupied + new Vector2(pulpitSize, 0),
                occupied - new Vector2(pulpitSize, 0),
                occupied + new Vector2(0, pulpitSize),
                occupied - new Vector2(0, pulpitSize)
            };
            foreach (Vector2 adjacent in adjacentPositions)
            {
                if (!occupiedPositions.Contains(adjacent) && !validPositions.Contains(adjacent))
                {
                    validPositions.Add(adjacent);
                }
            }
        }
        if (validPositions.Count > 0)
        {
            return validPositions[Random.Range(0, validPositions.Count)];
        }
        return Vector2.positiveInfinity;
    }

    IEnumerator DestroyPulpitAfterDelay(GameObject pulpit, Vector2 position, float delay)
    {
        Debug.Log($"Pulpit destruction scheduled. Delay: {delay} seconds");
        yield return new WaitForSeconds(delay);
       
        Debug.Log("Pulpit destruction time reached");
        occupiedPositions.Remove(position);
        Destroy(pulpit);
        Debug.Log("Pulpit destroyed");
       
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(1);
            Debug.Log("Score incremented after pulpit destruction");
        }
        else
        {
            Debug.LogError("ScoreManager.Instance is null when trying to increment score!");
        }
    }
}